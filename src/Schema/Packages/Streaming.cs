using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Packages
{
	public class Streaming
	{
		[XmlElement("releaseDate")]
		public DateTime ReleaseDate { get; set; }
	}
}