using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Users
{
	[Serializable]
	[OAuthSigned]
	[ApiEndpoint("user/deliveritem")]
	[XmlRoot("purchase")]
	public class UserDeliverItem : HasReleaseIdParameter, HasTrackIdParameter, HasUserDeliverItemParameter
	{
		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlElement("purchaseDate")]
		public DateTime PurchaseDate { get; set; }
	}
}