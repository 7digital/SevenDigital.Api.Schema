using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Matching
{
	[Serializable]
	public class MatchError
	{
		[XmlAttribute("code")]
		public string Code { get; set; }
	}
}