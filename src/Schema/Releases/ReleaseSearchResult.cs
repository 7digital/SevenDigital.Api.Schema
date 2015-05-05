using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Releases
{
	[Serializable]
	public class ReleaseSearchResult
	{
		[XmlElement("type")]
		public ItemType Type { get; set; }

		[XmlElement("release")]
		public Release Release { get; set; }

		[XmlElement("score")]
		public float Score { get; set; }
	}
}