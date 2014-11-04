using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Lockers
{
	[XmlRoot("lockerRelease")]
	public class LockerRelease
	{
		[XmlElement("release")]
		public Release Release { get; set; }

		[XmlArray("lockerTracks")]
		[XmlArrayItem("lockerTrack")]
		public List<LockerTrack> LockerTracks { get; set; }

		[XmlElement("available")]
		public bool Available { get; set; }

		public override string ToString()
		{
			if (Release == null)
			{
				return "No release";
			}
			return Release.ToString();
		}
	}
}