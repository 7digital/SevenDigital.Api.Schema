using System.Xml.Serialization;
using SevenDigital.Api.Schema.Media;

namespace SevenDigital.Api.Schema.Lockers
{
	[XmlRoot("downloadUrl")]
	public class DownloadUrl
	{
		[XmlElement("url")]
		public string Url { get; set; }

		[XmlElement("format")]
		public Format Format { get; set; }

		public override string ToString()
		{
			return string.Format("Download format: {0} at url : {1}", Format, Url);
		}
	}
}