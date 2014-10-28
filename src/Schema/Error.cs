using System.Xml.Serialization;

namespace SevenDigital.Api.Schema
{
	[XmlRoot("error")]
	public class Error
	{
		[XmlAttribute("code")]
		public int Code { get; set; }

		[XmlElement("errorMessage")]
		public string ErrorMessage { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Code, ErrorMessage);
		}
	}
}