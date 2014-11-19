using System.Collections.Generic;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Baskets
{
	public class BasketItemList
	{
		[XmlElement("basketItem")]
		public List<BasketItem> Items { get; set; }
	}
}