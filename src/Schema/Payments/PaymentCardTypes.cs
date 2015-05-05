using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;

namespace SevenDigital.Api.Schema.Payments
{
	[Serializable]
	[ApiEndpoint("payment/card/type")]
	[XmlRoot("cardTypes")]
	public class PaymentCardTypes
	{
		[XmlElement("cardType")]
		public List<CardType> CardTypes { get; set; }
	}

	[Serializable]
	public class CardType
	{
		[XmlText]
		public string Type { get; set; }

		[XmlAttribute("id")]
		public string Id { get; set; }
	}
}
