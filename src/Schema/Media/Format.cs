using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Media
{
	[Serializable]
	public class Format
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlElement("fileFormat")]
		public string FileFormat { get; set; }

		[XmlElement("bitRate")]
		public string BitRate { get; set; }

		[XmlElement("drmFree")]
		public bool DrmFree { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1} {2}", Id, FileFormat, BitRate);
		}
	}
}