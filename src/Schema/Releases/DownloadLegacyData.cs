using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SevenDigital.Api.Schema.Media;
using SevenDigital.Api.Schema.Packages;
using SevenDigital.Api.Schema.Pricing;

namespace SevenDigital.Api.Schema.Releases
{
	public static class DownloadLegacyData
	{
		private static Dictionary<string, string> currencyMap;

		static DownloadLegacyData()
		{
			currencyMap = BuildCurrencySymbolMap();
		}

		private static Dictionary<string, string> BuildCurrencySymbolMap()
		{
			return CultureInfo
				.GetCultures(CultureTypes.AllCultures)
				.Where(c => !c.IsNeutralCulture)
				.Select(culture => {
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

			var rrp = packagePrice.RecommendedRetailPrice != null
				? packagePrice.RecommendedRetailPrice.Value.ToString() : string.Empty;
			var priceValue = packagePrice.SevendigitalPrice != null 
				? packagePrice.SevendigitalPrice.Value.ToString() : string.Empty;


			return new Price
			{
				Currency = new Currency
				{
					Code = packagePrice.CurrencyCode,
					Symbol = currencyMap[packagePrice.CurrencyCode]
				},
				Rrp = rrp,
				Value = priceValue
			};
		}

		public static FormatList PrimaryPackageFormats(Download download)
		{
			var package = download.PrimaryPackage();
			if (package == null)
			{
				return null;
			}

			return new FormatList
			{
				AvailableDrmFree = true,
				Formats = package.Formats.Select(ConvertFormat).ToList()
			};
		}

		private static Format ConvertFormat(PackageFormat packageFormat)
		{
			var desc = packageFormat.Description;
			var fileFormat = desc;
			var bitRate = string.Empty;

			var spaceIndex = desc.IndexOf(' ');
			if (spaceIndex > 0)
			{
				fileFormat = desc.Substring(0, spaceIndex);
				bitRate = desc.Substring(spaceIndex + 1);
			}

			return new Format
			{
				Id = packageFormat.Id,
				DrmFree = true,
				FileFormat = fileFormat,
				BitRate = bitRate
			};
		}

	}
}