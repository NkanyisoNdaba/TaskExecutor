using NUnit.Framework;
using System;

namespace TaskExecutor.Tests
{
    public class OperatingSystemTests
    {
        [Test]
        public void GetOperatingSystem_GivenOSCommandIsSent_ShouldReturnComputerOperatingSystem()
        {
            //Arrange
            var operatingSystem = CreateOperatingSystem();
            var expected = Environment.OSVersion.ToString();

            //Act
            var result = operatingSystem.GetOperatingSystem();

            //Assert
            Assert.AreEqual(expected, result);
        }

        private OperatingSystemProcessor CreateOperatingSystem()
        {
            return  new OperatingSystemProcessor();
        }
    }
}