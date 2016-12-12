using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;

namespace SevenDigital.Api.Schema.Playlists.Response.Endpoints
{
	[HttpPost]
	[Serializable]
	[OAuthSigned]
	[ApiEndpoint("playlists/tags")]
	[XmlRoot("tagsUpdate")]
	public class TagsUpdate
	{
		[XmlElement("shopId")]
		public int ShopId { get; set; }

		[XmlArray("tags")]
		[XmlArrayItem("tag")]
		public List<PlaylistTag> Tags { get; set; }
	}
}
