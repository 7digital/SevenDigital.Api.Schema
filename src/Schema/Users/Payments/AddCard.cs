using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;
using SevenDigital.Api.Schema.ParameterDefinitions.Post;
using SevenDigital.Api.Schema.Users.Payment;

namespace SevenDigital.Api.Schema.Users.Payments
{
	[ApiEndpoint("user/payment/card/add")]
	[OAuthSigned]
	[RequireSecure]
	[HttpPost]
	[XmlRoot("card")]
	public class AddCard : Card, HasAddCardParameter
	{
	}
}
