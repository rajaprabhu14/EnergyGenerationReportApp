using System.Collections.Generic;
using EnergyReportGenerationApp.Entities.GenerationReportInput;
using EnergyReportGenerationApp.Entities.GenerationReportOutput;
using EnergyReportGenerationApp.Entities.ReferenceData;
using EnergyReportGenerationApp.Interfaces;
using Topshelf.Logging;

namespace EnergyReportGenerationApp.Handlers
{
    /// <summary>
    /// EnergyDataProcessingHandler to calculate output report data. 
    /// </summary>
    public class EnergyDataProcessingHandler : IEnergyDataProcessingHandler
    {
        private readonly ITotalGenerationValueHandler _totalGenerationValueHandler;
        private readonly IActualHeatRateValueHandler _actualHeatRateValueHandler;
        private readonly IEmissionValueHandler _emissionValueHandler;
        private ReferenceDataDetail _referenceDataDetail;
        private readonly LogWriter logWriter;

        public EnergyDataProcessingHandler(ITotalGenerationValueHandler totalGenerationValueHandler, IActualHeatRateValueHandler actualHeatRateValueHandler, IEmissionValueHandler emissionValueHandler)
        {
            _totalGenerationValueHandler = totalGenerationValueHandler;
            _actualHeatRateValueHandler = actualHeatRateValueHandler;
            _emissionValueHandler = emissionValueHandler;
            logWriter = HostLogger.Get<ReportGenerationProcessor>();
        }
        public GenerationReportOutputDetail ProcessEnergyGenerationData(GenerationReportIntputDetail generationReportIntputDetail, ReferenceDataDetail referenceDataDetail)
        {
            logWriter.Info("EnergyDataProcessingHandler calculating output report data");

            _referenceDataDetail = referenceDataDetail;

            var generationReportOutputDetail = new GenerationReportOutputDetail { ActualHeatRates = new GenerationOutputActualHeatRates(), Totals = new GenerationOutputTotals(), MaxEmissionGenerators = new GenerationOutputMaxEmissionGenerators() };
            generationReportOutputDetail.Totals.Generator = new List<GenerationOutputTotalsGenerator>();
            generationReportOutputDetail.ActualHeatRates.ActualHeatRates = new List<GenerationOutputActualHeatRatesActualHeatRate>();
            generationReportOutputDetail.MaxEmissionGenerators.Day = new List<GenerationOutputMaxEmissionGeneratorsDay>();

            _totalGenerationValueHandler.CalculateTotalGenerationValue(generationReportIntputDetail, generationReportOutputDetail, _referenceDataDetail);

            _emissionValueHandler.CalculateEmissionValue(generationReportIntputDetail, generationReportOutputDetail, _referenceDataDetail);

            _actualHeatRateValueHandler.CalculateActualHeatRateValue(generationReportIntputDetail, generationReportOutputDetail);

            return generationReportOutputDetail;
        }

    }
}
