using System.Collections.Generic;
using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportOutput
{
    public class GenerationOutputTotals
    {
        [XmlElement("Generator")]
        public List<GenerationOutputTotalsGenerator> Generator { get; set; }
    }
}
