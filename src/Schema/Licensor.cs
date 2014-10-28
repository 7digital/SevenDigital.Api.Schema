using System.Xml.Serialization;

namespace SevenDigital.Api.Schema
{
	[XmlRoot("licensor")]
	public class Licensor
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlElement("name")]
		public string Name { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Id, Name);
		}
	}
}