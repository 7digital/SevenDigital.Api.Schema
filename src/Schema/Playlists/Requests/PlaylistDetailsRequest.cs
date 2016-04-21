using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Playlists.Requests
{
	[Serializable]
	[XmlRoot("playlist")]
	public class PlaylistDetailsRequest
	{
		[XmlElement("name")]
		public string Name { get; set; }

		[XmlElement("visibility")]
		public PlaylistVisibilityType Visibility { get; set; }

		[XmlElement("status")]
		public PlaylistStatusType Status { get; set; }

		[XmlElement("description")]
		public string Description { get; set; }
	}
}