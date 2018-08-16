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
    public class IpEndpointTest
    {
        [Test]
        public void GetIpAddress_GivenIpEndpoint_ShouldRetunStatusCode200()
        {
            //Arrange
            var ipAddressProcessorSubstitute = Substitute.For<IIpAddressProcessor>();
            var ipAddress = "192.168.2.175";
            ipAddressProcessorSubstitute.GetIPAddress().Returns(ipAddress);
            var browser = new Browser(with =>
            {
                with.Module<IpEndpoint>();
                with.Dependency(ipAddressProcessorSubstitute);
                
            });
            //Act
            var result = browser.Get("/ip", with => {
                with.HttpRequest();
            });
            var actual = JsonConvert.DeserializeObject<MachineInformationResults>(result.Body.AsString());
            //Assert
            var expected = "192.168.2.175";
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expected, actual.output);
        }

        [Test]
        public void GetIpAddress_GivenValidIpAddressEndpointAndExecutionFails_ShouldReturnStatusCode500()
        {
            //Arrange
            var ipAddressProcessorSubstitute = Substitute.For<IIpAddressProcessor>();
            ipAddressProcessorSubstitute.GetIPAddress().Throws(new Exception("something went wrong"));
            var browser = new Browser(with =>
            {
                with.Module<IpEndpoint>();
                with.Dependency(ipAddressProcessorSubstitute);
            });
            //Act
            var result = browser.Get("/ip", with =>
            {
                with.HttpRequest();
            });
            //Assert
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
        }

    }
}
