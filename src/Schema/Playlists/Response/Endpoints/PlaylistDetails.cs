using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;

namespace SevenDigital.Api.Schema.Playlists.Response.Endpoints
{
	[Serializable]
	[OAuthSigned]
	[ApiEndpoint("playlists/{playlistId}/details")]
	[XmlRoot("playlist")]
	public class PlaylistDetails : UserBasedUpdatableItem, HasPlaylistIdParameter
	{
		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlElement("name")]
		public string Name { get; set; }

		[XmlElement("visibility")]
		public PlaylistVisibilityType Visibility { get; set; }

		[XmlElement("status")]
		public PlaylistStatusType Status { get; set; }

		[XmlElement("description")]
		public string Description { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Id, Name);
		}
	}
}