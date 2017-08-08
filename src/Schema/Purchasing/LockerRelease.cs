using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Artists;
using SevenDigital.Api.Schema.Releases;

namespace SevenDigital.Api.Schema.Purchasing
{
	[Serializable]
	public class LockerRelease
	{
		[XmlElement("release")]
		public PurchasedRelease Release { get; set; }

		[XmlArray("lockerTracks")]
		[XmlArrayItem("lockerTrack")]
		public List<PurchasedLockerTrack> LockerTracks { get; set; }

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

	[Serializable]
	public class PurchasedRelease
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("version")]
		public string Version { get; set; }

		[XmlElement("type")]
		public ReleaseType Type { get; set; }

		[XmlElement("barcode")]
		public string Barcode { get; set; }

		[XmlElement("year")]
		public string Year { get; set; }

		[XmlElement("explicitContent")]
		public bool ExplicitContent { get; set; }

		[XmlElement("artist")]
		public Artist Artist { get; set; }

		[XmlElement("url")]
		public string Url { get; set; }

		[XmlElement("image")]
		public string Image { get; set; }

		[XmlElement("releaseDate")]
		public DateTime ReleaseDate { get; set; }

		[XmlElement("label")]
		public Label Label { get; set; }

		[XmlElement("licensor")]
		public Licensor Licensor { get; set; }

		[XmlElement("duration")]
		public int Duration { get; set; }

		[XmlElement("trackCount")]
		public int? TrackCount { get; set; }
		
		[XmlElement("slug")]
		public string Slug { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1} {2} {3}, Barcode {4}", Id, Title, Version, Type, Barcode);
		}
	}

	[Serializable]
	public class PurchasedLockerTrack
	{
		[XmlElement("track")]
		public PurchasedTrack Track { get; set; }

		[XmlElement("remainingDownloads")]
		public int RemainingDownloads { get; set; }

		[XmlElement("purchaseDate")]
		public DateTime PurchaseDate { get; set; }

		[XmlElement("purchaseId")]
		public int PurchaseId { get; set; }

		[XmlArray("downloadUrls")]
		[XmlArrayItem("downloadUrl")]
		public List<PurchasedDownloadUrl> DownloadUrls { get; set; }

		public override string ToString()
		{
			if (Track == null)
			{
				return "No track";
			}
			return Track.ToString();
		}
	}

	[Serializable]
	public class PurchasedTrack
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("version")]
		public string Version { get; set; }

		[XmlElement("artist")]
		public Artist Artist { get; set; }

		/// <summary>
		/// Track number or in cases where disc number is greater than 1, it is the discNumber + trackNumber (ie 203)
		/// </summary>
		/// <remarks>Will soon be decommissioned</remarks>
		[XmlElement("trackNumber")]
		public int TrackNumber { get; set; }

		[XmlElement("duration")]
		public int Duration { get; set; }

		[XmlElement("url")]
		public string Url { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1} {2}", Id, Title, Version);
		}
	}

	[Serializable]
	public class PurchasedDownloadUrl
	{
		[XmlElement("url")]
		public string Url { get; set; }

		[XmlElement("format")]
		public PurchasedFormat Format { get; set; }

		public override string ToString()
		{
			return string.Format("Download format: {0} at url : {1}", Format, Url);
		}
	}

	[Serializable]
	public class PurchasedFormat
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlElement("fileFormat")]
		public string FileFormat { get; set; }

		[XmlElement("bitRate")]
		public string BitRate { get; set; }

		[XmlElement("drmFree")]
		public bool DrmFree { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1} {2}", Id, FileFormat, BitRate);
		}
	}
}