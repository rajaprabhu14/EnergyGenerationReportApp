using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.ReferenceData
{
    public class ReferenceDataFactorsValueFactor
    {
        [XmlElement("High")]
        public decimal? High { get; set; }

        [XmlElement("Medium")]
        public decimal? Medium { get; set; }

        [XmlElement("Low")]
        public decimal? Low { get; set; }
    }
}
