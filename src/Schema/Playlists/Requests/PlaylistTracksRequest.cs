using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Playlists.Requests
{
	[Serializable]
	public class PlaylistTracksRequest
	{
		[XmlElement("track")]
		public List<Product> Tracks { get; set; }
	}
}