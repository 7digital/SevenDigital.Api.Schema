using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;

namespace SevenDigital.Api.Schema.Playlists.Response.Endpoints
{
	[Serializable]
	[OAuthSigned]
	[ApiEndpoint("playlists/tags")]
	[XmlRoot("tags")]
	public class PlaylistTags
	{
		[XmlElement("tag")]
		public List<PlaylistTag> Tags { get; set; }
	}
}