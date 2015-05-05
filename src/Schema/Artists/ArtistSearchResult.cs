using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Artists
{
	[Serializable]
	public class ArtistSearchResult
	{
		[XmlElement("type")]
		public ItemType Type { get; set; }

		[XmlElement("artist")]
		public Artist Artist { get; set; }

		[XmlElement("score")]
		public float Score { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Type, Artist);
		}
	}
}