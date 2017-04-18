﻿using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Merchandising
{
	[Serializable]
	[ApiEndpoint("editorial/list")]
	[XmlRoot("list")]
	public class MerchandisingList : HasKeyParameter, HasUsageTypesParameter
	{
		[XmlElement("key")]
		public string Key { get; set; }

		[XmlArray("listItems")]
		[XmlArrayItem("listItem")]
		public List<ListItem> Items { get; set; }
	}
}
