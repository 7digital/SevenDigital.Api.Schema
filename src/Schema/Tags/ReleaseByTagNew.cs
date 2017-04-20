using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Tags
{
	[Serializable]
	[ApiEndpoint("release/bytag/new")]
	[XmlRoot("taggedResults")]
	public class ReleaseByTagNew : HasPaging, HasTags, HasUsageTypesParameter
	{
		[XmlElement("type")]
		public ItemType Type { get; set; }

		[XmlElement("taggedItem")]
		public List<TaggedRelease> TaggedReleases { get; set; }
	}
}