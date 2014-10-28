using System.Xml.Serialization;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema
{
	[XmlRoot("recommendedItem")]
	public class RecommendedItem
	{
		[XmlElement("release")]
		public Release Release { get; set; }

		public override string ToString()
		{
			return Release != null ? Release.ToString() : string.Empty;
		}
	}
}