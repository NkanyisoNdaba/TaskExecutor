using Nancy;
using Nancy.Testing;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using TaskExecutor.Nancy;

namespace TaskExecutor.Tests
{
    [TestFixture]
    public class HostNameEndpointTests
    {
        [Test]
        public void GetHostname_GivenHostnameEndpoint_ShouldReturnStatus200AndHostname()
        {
            //Arrange
            var hostnameProcessorSubstitute = Substitute.For<IHostNameProcessor>();
            var hostName = "DevFluence12-DBN";
            hostnameProcessorSubstitute.GetHostName().Returns(hostName);
            var browser = new Browser(with =>
            {
                with.Module<HostNameEndpoint>();
                with.Dependency(hostnameProcessorSubstitute);
            });
            //Act
            var result = browser.Get("/hostname", with => {
                with.HttpRequest();
            });
            var actual = JsonConvert.DeserializeObject<MachineInformationResults>(result.Body.AsString());
            //Assert
            var expected = "DevFluence12-DBN";
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expected, actual.output);
        }

        [Test]
        public void GetFullyQualifiedHostName_GivenFullyQualifiedEndpoint_ShouldReturnStatusCodeOk()
        {
            //Arrange
            var fullyQualified = "DevFluence12-DBN.domain.local";
            var hostnameSubstitute = Substitute.For<IHostNameProcessor>();
            hostnameSubstitute.GetFullyQualifiedHostName().Returns(fullyQualified);
            var browser = new Browser(with =>
            {
                with.Module<HostNameEndpoint>();
                with.Dependency(hostnameSubstitute);
            });
            //Act
            var result = browser.Get("/hostname", with => {
                with.HttpRequest();
                with.Query("fully-qualified", "true");
            });
            var actual = JsonConvert.DeserializeObject<MachineInformationResults>(result.Body.AsString());
            //Assert
            var expected = "DevFluence12-DBN.domain.local";
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expected, actual.output);
        }

        [Test]
        public void GetHostname_GivenValidHostnameEndpointAndExecutionFails_ShouldReturnStatusCode500()
        {
            //Arrange
            var hostnameProcessorSubstitute = Substitute.For<IHostNameProcessor>();
            hostnameProcessorSubstitute.GetHostName().Throws(new Exception("something went wrong"));
            var browser = new Browser(with =>
            {
                with.Module<HostNameEndpoint>();
                with.Dependency(hostnameProcessorSubstitute);
            });
            //Act
            var result = browser.Get("/hostname", with =>
            {
                with.HttpRequest();
            });
            //Assert
            hostnameProcessorSubstitute.Received(1).GetHostName();
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
