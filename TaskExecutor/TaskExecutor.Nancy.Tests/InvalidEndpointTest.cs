using Nancy;
using Nancy.Testing;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using TaskExecutor.Nancy;

namespace TaskExecutor.Tests
{
    [TestFixture]
    public class InvalidEndpointTest
    {
        [Test]
        public void CheckInvalidEndpoint_GivenInvalidEndPoint_ShouldReturnStatusCode404()
        {
            //Arrange
            var machineInformation = Substitute.For<IHostNameProcessor>();
            machineInformation.GetHostName().Throws(new Exception("Not Found"));
            var browser = new Browser(with =>
            {
                with.Module<HostNameEndpoint>();
                with.Dependency<IHostNameProcessor>(machineInformation);
            });
            //Act
            var result = browser.Get("/drytrdfsgdsgsfds", with =>
            {
                with.HttpRequest();
            });
            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

    }
}
