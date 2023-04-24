using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.ReferenceData
{
    public class ReferenceDataFactors
    {
        [XmlElement("ValueFactor")]
        public ReferenceDataFactorsValueFactor ValueFactor { get; set; }

        [XmlElement("EmissionsFactor")]
        public ReferenceDataFactorsEmissionsFactor EmissionsFactor { get; set; }
    }
}
