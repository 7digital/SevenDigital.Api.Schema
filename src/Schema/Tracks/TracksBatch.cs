using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Tracks
{
	[Serializable]
	[ApiEndpoint("track/details/batch")]
	[XmlRoot("items")]
	public class TracksBatch : HasUsageTypesParameter
	{
		[XmlArray("tracks")]
		[XmlArrayItem("track")]
		public List<Track> Tracks { get; set; }

		[XmlArray("errors")]
		[XmlArrayItem("error")]
		public List<ErrorWithItemId> Errors { get; set; }

	}
}