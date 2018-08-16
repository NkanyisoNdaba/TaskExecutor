using Nancy;
using Nancy.Testing;
using NUnit.Framework;
using TaskExecutor.Nancy;

namespace TaskExecutor.Tests
{
    [TestFixture]
    public class HealthCheckTest
    {
        [Test]
        public void CheckHealth_GivenHealthEndpoint_ShouldReturnStatusCode200()
        {
            // Arrange
            var browser = new Browser(with =>
            {
                with.Module<HealthEndpoint>();
            }, to => to.Accept("application/json"));

            // Act
            var result = browser.Get("/health", with => {
                with.HttpRequest();
            });

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
