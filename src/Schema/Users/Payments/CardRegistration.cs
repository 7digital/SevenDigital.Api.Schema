﻿using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.OAuth;
using SevenDigital.Api.Schema.Users.Payment;

namespace SevenDigital.Api.Schema.Users.Payments
{
	[ApiEndpoint("user/payment/cardregistration")]
	[OAuthSigned]
	[HttpPost]
	[XmlRoot("cardRegistration")]
	public class CardRegistration
	{
		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlAttribute("status")]
		public CardRegistrationStatus Status { get; set; }

		[XmlElement("redirectUrl")]
		public string RedirectUrl { get; set; }
	}
}
