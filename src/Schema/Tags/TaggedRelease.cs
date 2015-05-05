using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Tags
{
	[Serializable]
	public class TaggedRelease
	{
		[XmlElement("release")]
		public Release Release { get; set; }

		public override string ToString()
		{
			return Release != null ? Release.ToString() : string.Empty;
		}
	}
}