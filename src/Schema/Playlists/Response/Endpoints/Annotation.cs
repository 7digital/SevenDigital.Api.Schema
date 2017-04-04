using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Playlists.Response.Endpoints
{
	[Serializable]
	[XmlRoot("annotation")]
	public class Annotation
	{
		[XmlAttribute("key")]
		public string Key {get; set; }

		[XmlText]
		public string Value { get; set; }
	}
}