﻿using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Releases
{
	[Serializable]
	[XmlRoot("editorial")]
	[ApiEndpoint("release/editorial")]
	public class ReleaseEditorial : HasReleaseIdParameter
	{
		[XmlElement("review")]
		public TextItem Review { get; set; }

		[XmlElement("promotionalText")]
		public TextItem PromotionalText { get; set; }
	}

	[Serializable]
	public class TextItem
	{
		[XmlElement("text")]
		public string Text { get; set; }
	}
}
