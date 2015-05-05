using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Charts
{
	[Serializable]
	public class ReleaseChartItem
	{
		[XmlElement("position")]
		public int Position { get; set; }

		[XmlElement("change")]
		public ChartItemChange Change { get; set; }

		[XmlElement("release")]
		public Release Release { get; set; }

		public override string ToString()
		{
			return string.Format("{0}, Release {1}", Position, Release);
		}
	}
}