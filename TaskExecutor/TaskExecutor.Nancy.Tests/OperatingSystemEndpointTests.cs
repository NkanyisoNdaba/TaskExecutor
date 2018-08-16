using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using TaskExecutor.Nancy;
using TaskExecutor.Nancy.Models;

namespace TaskExecutor.Tests
{
    [TestFixture]
    public class OperatingSystemEndpointTests
    {
        [Test]
        public void GetOperatingSystem_GivenOSEndpoint_ShouldRetunStatusCode200AndOperatingSystem()
        {
            //Arrange
            var osProcessorSubstitute = Substitute.For<IOperatingSystemProcessor>();
            var operatingSystem = "Microsoft Windows NT 10.0.16299.0";
            osProcessorSubstitute.GetOperatingSystem().Returns(operatingSystem);
            var browser = new Browser(with =>
            {
                with.Module<OperatingSystemEndpoint>();
                with.Dependency(osProcessorSubstitute);
            });
            //Act
            var result = browser.Get("/os", with => {
                with.HttpRequest();
            });
            var actual = JsonConvert.DeserializeObject<MachineInformationResults>(result.Body.AsString());
            //Assert
            var expected = "Microsoft Windows NT 10.0.16299.0";
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expected, actual.output);

        }

        [Test]
        public void GetOperatingSystem_GivenValidOperatingSystemEndpointAndExecutionFails_ShouldReturnStatusCode500()
        {
            //Arrange
            var osProcessorSubstitute = Substitute.For<IOperatingSystemProcessor>();
            osProcessorSubstitute.GetOperatingSystem().Throws(new Exception("something went wrong"));
            var browser = new Browser(with =>
            {
                with.Module<OperatingSystemEndpoint>();
                with.Dependency(osProcessorSubstitute);
            });
            //Act
            var result = browser.Get("/os", with =>
            {
                with.HttpRequest();
            });
            //Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
