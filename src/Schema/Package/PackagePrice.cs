using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Package
{
	public class PackagePrice
	{
		[XmlElement("currencyCode")]
		public string CurrencyCode { get; set; }

		[XmlElement("sevendigitalPrice", IsNullable = true)]
		public decimal? SevendigitalPrice { get; set; }

		[XmlElement("recommendedRetailPrice", IsNullable = true)]
		public decimal? RecommendedRetailPrice { get; set; }
	}
}