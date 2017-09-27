using Newtonsoft.Json;
using SevenDigital.Api.Schema.Playlists.Response.Endpoints;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Playlists.Response
{
	[Serializable]
	public class PlaylistLocation : UserBasedUpdatableItem
	{
		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlElement("name")]
		public string Name { get; set; }

		[XmlArray("links")]
		[XmlArrayItem("link")]
		public List<Link> Links { get; set; }

		[XmlElement("trackCount")]
		public int TrackCount { get; set; }

		[XmlElement("visibility")]
		public PlaylistVisibilityType Visibility { get; set; }

		[XmlElement("description")]
		public string Description { get; set; }

		[XmlElement("image")]
		public string Image { get; set; }

		[XmlIgnore]
		public PlaylistStatusType Status
		{
			get
			{
				PlaylistStatusType status;
				Enum.TryParse(CustomStatus, true, out status);
				return status;
			}
			set
			{
				CustomStatus = value.ToString();
			}
		}

		[XmlElement("status")]
		public string CustomStatus { get; set;}

		[XmlArray("tags")]
		[XmlArrayItem("tag")]
		public List<Tag> Tags { get; set; }

		[XmlArray("annotations")]
		[XmlArrayItem("annotation")]
		[JsonConverter(typeof(AnnotationsJsonConverter))]
		public List<Annotation> Annotations { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Id, Name);
		}
	}
}