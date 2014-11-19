using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Baskets
{
	public class AmountDue
	{
		[XmlElement("amount")]
		public string Amount { get; set; }

		[XmlElement("formattedAmount")]
		public string FormattedAmount { get; set; }
	}
}