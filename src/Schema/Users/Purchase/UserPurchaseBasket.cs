using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Users.Purchase
{
	[Serializable]
	[OAuthSigned]
	[ApiEndpoint("user/purchase/basket")]
	[XmlRoot("purchase")]
	public class UserPurchaseBasket : HasBasketParameter
	{
		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlElement("purchaseDate")]
		public DateTime PurchaseDate { get; set; }
	}
}