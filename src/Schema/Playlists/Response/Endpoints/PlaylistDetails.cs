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
		public PlaylistDetails()
		{
			Tags = new List<Tag>();
			Annotations = new List<Annotation>();
		}

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

		[XmlArray("tags")]
		[XmlArrayItem("tag")]
		public List<Tag> Tags { get; set; }

		[XmlArray("annotations")]
		[XmlArrayItem("annotation")]
		public List<Annotation> Annotations { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Id, Name);
		}
	}
}