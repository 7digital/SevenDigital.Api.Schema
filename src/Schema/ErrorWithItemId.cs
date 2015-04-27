using System.Xml.Serialization;

namespace SevenDigital.Api.Schema
{
	public class ErrorWithItemId
	{
		[XmlAttribute("itemId")]
		public int ItemId { get; set; }

		[XmlAttribute("code")]
		public int Code { get; set; }

		[XmlElement("errorMessage")]
		public string ErrorMessage { get; set; }

		public override string ToString()
		{
			return string.Format("Item: {0}. Error {1}: {2}", ItemId,  Code, ErrorMessage);
		}
	}
}