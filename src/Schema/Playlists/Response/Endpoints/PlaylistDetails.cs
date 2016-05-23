using System;
using System.Collections.Generic;
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
		private List<Tag> _tags = new List<Tag>();

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

		[XmlElement("image")]
		public string ImageUrl { get; set; }

		[XmlElement("tags")]
		public List<Tag> Tags
		{
			get { return _tags; }
			set { _tags = value; }
		}

		public override string ToString()
		{
			return string.Format("{0}: {1}", Id, Name);
		}
	}
}