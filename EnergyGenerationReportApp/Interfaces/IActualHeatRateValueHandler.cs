using EnergyReportGenerationApp.Entities.GenerationReportInput;
using EnergyReportGenerationApp.Entities.GenerationReportOutput;

namespace EnergyReportGenerationApp.Interfaces
{
    public interface IActualHeatRateValueHandler
    {
        void CalculateActualHeatRateValue(GenerationReportIntputDetail generationReportIntputDetail, GenerationReportOutputDetail generationReportOutputDetail);
    }
}
