using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Playlists.Requests
{
	[Serializable]
	[XmlRoot("playlist")]
	public class PlaylistRequest : PlaylistDetailsRequest
	{
		[XmlElement("tracks")]
		public List<Product> Tracks { get; set; }
	}
}