using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SevenDigital.Api.Schema.Packages;
using SevenDigital.Api.Schema.Pricing;

namespace SevenDigital.Api.Schema.Legacy
{
	public static class PriceLegacyMapper
	{
		private static readonly Dictionary<string, string> regionMap;

		static PriceLegacyMapper()
		{
			regionMap = BuildRegionMap();
		}

		private static Dictionary<string, string> BuildRegionMap()
		{
			return CultureInfo
				.GetCultures(CultureTypes.AllCultures)
				.Where(c => !c.IsNeutralCulture)
				.Select(culture =>
				{
					try
					{
						return new RegionInfo(culture.LCID);
					}
					catch (ArgumentException)
					{
						return null;
					}
				})
				.Where(ri => ri != null)
				.GroupBy(ri => ri.ISOCurrencySymbol)
				.ToDictionary(x => x.Key, x => x.First().CurrencySymbol);
		}

		public static Price PrimaryPackagePrice(Download download)
		{
			var package = download.PrimaryPackage();
			if (package == null)
			{
				return null;
			}

			var packagePrice = package.Price;

			var isOnSale = (packagePrice.RecommendedRetailPrice != packagePrice.SevendigitalPrice);

			var currencySymbol = regionMap[packagePrice.CurrencyCode];

			var formattedRrp = FormatPriceWithSymbol(packagePrice.RecommendedRetailPrice, currencySymbol);
			var formattedPriceValue = FormatPriceWithSymbol(packagePrice.SevendigitalPrice, currencySymbol);

			var rrp = packagePrice.RecommendedRetailPrice != null
				? packagePrice.RecommendedRetailPrice.Value.ToString()
				: null;
			var priceValue = packagePrice.SevendigitalPrice != null
				? packagePrice.SevendigitalPrice.Value.ToString()
				: null;

			return new Price
			{
				Currency = new Currency
					{
						Code = packagePrice.CurrencyCode,
						Symbol = currencySymbol
					},
				Rrp = rrp,
				FormattedRrp = formattedRrp,
				Value = priceValue,
				FormattedPrice = formattedPriceValue,
				IsOnSale = isOnSale
			};
		}
		private static string FormatPriceWithSymbol(decimal? price, string currencySymbol)
		{
			if (!price.HasValue)
			{
				return "N/A";
			}

			var decimalValue = price.Value;
			switch (currencySymbol)
			{
				case "€":
					return String.Format("{0}{1}", decimalValue.ToString("##0.00"), currencySymbol).Replace(".", ",");

				case "SEK":
				case "NOK":
				case "DKK":
					return String.Format("{0} {1}", decimalValue.ToString("##0.00"), currencySymbol).Replace(".", ",");

				default:
					return String.Format("{0}{1}", currencySymbol, decimalValue.ToString("##0.00"));
			}
		}
	}
}