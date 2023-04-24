using System.Collections.Generic;
using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportInput
{
    public class Generation
    {
        [XmlElement("Day")]
        public List<GenerationDay> Day { get; set; }
    }
}
