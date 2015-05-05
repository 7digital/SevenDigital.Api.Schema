using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;

namespace SevenDigital.Api.Schema.Territories
{
	[Serializable]
	[ApiEndpoint("countries")]
	[XmlRoot("countries")]
	public class Countries
	{
		[XmlElement("country")]
		public List<Country> CountryItems { get; set; }
	}
}
