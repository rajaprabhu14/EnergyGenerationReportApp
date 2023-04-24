using EnergyReportGenerationApp.Entities.GenerationReportInput;
using EnergyReportGenerationApp.Entities.GenerationReportOutput;
using EnergyReportGenerationApp.Entities.ReferenceData;

namespace EnergyReportGenerationApp.Interfaces
{
    public interface ITotalGenerationValueHandler
    {
        void CalculateTotalGenerationValue(GenerationReportIntputDetail generationReportIntputDetail, GenerationReportOutputDetail generationReportOutputDetail, ReferenceDataDetail referenceDataDetail);
    }
}
