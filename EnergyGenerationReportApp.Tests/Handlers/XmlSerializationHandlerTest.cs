using EnergyReportGenerationApp.Entities.GenerationReportInput;
using EnergyReportGenerationApp.Entities.GenerationReportOutput;
using EnergyReportGenerationApp.Handlers;
using EnergyReportGenerationApp.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace EnergyReportGenerationApp.Tests.Handlers
{
    [TestClass]
    public class XmlSerializationHandlerTest
    {
        private const string OutputXmlFilepath = "AppData\\Output.xml";
        private IXmlSerializationHandler _xmlSerializationHandler;
        private GenerationReportOutputDetail _generationReportOutputDetail;


        [TestInitialize]
        public void InitTest()
        {
            this._xmlSerializationHandler = new XmlSerializationHandler();
            DeleteOutputFile();
        }



        [TestMethod]
        public void ShouldInheritXmlSerializationHandlerInterface()
        {
            Assert.IsInstanceOfType(this._xmlSerializationHandler, typeof(IXmlSerializationHandler));
        }

        [TestMethod]
        public void DeserializeToObjectShouldParseInputXmlIntoGenerationReportIntputDetail()
        {
            // Arrange
            var filePath = "AppData\\01-Basic.xml";

            // Act
            var result = _xmlSerializationHandler.DeserializeToObject<GenerationReportIntputDetail>(filePath);

            // Assert
            Assert.AreEqual(2, result.Wind.WindGenerator.Count);
            Assert.AreEqual(3, result.Gas.GasGenerator[0].Generation.Day.Count);
            Assert.AreEqual("Coal[1]", result.Coal.CoalGenerator[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeserializeToObjectShouldThrowExceptionWhenInputXmlIsInvalid()
        {
            // Arrange
            var filePath = "AppData\\Invalid.xml";

            // Act & Assert
            var result = _xmlSerializationHandler.DeserializeToObject<GenerationReportIntputDetail>(filePath);
        }

        [TestMethod]
        public void SerializeToXmlShouldWriteXmlToOutputPath()
        {
            // Arrange
            var filePath = OutputXmlFilepath;
            var actualHeatRateList = new List<GenerationOutputActualHeatRatesActualHeatRate>();
            var acutualHeatRate = new GenerationOutputActualHeatRatesActualHeatRate { HeatRate = "1", Name = "Coal[1]" };
            actualHeatRateList.Add(acutualHeatRate);

            _generationReportOutputDetail = new GenerationReportOutputDetail
            {
                ActualHeatRates = new GenerationOutputActualHeatRates
                {
                    ActualHeatRates = new List<GenerationOutputActualHeatRatesActualHeatRate>()
                }
            };

            // Act
            _xmlSerializationHandler.SerializeToXml<GenerationReportOutputDetail>(_generationReportOutputDetail, filePath);

            //Assert
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SerializeToXmlShouldThrowExceptionWhenOutputPathIsInvalid()
        {
            // Arrange
            var filePath = "InvalidPath.file";

            // Act & Assert
            var result = _xmlSerializationHandler.DeserializeToObject<GenerationReportIntputDetail>(filePath);
        }

        private void DeleteOutputFile()
        {
            if (File.Exists(OutputXmlFilepath))
            {
                File.Delete(OutputXmlFilepath);
            }
        }
    }
}
