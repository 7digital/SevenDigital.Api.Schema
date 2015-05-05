using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Baskets
{
	[Serializable]
	public class Discount
	{
		[XmlElement("type")]
		public DiscountType Type { get; set; }
	}
}