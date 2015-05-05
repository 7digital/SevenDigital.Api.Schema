using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Tracks;

namespace SevenDigital.Api.Schema.Charts
{
	[Serializable]
	public class TrackChartItem
	{
		[XmlElement("position")]
		public int Position { get; set; }

		[XmlElement("change")]
		public ChartItemChange Change { get; set; }

		[XmlElement("track")]
		public Track Track { get; set; }

		public override string ToString()
		{
			return string.Format("{0}, Track {1}", Position, Track);
		}
	}
}