using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Tags
{
	[Serializable]
	[ApiEndpoint("release/bytag/top")]
	[XmlRoot("taggedResults")]
	public class ReleaseByTagTop : HasPaging , HasTags, HasUsageTypesParameter
	{
		[XmlElement("type")]
		public ItemType Type { get; set; }

		[XmlElement("taggedItem")]
		public List<TaggedRelease> TaggedReleases { get; set; }
	}
}