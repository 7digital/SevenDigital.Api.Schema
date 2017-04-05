using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Playlists
{
	[Serializable]
	public class Product
	{
		[XmlElement("trackId")]
		public string TrackId { get; set; }

		[XmlElement("trackTitle")]
		public string TrackTitle { get; set; }

		[XmlElement("trackVersion")]
		public string TrackVersion { get; set; }

		[XmlElement("artistId")]
		public string ArtistId { get; set; }

		[XmlElement("artistAppearsAs")]
		public string ArtistAppearsAs { get; set; }

		[XmlElement("releaseId")]
		public string ReleaseId { get; set; }

		[XmlElement("releaseTitle")]
		public string ReleaseTitle { get; set; }

		[XmlElement("releaseArtistId")]
		public string ReleaseArtistId { get; set; }

		[XmlElement("releaseArtistAppearsAs")]
		public string ReleaseArtistAppearsAs { get; set; }

		[XmlElement("releaseVersion")]
		public string ReleaseVersion { get; set; }

		[XmlElement("source")]
		public string Source { get; set; }

		[XmlElement("audioUrl")]
		public string AudioUrl { get; set; }

		[XmlElement("image")]
		public string ImageUrl { get; set; }
	}
}