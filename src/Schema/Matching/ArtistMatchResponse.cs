using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;

namespace SevenDigital.Api.Schema.Matching
{
	[ApiEndpoint("artist/match/byid")]
	[XmlRoot("matches")]
	[OAuthSigned]
	public class ArtistMatchResponse
	{
		[XmlElement("artists")]
		public List<ArtistsMatch> Artists { get; set; }
	}

	public class ArtistsMatch
	{
		[XmlAttribute("mbId")]
		public string MbId { get; set; }

		[XmlAttribute("sevenDigitalId")]
		public string SevenDigitalId { get; set; }

		[XmlElement("artist")]
		public List<ArtistMatch> Artist { get; set; }

		[XmlElement("matchError")]
		public MatchError MatchError { get; set; }
	}

	public class ArtistMatch
	{
		[XmlElement("sevenDigitalId")]
		public string SevenDigitalId { get; set; }

		[XmlElement("mbId")]
		public string MbId { get; set; }
	}
}