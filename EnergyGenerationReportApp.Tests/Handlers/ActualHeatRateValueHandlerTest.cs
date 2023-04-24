using EnergyReportGenerationApp.Entities.GenerationReportInput;
using EnergyReportGenerationApp.Entities.GenerationReportOutput;
using EnergyReportGenerationApp.Handlers;
using EnergyReportGenerationApp.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace EnergyReportGenerationApp.Tests.Handlers
{
    [TestClass]
    public class ActualHeatRateValueHandlerTest
    {
        private IActualHeatRateValueHandler _actualHeatRateValueHandler;

        private GenerationReportIntputDetail _generationReportIntputDetail;
        private GenerationReportOutputDetail _generationReportOutputDetail;

        [TestInitialize]
        public void InitTest()
        {
            this._actualHeatRateValueHandler = new ActualHeatRateValueHandler();
        }

        [TestMethod]
        public void ShouldInheritActualHeatRateValueHandlerInterface()
        {
            Assert.IsInstanceOfType(this._actualHeatRateValueHandler, typeof(IActualHeatRateValueHandler));
        }

        [TestMethod]
        public void CalculateActualHeatRateValueShouldCalculateAndReturnTwoValuesWhenTwoCoalGeneratorsPassed()
        {
            // Arrange
            var coalgeneratorList = new List<GenerationReportCoalCoalGenerator>();
            var firstCoalGenerator = new GenerationReportCoalCoalGenerator { ActualNetGeneration = (decimal)13.25, TotalHeatInput = (decimal)13.25, EmissionsRating = (decimal)0.38, Name = "One" };
            var seconCoalGenerator = new GenerationReportCoalCoalGenerator { ActualNetGeneration = (decimal)18.25, TotalHeatInput = (decimal)16.25, EmissionsRating = (decimal)0.34, Name = "two" };
            coalgeneratorList.Add(firstCoalGenerator);
            coalgeneratorList.Add(seconCoalGenerator);

            _generationReportIntputDetail = new GenerationReportIntputDetail { Coal = new GenerationReportCoal { CoalGenerator = coalgeneratorList } };
            _generationReportOutputDetail = new GenerationReportOutputDetail { ActualHeatRates = new GenerationOutputActualHeatRates { ActualHeatRates = new List<GenerationOutputActualHeatRatesActualHeatRate>() } };

            // Act
            _actualHeatRateValueHandler.CalculateActualHeatRateValue(_generationReportIntputDetail, _generationReportOutputDetail);

            //Assert
            Assert.AreEqual(2, _generationReportOutputDetail.ActualHeatRates.ActualHeatRates.Count);
            Assert.AreEqual("1", _generationReportOutputDetail.ActualHeatRates.ActualHeatRates[0].HeatRate);
            Assert.AreEqual("two", _generationReportOutputDetail.ActualHeatRates.ActualHeatRates[1].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CalculateActualHeatRateValueShouldThrowNullReferenceExceptionWhenCoalgeneratorDataNotAvailable()
        {
            // Arrange
            _generationReportIntputDetail = new GenerationReportIntputDetail { Coal = new GenerationReportCoal { CoalGenerator = null } };
            _generationReportOutputDetail = new GenerationReportOutputDetail { ActualHeatRates = new GenerationOutputActualHeatRates { ActualHeatRates = new List<GenerationOutputActualHeatRatesActualHeatRate>() } };

            // Act & Assert
            _actualHeatRateValueHandler.CalculateActualHeatRateValue(_generationReportIntputDetail, _generationReportOutputDetail);
        }
    }
}
