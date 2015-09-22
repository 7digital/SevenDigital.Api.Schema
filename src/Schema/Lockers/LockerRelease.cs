using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Lockers
{
	[Serializable]
	public class LockerRelease
	{
		[XmlElement("release")]
		public ReleaseInLocker Release { get; set; }

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