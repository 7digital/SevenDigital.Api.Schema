using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Baskets
{
	[Serializable]
	public class BasketPackage
	{
		[XmlAttribute("id")]
		public int Id { get; set; }
	}
}