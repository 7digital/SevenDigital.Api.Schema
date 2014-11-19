using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Packages
{
	public class PackagePrice
	{
		[XmlElement("currencyCode")]
		public string CurrencyCode { get; set; }

		[XmlElement("sevendigitalPrice", IsNullable = true)]
		public decimal? SevendigitalPrice { get; set; }

		[XmlElement("recommendedRetailPrice", IsNullable = true)]
		public decimal? RecommendedRetailPrice { get; set; }

		public override string ToString()
		{
			return string.Format("{0} {1}", CurrencyCode, RecommendedRetailPrice);
		}
	}
}