using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportInput
{
    public class BaseGenerator
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Generation")]
        public Generation Generation { get; set; }

        [XmlElement("EmissionsRating")]
        public decimal? EmissionsRating { get; set; }
    }
}
