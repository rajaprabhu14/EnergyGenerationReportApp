using EnergyReportGenerationApp.Handlers;
using EnergyReportGenerationApp.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;

namespace EnergyReportGenerationApp.Tests.Handlers
{
    [TestClass]
    public class FileHandlerTest
    {
        private IFileHandler _fileHandler;

        [TestInitialize]
        public void InitTest()
        {
            this._fileHandler = new FileHandler();
        }

        [TestMethod]
        public void ShouldInheritFileHandlerInterface()
        {
            Assert.IsInstanceOfType(this._fileHandler, typeof(IFileHandler));
        }

        [TestMethod]
        public void IsFileReadyForProcessingShouldReturnTrueWhenFileIsAvailable()
        {
            // Arrange 
            var filePath = "AppData\\ReferenceData.xml";

            // Act
            var result = _fileHandler.IsFileReadyForProcessing(filePath);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsFileReadyForProcessingShouldReturnFalseWhenFileIsAvailable()
        {
            // Arrange 
            var filePath = "InvalidPath.file";

            // Act
            var result = _fileHandler.IsFileReadyForProcessing(filePath);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsXmlvalidShouldReturnTrueWhenFileIsValid()
        {
            // Arrange 
            var filePath = "AppData\\ReferenceData.xml";

            // Act
            var result = _fileHandler.IsXmlvalid(filePath);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void IsXmlvalidShouldReturnFalseWhenFileIsInValid()
        {
            // Arrange 
            var filePath = "AppData\\Invalid.xml";

            // Act
            var result = _fileHandler.IsXmlvalid(filePath);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
