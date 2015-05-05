using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;

namespace SevenDigital.Api.Schema.Users.Purchase
{
	[Serializable]
	[OAuthSigned]
	[ApiEndpoint("user/purchase/item")]
	[XmlRoot("purchase")]
	public class UserPurchaseItem : BasePurchaseItem
	{
	}
}