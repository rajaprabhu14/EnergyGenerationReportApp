using System.Collections.Generic;
using EnergyReportGenarationApp.Infrastructure;
using EnergyReportGenerationApp.Entities.GenerationReportInput;
using EnergyReportGenerationApp.Entities.GenerationReportOutput;
using EnergyReportGenerationApp.Entities.ReferenceData;
using EnergyReportGenerationApp.Interfaces;

namespace EnergyReportGenerationApp.Handlers
{
    /// <summary>
    /// TotalGenerationValueHandler class 
    /// </summary>
    public class TotalGenerationValueHandler : ITotalGenerationValueHandler
    {
        private ReferenceDataDetail _referenceDataDetail;

        /// <summary>
        /// Method to calculate total generation value for all generators.
        /// </summary>
        /// <param name="generationReportIntputDetail"></param>
        /// <param name="generationReportOutputDetail"></param>
        /// <param name="referenceDataDetail"></param>
        public void CalculateTotalGenerationValue(GenerationReportIntputDetail generationReportIntputDetail, GenerationReportOutputDetail generationReportOutputDetail, ReferenceDataDetail referenceDataDetail)
        {
            _referenceDataDetail = referenceDataDetail;

            CalculateTotalGenerationvalue(generationReportIntputDetail.Wind.WindGenerator, generationReportOutputDetail);

            CalculateTotalGenerationvalue(generationReportIntputDetail.Gas.GasGenerator, generationReportOutputDetail);

            CalculateTotalGenerationvalue(generationReportIntputDetail.Coal.CoalGenerator, generationReportOutputDetail);

        }

        private void CalculateTotalGenerationvalue(IEnumerable<BaseGenerator> baseGenerator, GenerationReportOutputDetail generationReportOutputDetail)
        {
            decimal totalGenerationValue = default;

            foreach (BaseGenerator item in baseGenerator)
            {
                item.Generation.Day.ForEach(i =>
                {
                    totalGenerationValue = decimal.Add(totalGenerationValue, (decimal)(i.Energy * i.Price * GetValueFactor(item.Name)));
                });

                generationReportOutputDetail.Totals.Generator.Add(new GenerationOutputTotalsGenerator { Name = item.Name, Total = totalGenerationValue.ToString() });
            }

        }

        /// <summary>
        /// Method to get the value factor for each geneartor.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private decimal GetValueFactor(string name)
        {
            decimal valueFactor;
            switch (name)
            {
                case EnergyGeneratorNameType.WindGeneratorOffShoreName:
                    valueFactor = _referenceDataDetail.Factors.ValueFactor.High ?? default;
                    break;
                case EnergyGeneratorNameType.WindGeneratorOnShoreName:
                    valueFactor = _referenceDataDetail.Factors.ValueFactor.Low ?? default;
                    break;
                case EnergyGeneratorNameType.GasGeneratorName:
                case EnergyGeneratorNameType.CoalGeneratorName:
                    valueFactor = _referenceDataDetail.Factors.ValueFactor.Medium ?? default;
                    break;
                default:
                    valueFactor = default;
                    break;
            }

            return valueFactor;
        }
    }
}
