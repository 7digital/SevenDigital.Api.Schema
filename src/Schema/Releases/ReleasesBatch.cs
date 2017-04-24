using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Releases
{
	[Serializable]
	[ApiEndpoint("release/details/batch")]
	[XmlRoot("items")]
	public class ReleasesBatch : HasUsageTypesParameter
	{
		[XmlArray("releases")]
		[XmlArrayItem("release")]
		public List<Release> Releases { get; set; }

		[XmlArray("errors")]
		[XmlArrayItem("error")]
		public List<ErrorWithItemId> Errors { get; set; }
	}
}
