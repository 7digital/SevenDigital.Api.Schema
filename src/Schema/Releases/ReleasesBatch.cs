using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;

namespace SevenDigital.Api.Schema.Releases
{
	[ApiEndpoint("release/details/batch")]
	[XmlRoot("items")]
	public class ReleasesBatch
	{
		[XmlArray("releases")]
		[XmlArrayItem("release")]
		public List<Release> Releases { get; set; }

		[XmlArray("errors")]
		[XmlArrayItem("error")]
		public List<ErrorWithItemId> Errors { get; set; }
	}
}
