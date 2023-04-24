using EnergyReportGenerationApp.Entities.GenerationReportInput;
using EnergyReportGenerationApp.Entities.GenerationReportOutput;
using EnergyReportGenerationApp.Entities.ReferenceData;
using EnergyReportGenerationApp.Handlers;
using EnergyReportGenerationApp.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace EnergyReportGenerationApp.Tests.Handlers
{
    [TestClass]
    public class EnenrgyGenerationReportHandlerTest
    {
        private Mock<IFileHandler> _fileHandler;
        private Mock<IXmlSerializationHandler> _xmlSerializationHandler;
        private Mock<IEnergyDataProcessingHandler> _energyDataProcessingHandler;
        private IConfiguration _configuration;
        private IEnenrgyGenerationReportHandler _enenrgyGenerationReportHandler;

        [TestInitialize]
        public void InitTest()
        {
            this._configuration = this.Buildconfiguration();
            this._energyDataProcessingHandler = new Mock<IEnergyDataProcessingHandler>();
            this._fileHandler = new Mock<IFileHandler>();
            this._xmlSerializationHandler = new Mock<IXmlSerializationHandler>();
            this._enenrgyGenerationReportHandler = new EnenrgyGenerationReportHandler(this._fileHandler.Object,
                this._xmlSerializationHandler.Object, this._energyDataProcessingHandler.Object, this._configuration);
        }

        [TestMethod]
        public void ShouldInheritEnenrgyGenerationReportHandlerInterface()
        {
            Assert.IsInstanceOfType(this._enenrgyGenerationReportHandler, typeof(IEnenrgyGenerationReportHandler));
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenInputFileIsInvalid()
        {
            // Arrange
            _fileHandler.Setup(x => x.IsXmlvalid(It.IsAny<string>())).Throws(new Exception());

            // Act
            _enenrgyGenerationReportHandler.ProcessEnergyGenerationReport("TestPath");

            // Assert
            _xmlSerializationHandler.Verify(s => s.DeserializeToObject<GenerationReportIntputDetail>(It.IsAny<string>()), Times.Never);
            _xmlSerializationHandler.Verify(s => s.DeserializeToObject<ReferenceDataDetail>(It.IsAny<string>()), Times.Never);
            _energyDataProcessingHandler.Verify(s => s.ProcessEnergyGenerationData(It.IsAny<GenerationReportIntputDetail>(), It.IsAny<ReferenceDataDetail>()), Times.Never);
            _xmlSerializationHandler.Verify(s => s.SerializeToXml<GenerationReportOutputDetail>(It.IsAny<GenerationReportOutputDetail>(), It.IsAny<string>()), Times.Never);
            _fileHandler.Verify(s => s.MoveFileToFolder(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenInputFileIsNotAvailableForprocessing()
        {
            // Arrange
            _fileHandler.Setup(x => x.IsXmlvalid(It.IsAny<string>())).Returns(true);
            _fileHandler.Setup(x => x.IsFileReadyForProcessing(It.IsAny<string>())).Throws(new Exception());

            // Act
            _enenrgyGenerationReportHandler.ProcessEnergyGenerationReport("TestPath");

            // Assert
            _xmlSerializationHandler.Verify(s => s.DeserializeToObject<GenerationReportIntputDetail>(It.IsAny<string>()), Times.Never);
            _xmlSerializationHandler.Verify(s => s.DeserializeToObject<ReferenceDataDetail>(It.IsAny<string>()), Times.Never);
            _energyDataProcessingHandler.Verify(s => s.ProcessEnergyGenerationData(It.IsAny<GenerationReportIntputDetail>(), It.IsAny<ReferenceDataDetail>()), Times.Never);
            _xmlSerializationHandler.Verify(s => s.SerializeToXml<GenerationReportOutputDetail>(It.IsAny<GenerationReportOutputDetail>(), It.IsAny<string>()), Times.Never);
            _fileHandler.Verify(s => s.MoveFileToFolder(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ShouldProcessreportWhenInputFileIsvalidAndAvailableForprocessing()
        {
            // Arrange
            _fileHandler.Setup(x => x.IsXmlvalid(It.IsAny<string>())).Returns(true);
            _fileHandler.Setup(x => x.IsFileReadyForProcessing(It.IsAny<string>())).Returns(true);

            // Act
            _enenrgyGenerationReportHandler.ProcessEnergyGenerationReport("TestPath");

            // Assert
            _xmlSerializationHandler.Verify(s => s.DeserializeToObject<GenerationReportIntputDetail>(It.IsAny<string>()), Times.Once);
            _xmlSerializationHandler.Verify(s => s.DeserializeToObject<ReferenceDataDetail>(It.IsAny<string>()), Times.Once);
            _energyDataProcessingHandler.Verify(s => s.ProcessEnergyGenerationData(It.IsAny<GenerationReportIntputDetail>(), It.IsAny<ReferenceDataDetail>()), Times.Once);
            _xmlSerializationHandler.Verify(s => s.SerializeToXml<GenerationReportOutputDetail>(It.IsAny<GenerationReportOutputDetail>(), It.IsAny<string>()), Times.Once);
            _fileHandler.Verify(s => s.MoveFileToFolder(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        private IConfiguration Buildconfiguration()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            return configuration;
        }
    }
}
