using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Baskets
{
	public class Discount
	{
		[XmlElement("type")]
		public DiscountType Type { get; set; }
	}
}