using System.Collections.Generic;
using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportOutput
{
    public class GenerationOutputActualHeatRates
    {
        [XmlElement("ActualHeatRate")]
        public List<GenerationOutputActualHeatRatesActualHeatRate> ActualHeatRates { get; set; }
    }
}
