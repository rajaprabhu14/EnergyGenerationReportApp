using System;
using System.Xml.Serialization;

namespace EnergyReportGenerationApp.Entities.ReferenceData
{
    [XmlType(AnonymousType = true)]
    [Serializable]
    [XmlRoot("ReferenceData")]
    public class ReferenceDataDetail
    {
        [XmlElement("Factors")]
        public ReferenceDataFactors Factors;
    }
}
