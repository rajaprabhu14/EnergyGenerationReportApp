using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportInput
{
    public class GenerationReportCoalCoalGenerator : BaseGenerator
    {
        [XmlElement("TotalHeatInput")]
        public decimal? TotalHeatInput { get; set; }

        [XmlElement("ActualNetGeneration")]
        public decimal? ActualNetGeneration { get; set; }
    }
}
