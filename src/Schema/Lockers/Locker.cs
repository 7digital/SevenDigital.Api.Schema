using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Lockers
{
	[Serializable]
	[ApiEndpoint("user/locker")]
	[XmlRoot("locker")]
	[OAuthSigned]
	public class Locker : IHasPaging, HasReleaseIdParameter, HasTrackIdParameter, HasPurchaseIdParameter, HasLockerSort, HasUsageTypesParameter
	{	
		[XmlElement("lockerReleases")]
		public LockerResponse Response { get; set; }
	}

	[Serializable]
	public class LockerResponse : HasPaging
	{
		[XmlElement("lockerRelease")]
		public List<LockerRelease> LockerReleases { get; set; }
	}
}