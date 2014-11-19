using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Baskets
{
	public class BasketPackage
	{
		[XmlAttribute("id")]
		public int Id { get; set; }
	}
}