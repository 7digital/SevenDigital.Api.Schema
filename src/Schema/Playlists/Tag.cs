using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Playlists
{
	[Serializable]
	public class Tag
	{
		[XmlElement("name")]
		public string Name { get; set; }

		[XmlElement("playlistPosition")]
		public int? PlaylistPosition { get; set; }

		public Tag()
		{
		}

		public Tag(string name)
		{
			Name = name;
		}
	}
}