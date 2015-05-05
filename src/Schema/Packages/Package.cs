using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Packages
{
	[Serializable]
	public class Package
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlElement("description")]
		public string Description { get; set; }

		[XmlElement("price")]
		public PackagePrice Price { get; set; }

		[XmlArray("formats")]
		[XmlArrayItem("format")]
		public List<PackageFormat> Formats { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Id, Description);
		}
	}
}