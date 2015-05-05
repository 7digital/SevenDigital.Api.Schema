using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;

namespace SevenDigital.Api.Schema.Matching
{
	[Serializable]
	[ApiEndpoint("track/match/byid")]
	[XmlRoot("matches")]
	[OAuthSigned]
	public class TrackMatchResponse
	{
		[XmlElement("tracks")]
		public List<TracksMatch> Tracks { get; set; }
	}

	[Serializable]
	public class TracksMatch
	{
		[XmlAttribute("mbId")]
		public string MbId { get; set; }

		[XmlAttribute("sevenDigitalId")]
		public string SevenDigitalId { get; set; }

		[XmlElement("track")]
		public List<TrackMatch> Track { get; set; }

		[XmlElement("matchError")]
		public MatchError MatchError { get; set; }
	}

	public class TrackMatch
	{
		[XmlElement("sevenDigitalId")]
		public string SevenDigitalId { get; set; }

		[XmlElement("mbId")]
		public string MbId { get; set; }
	}
}
