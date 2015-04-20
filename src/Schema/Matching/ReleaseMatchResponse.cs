using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;

namespace SevenDigital.Api.Schema.Matching
{
	[ApiEndpoint("release/match/byid")]
	[XmlRoot("matches")]
	[OAuthSigned]
	public class ReleaseMatchResponse
	{
		[XmlElement("releases")]
		public List<ReleasesMatch> Releases { get; set; }
	}

	public class ReleasesMatch
	{
		[XmlAttribute("mbId")]
		public string MbId { get; set; }

		[XmlAttribute("sevenDigitalId")]
		public string SevenDigitalId { get; set; }

		[XmlElement("release")]
		public List<ReleaseMatch> Release { get; set; }

		[XmlElement("matchError")]
		public MatchError MatchError { get; set; }
	}

	public class ReleaseMatch
	{
		[XmlElement("sevenDigitalId")]
		public string SevenDigitalId { get; set; }

		[XmlElement("mbId")]
		public string MbId { get; set; }
	}
}