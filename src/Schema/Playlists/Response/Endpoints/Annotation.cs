using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Playlists.Response.Endpoints
{
	[Serializable]
	[XmlRoot("annotation")]
	public class Annotation
	{
		public Annotation()
		{}

		public Annotation(string key, string value)
		{
			Key = key;
			Value = value;
		}

		[XmlAttribute("key")]
		public string Key {get; set; }

		[XmlText]
		public string Value { get; set; }
	}
}