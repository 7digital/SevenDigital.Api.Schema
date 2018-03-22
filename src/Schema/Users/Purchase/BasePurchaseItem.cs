using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Users.Purchase
{
	public abstract class BasePurchaseItem : HasReleaseIdParameter, HasPriceParameter, HasTrackIdParameter, HasBasketParameter
	{
		[XmlElement("purchaseDate")]
		public DateTime PurchaseDate { get; set; }
	}
}