using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Tracks
{
	[Serializable]
	public class TrackSearchResult
	{
		[XmlElement("type")]
		public TrackType Type { get; set; }

		[XmlElement("track")]
		public Track Track { get; set; }

		[XmlElement("score")]
		public float Score { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Type, Track);
		}
	}
}