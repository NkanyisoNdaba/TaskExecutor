using NUnit.Framework;
using System.IO;

namespace TaskExecutor.Tests
{
    [TestFixture]
    public class ScriptTests
    {
        [Test]
        public void RunScript_GivenScriptCommand_ShouldReturnScriptOutputHelloWorld()
        {
            var myDirectory = new DirectoryInfo(TestContext.CurrentContext.TestDirectory).Parent.Parent.Parent.Parent;
            var path = $"{myDirectory.FullName}\\TaskExecutor\\TaskExecutor\\Scripts\\HelloWorld.ps1";
            //Arrange
            var sut = CreateScript();
            //Act
            var actual = sut.RunScript(path);
            var expected = "Hello World";
            //Assert
            Assert.AreEqual(expected,actual);
        }
        private Script CreateScript()
        {
            return new Script();
        }

    }
}
