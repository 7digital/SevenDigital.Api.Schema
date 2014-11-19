using System.Collections.Generic;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Packages
{
	[XmlRoot("packages")]
	public class PackageList
	{
		[XmlArray("packages")]
		[XmlArrayItem("package")]
		public List<Package> Packages { get; set; }
	}
}