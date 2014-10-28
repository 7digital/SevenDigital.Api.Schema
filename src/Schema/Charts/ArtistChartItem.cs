using System.Xml.Serialization;
using SevenDigital.Api.Schema.Artists;

namespace SevenDigital.Api.Schema.Charts
{
	[XmlRoot("chartItem")]
	public class ArtistChartItem
	{
		[XmlElement("position")]
		public int Position { get; set; }

		[XmlElement("change")]
		public ChartItemChange Change { get; set; }

		[XmlElement("artist")]
		public Artist Artist { get; set; }

		public override string ToString()
		{
			return string.Format("{0}, Artist {1}", Position, Artist);
		}
	}
}