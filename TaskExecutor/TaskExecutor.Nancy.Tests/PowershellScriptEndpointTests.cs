using System;
using Nancy;
using Nancy.Testing;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TaskExecutor.Nancy;
using TaskExecutor.Nancy.Models;
using TaskExecutorBoundry;

namespace TaskExecutor.Tests
{
    [TestFixture]
    public class ValidScriptEndpointTest
    {
        [Test]
        public void ExecuteScript_GivenValidScriptContent_ShouldReturnStatusCode200AndScriptContent()
        {
            // Arrange
            var scriptSubstitute = Substitute.For<IPowershellScript>();
            var scriptContent = $"Write-Output \"Hello world\"";

            scriptSubstitute.ExecuteScript(scriptContent).Returns("Hello world");
            var browser = new Browser(with =>
            {
                with.Module<PowershellScriptEndpoint>();
                with.Dependency(scriptSubstitute);
            });
            var scriptModel = new ScriptModel() { text = scriptContent };
            var expected = "Hello world";

            // Act
            var result = browser.Post("/script", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.JsonBody(scriptModel);

            });
            var actual = JsonConvert.DeserializeObject<ScriptModel>(result.Body.AsString());
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(expected, actual.text);
        }

        [Test]
        public void ExecuteScript_GivenEmptyScriptBody_ShouldReturnStatusCode400()
        {
            // Arrange
            var scriptSubstitute = Substitute.For<IPowershellScript>();
            var scriptContent = "";

            scriptSubstitute.ExecuteScript(scriptContent).Returns("");
            var browser = new Browser(with =>
            {
                with.Module<PowershellScriptEndpoint>();
                with.Dependency(scriptSubstitute);
                with.ApplicationStartupTask<NancyBootstrapper>();
            });

            var scriptModel = new ScriptModel() { text = scriptContent };

            // Act
            var result = browser.Post("/script", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.JsonBody(scriptModel);

            });
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public void ExecuteScript_GivenInvalidScriptContent_ShouldReturnStatusCode400()
        {
            // Arrange
            var scriptSubstitute = Substitute.For<IPowershellScript>();
            var scriptContent = $"griffins";

            scriptSubstitute.ExecuteScript(scriptContent).Returns($"The body content \'{scriptContent}\' is not recognized");
            var browser = new Browser(with =>
            {
                with.Module<PowershellScriptEndpoint>();
                with.Dependency(scriptSubstitute);
                with.ApplicationStartupTask<NancyBootstrapper>();
            });

            var scriptModel = new ScriptModel() { text = scriptContent };

            var expected = $"The body content \'{scriptContent}\' is not recognized";
            // Act
            var result = browser.Post("/script", with =>
            {
                with.HttpRequest();
                with.Header("Accept", "application/json");
                with.JsonBody(scriptModel);

            });
            var actual = JsonConvert.DeserializeObject<ScriptModel>(result.Body.AsString());
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.AreEqual(expected, actual.text);

        }

    }
}
