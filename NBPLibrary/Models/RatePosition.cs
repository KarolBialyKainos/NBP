using System.Globalization;
using System.Xml.Serialization;

namespace NBPLibrary.Models
{
    [XmlType("pozycja")]
    public class RatePosition
    {
        [XmlElement("nazwa_waluty")]
        public string CurrencyName { set; get; }
        [XmlElement("kod_waluty")]
        public string CurrencyCode { set; get; }

        [XmlIgnore]
        public double AverageRate { set; get; }

        [XmlElement("kurs_sredni")]
        public string AverageRateString {
            set{
                if (value != null)
                {
                    if (value.Contains(","))
                    {
                        value = value.Replace(",", ".");
                    }
                }
                double tmpDouble = double.Parse(value, CultureInfo.InvariantCulture);
                AverageRate = tmpDouble;
            }
            get{
                return AverageRate.ToString();
            }
        }
        [XmlElement("przelicznik")]
        public int CalculationFactor { set; get; }
    }
}
