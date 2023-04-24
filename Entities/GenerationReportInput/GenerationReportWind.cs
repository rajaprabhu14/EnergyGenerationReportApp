using System.Collections.Generic;
using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportInput
{
    public class GenerationReportWind
    {
        [XmlElement("WindGenerator")]
        public List<GenerationReportWindWindGenerator> WindGenerator { get; set; }

    }
}
