using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Artists;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Lockers
{
	[Serializable]
	public class ReleaseInLocker
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("version")]
		public string Version { get; set; }

		[XmlElement("type")]
		public ReleaseType Type { get; set; }

		[XmlElement("barcode")]
		public string Barcode { get; set; }

		[XmlElement("artist")]
		public Artist Artist { get; set; }

		[XmlElement("url")]
		public string Url { get; set; }

		[XmlElement("image")]
		public string Image { get; set; }

		[XmlElement("releaseDate")]
		public DateTime ReleaseDate { get; set; }

		[XmlElement("licensor")]
		public Licensor Licensor { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1} {2} {3}, Barcode {4}", Id, Title, Version, Type, Barcode);
		}
	}
}