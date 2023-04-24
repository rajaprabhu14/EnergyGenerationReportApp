using EnergyReportGenerationApp.Entities.GenerationReportInput;
using EnergyReportGenerationApp.Entities.GenerationReportOutput;
using EnergyReportGenerationApp.Entities.ReferenceData;

namespace EnergyReportGenerationApp.Interfaces
{
    public interface IEnergyDataProcessingHandler
    {
        GenerationReportOutputDetail ProcessEnergyGenerationData(GenerationReportIntputDetail generationReportIntputDetail, ReferenceDataDetail referenceDataDetail);
    }
}
