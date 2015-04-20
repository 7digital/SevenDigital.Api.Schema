using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Matching
{
	public class MatchError
	{
		[XmlAttribute("code")]
		public string Code { get; set; }
	}
}