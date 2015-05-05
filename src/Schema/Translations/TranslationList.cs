﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;

namespace SevenDigital.Api.Schema.Translations
{
	[Serializable]
	[ApiEndpoint("translations")]
	[XmlRoot("translations")]
	public class TranslationList : HasPaging
	{
		[XmlElement("translation")]
		public List<Translation> TranslationItems { get; set; }
	}

	[Serializable]
	public class Translation
	{
		[XmlElement("key")]
		public string Key { get; set; }

		[XmlElement("translatedText")]
		public string TranslatedText { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Key, TranslatedText);
		}
	}
}
