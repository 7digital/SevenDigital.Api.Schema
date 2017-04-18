using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Artists
{
	[Serializable]
	[ApiEndpoint("artist/similar")]
	[XmlRoot("artists")]
	public class ArtistSimilar : HasPaging, HasArtistIdParameter, HasUsageTypesParameter
	{
		[XmlElement("artist")]
		public List<Artist> Artists { get; set; }
	}
}
