using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Artists;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.Packages;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Releases
{
	[Serializable]
	[XmlRoot("release")]
	[ApiEndpoint("release/details")]
	public class Release : HasReleaseIdParameter, HasUsageTypesParameter
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("version")]
		public string Version { get; set; }

		[XmlElement("type")]
		public ReleaseType Type { get; set; }

		[XmlElement("barcode")]
		public string Barcode { get; set; }

		[XmlElement("year")]
		public string Year { get; set; }

		[XmlElement("explicitContent")]
		public bool ExplicitContent { get; set; }

		[XmlElement("artist")]
		public Artist Artist { get; set; }

		[XmlElement("image")]
		public string Image { get; set; }

		[XmlElement("label")]
		public Label Label { get; set; }

		[XmlElement("licensor")]
		public Licensor Licensor { get; set; }

		[XmlElement("duration")]
		public int Duration { get; set; }

		[XmlElement("trackCount")]
		public int? TrackCount { get; set; }

		[XmlElement("download")]
		public Download Download { get; set; }

		[XmlElement("subscriptionStreaming")]
		public Streaming SubscriptionStreaming { get; set; }

		[XmlElement("adSupportedStreaming")]
		public Streaming AdSupportedStreaming { get; set; }

		[XmlElement("slug")]
		public string Slug { get; set; }

		[XmlElement("cline")]
		public string Cline { get; set; }

		[XmlElement("pline")]
		public string Pline { get; set; }


		public override string ToString()
		{
			return string.Format("{0}: {1} {2} {3}, Barcode {4}", Id, Title, Version, Type, Barcode);
		}
	}
}