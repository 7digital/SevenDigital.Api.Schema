using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Artists
{
	[ApiEndpoint("artist/releases")]
	[XmlRoot("releases")]
	public class ArtistReleases : HasPaging, HasArtistIdParameter, HasReleaseTypeParameter
	{
		[XmlElement("release")]
		public List<Release> Releases { get; set; }
	}
}