using System;
using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.GenerationReportOutput
{
    [XmlType(AnonymousType = true)]
    [Serializable]
    [XmlRoot("GenerationOutput")]
    public class GenerationReportOutputDetail
    {
        [XmlElement("Totals")]
        public GenerationOutputTotals Totals { get; set; }

        [XmlElement("MaxEmissionGenerators")]
        public GenerationOutputMaxEmissionGenerators MaxEmissionGenerators { get; set; }

        [XmlElement("ActualHeatRates")]
        public GenerationOutputActualHeatRates ActualHeatRates { get; set; }
    }
}
