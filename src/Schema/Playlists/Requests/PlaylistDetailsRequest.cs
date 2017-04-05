using SevenDigital.Api.Schema.Playlists.Response.Endpoints;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SevenDigital.Api.Schema.Playlists.Requests
{
	[Serializable]
	[XmlRoot("playlist")]
	public class PlaylistDetailsRequest
	{
		public PlaylistDetailsRequest()
		{
			Tags = new List<Tag>();
			Annotations = new List<Annotation>();
		}

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
		[JsonConverter(typeof(AnnotationsJsonConverter))]
		public List<Annotation> Annotations { get; set; }
	}
}