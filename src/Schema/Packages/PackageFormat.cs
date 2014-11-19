using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Packages
{
	public class PackageFormat
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlElement("description")]
		public string Description { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Id, Description);
		}
	}
}