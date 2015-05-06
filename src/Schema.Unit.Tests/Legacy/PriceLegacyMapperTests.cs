using System.Collections.Generic;
using NUnit.Framework;
using SevenDigital.Api.Schema.Legacy;
using SevenDigital.Api.Schema.Packages;
using SevenDigital.Api.Schema.Pricing;

namespace SevenDigital.Api.Schema.Unit.Tests.Legacy
{
	[TestFixture]
	public class PriceLegacyMapperTests
	{
		[Test]
		public void ShouldMapCurrencyData()
		{
			var download = BuildDownload();

			var converted = PriceLegacyMapper.PrimaryPackagePrice(download);

			Assert.That(converted.Currency, Is.Not.Null);
			Assert.That(converted.Currency.Code, Is.EqualTo("GBP"));
			Assert.That(converted.Currency.Symbol, Is.EqualTo("£"));
		}

		[Test]
		public void ShouldMapPriceData()
		{
			var download = BuildDownload();

			var converted = PriceLegacyMapper.PrimaryPackagePrice(download);

			Assert.That(converted.IsOnSale, Is.True);
			Assert.That(converted.Rrp, Is.EqualTo("10.00"));
			Assert.That(converted.Value, Is.EqualTo("9.75"));
			Assert.That(converted.Status, Is.EqualTo(PriceStatus.Available));
		}

		[Test]
		public void ShouldMapPriceDataWhenNotOnSale()
		{
			var download = BuildDownload();
			download.Packages[0].Price.RecommendedRetailPrice = 11.01m;
			download.Packages[0].Price.SevendigitalPrice = 11.01m;

			var converted = PriceLegacyMapper.PrimaryPackagePrice(download);

			Assert.That(converted.IsOnSale, Is.False);
			Assert.That(converted.Rrp, Is.EqualTo("11.01"));
			Assert.That(converted.FormattedRrp, Is.EqualTo("£11.01"));
			Assert.That(converted.Value, Is.EqualTo("11.01"));
			Assert.That(converted.FormattedPrice, Is.EqualTo("£11.01"));
			Assert.That(converted.Status, Is.EqualTo(PriceStatus.Available));
		}

		[Test]
		public void ShouldFormatPriceData()
		{
			var download = BuildDownload();

			var converted = PriceLegacyMapper.PrimaryPackagePrice(download);

			Assert.That(converted.IsOnSale, Is.True);
			Assert.That(converted.FormattedRrp, Is.EqualTo("£10.00"));
			Assert.That(converted.FormattedPrice, Is.EqualTo("£9.75"));
		}

		[Test]
		public void ShouldMapWithZeroPrices()
		{
			var download = BuildDownload();
			download.Packages[0].Price.RecommendedRetailPrice = 0m;
			download.Packages[0].Price.SevendigitalPrice = 0m;

			var converted = PriceLegacyMapper.PrimaryPackagePrice(download);

			Assert.That(converted.IsOnSale, Is.False);
			Assert.That(converted.Rrp, Is.EqualTo("0"));
			Assert.That(converted.FormattedRrp, Is.EqualTo("£0.00"));
			Assert.That(converted.Value, Is.EqualTo("0"));
			Assert.That(converted.FormattedPrice, Is.EqualTo("£0.00"));
			Assert.That(converted.Status, Is.EqualTo(PriceStatus.Free));
		}

		[Test]
		public void ShouldMapWithoutPrices()
		{
			var download = BuildDownload();
			download.Packages[0].Price.RecommendedRetailPrice = null;
			download.Packages[0].Price.SevendigitalPrice = null;

			var converted = PriceLegacyMapper.PrimaryPackagePrice(download);

			Assert.That(converted.IsOnSale, Is.False);
			Assert.That(converted.Rrp, Is.Null);
			Assert.That(converted.FormattedRrp, Is.EqualTo("N/A"));
			Assert.That(converted.Value, Is.Null);
			Assert.That(converted.FormattedPrice, Is.EqualTo("N/A"));
			Assert.That(converted.Status, Is.EqualTo(PriceStatus.UnAvailable));
		}

		private static Download BuildDownload()
		{
			var price = new PackagePrice
			{
				CurrencyCode = "GBP",
				RecommendedRetailPrice = 10.00m,
				SevendigitalPrice = 9.75m
			};

			var download = new Download
			{
				Packages = new List<Package>
				{
					new Package
					{
						Id = 2,
						Price = price
					}
				}
			};
			return download;
		}
	}
}
