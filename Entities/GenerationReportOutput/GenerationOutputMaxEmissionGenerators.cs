using System.Collections.Generic;
using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportOutput
{
    public class GenerationOutputMaxEmissionGenerators
    {
        [XmlElement("Day")]
        public List<GenerationOutputMaxEmissionGeneratorsDay> Day { get; set; }
    }
}
