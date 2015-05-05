using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Merchandising
{
	[Serializable]
	public class ListItem
	{
		[XmlElement("type")]
		public string Type{ get; set; }

		[XmlElement("release")]
		public Release Release{ get; set; }
	}
}
