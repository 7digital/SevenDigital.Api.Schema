using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Tags
{
	[Serializable]
	public class Tag
	{
		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlElement("text")]
		public string Text { get; set; }

		[XmlElement("url")]
		public string Url { get; set; }

		[XmlElement("count")]
		public int Count { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Id, Text);
		}
	}
}