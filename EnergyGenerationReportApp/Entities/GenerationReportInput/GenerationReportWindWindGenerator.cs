using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportInput
{
    public class GenerationReportWindWindGenerator : BaseGenerator
    {
        [XmlElement("Location")]
        public string Location { get; set; }
    }
}
