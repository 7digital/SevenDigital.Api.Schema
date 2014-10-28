using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Users.Payments
{
	[ApiEndpoint("user/payment/card/select")]
	[OAuthSigned]
	[RequireSecure]
	[HttpPost]
	public class DefaultCard : HasCardIdParameter
	{
	}
}
