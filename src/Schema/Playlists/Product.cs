using System;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SevenDigital.Api.Schema.Playlists
{
	[Serializable]
	public class Product
	{
		[XmlElement("trackId")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string TrackId { get; set; }

		[XmlElement("trackTitle")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string TrackTitle { get; set; }

		[XmlElement("trackVersion")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string TrackVersion { get; set; }

		[XmlElement("artistId")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string ArtistId { get; set; }

		[XmlElement("artistAppearsAs")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string ArtistAppearsAs { get; set; }

		[XmlElement("releaseId")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string ReleaseId { get; set; }

		[XmlElement("releaseTitle")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string ReleaseTitle { get; set; }

		[XmlElement("releaseArtistId")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string ReleaseArtistId { get; set; }

		[XmlElement("releaseArtistAppearsAs")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string ReleaseArtistAppearsAs { get; set; }

		[XmlElement("releaseVersion")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string ReleaseVersion { get; set; }

		[XmlElement("source")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Source { get; set; }

		[XmlElement("audioUrl")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string AudioUrl { get; set; }

		[XmlElement("image")]
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string ImageUrl { get; set; }
	}
}