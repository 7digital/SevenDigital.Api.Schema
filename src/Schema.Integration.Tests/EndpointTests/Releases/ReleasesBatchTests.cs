﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.Packages;
using SevenDigital.Api.Schema.Releases;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Releases
{
	[TestFixture]
	public class ReleaseBatchTests
	{
		private readonly IApi _api = new ApiConnection();

		private const int FirstId = 12345;
		private const int SecondId = 12346;

		[Test]
		public async Task Should_return_releases_from_api()
		{
			var ids = new List<int> { FirstId, SecondId };
			var request = _api.Create<ReleasesBatch>()
				.WithParameter("releaseids", ids)
				.ForShop(34);

			var response = await request.Please();

			Assert.That(response, Is.Not.Null);
			Assert.That(response.Releases, Is.Not.Null);

			var releases = response.Releases;

			Assert.That(releases.Count, Is.EqualTo(2));
			Assert.That(releases[0].Id, Is.EqualTo(FirstId));
			Assert.That(releases[1].Id, Is.EqualTo(SecondId));
		}

		[Test]
		public async Task Should_return_releases_in_same_order_as_request()
		{
			var ids = new List<int> { SecondId, FirstId };
			var request = _api.Create<ReleasesBatch>()
				.WithParameter("releaseids", ids)
				.ForShop(34);

			var response = await request.Please();
			var releases = response.Releases;

			Assert.That(releases.Count, Is.EqualTo(2));
			Assert.That(releases[0].Id, Is.EqualTo(SecondId));
			Assert.That(releases[1].Id, Is.EqualTo(FirstId));
		}

		[Test]
		public async Task Should_return_releases_and_errors_when_showErrors_flag_is_specified()
		{
			var ids = new List<int> { FirstId, SecondId, -1, -2 };
			var request = _api.Create<ReleasesBatch>()
				.WithParameter("releaseids", ids)
				.WithParameter("showErrors", "true")
				.ForShop(34);

			var response = await request.Please();

			Assert.That(response, Is.Not.Null);

			Assert.That(response.Releases, Is.Not.Null);
			Assert.That(response.Releases.Count, Is.EqualTo(2));

			Assert.That(response.Errors, Is.Not.Null);
			Assert.That(response.Errors.Count, Is.EqualTo(2));
		}

		[Test]
		public async Task Should_return_error_details()
		{
			var ids = new List<int> { -1 };
			var request = _api.Create<ReleasesBatch>()
				.WithParameter("releaseids", ids)
				.WithParameter("showErrors", "true")
				.ForShop(34);

			var response = await request.Please();

			Assert.That(response, Is.Not.Null);

			var error = response.Errors[0];
			Assert.That(error.ItemId, Is.EqualTo(-1));
			Assert.That(error.Code, Is.EqualTo(2001));
			Assert.That(error.ErrorMessage, Is.Not.Empty);
		}

		[Test]
		public async Task Should_not_return_errors_by_default()
		{
			var ids = new List<int> { FirstId, SecondId, -1, -2 };
			var request = _api.Create<ReleasesBatch>()
				.WithParameter("releaseids", ids)
				.ForShop(34);

			var response = await request.Please();

			Assert.That(response, Is.Not.Null);

			Assert.That(response.Releases, Is.Not.Null);
			Assert.That(response.Releases.Count, Is.EqualTo(2));

			Assert.That(response.Errors, Is.Not.Null);
			Assert.That(response.Errors.Count, Is.EqualTo(0));
		}

		[Test]
		public async Task Should_return_subscription_streaming_when_requested()
		{
			var ids = new List<int> { FirstId };
			var request = _api.Create<ReleasesBatch>()
				.WithParameter("releaseids", ids)
				.ForUsageTypes(UsageType.SubscriptionStreaming)
				.ForShop(34);


			var response = await request.Please();

			Assert.That(response, Is.Not.Null);

			Assert.That(response.Releases, Is.Not.Null);
			Assert.That(response.Releases.Count, Is.EqualTo(1));

			var subscriptionStreaming = response.Releases.Select(r => r.SubscriptionStreaming).ToList();
			Assert.That(subscriptionStreaming.Count, Is.EqualTo(1));
			Assert.That(subscriptionStreaming[0], Is.Not.Null);
			Assert.That(subscriptionStreaming[0].ReleaseDate, Is.Not.EqualTo(default(DateTime)));
		}

		[Test]
		public async Task Should_return_ad_supported_streaming_when_requested()
		{
			var ids = new List<int> { FirstId };
			var request = _api.Create<ReleasesBatch>()
				.WithParameter("releaseids", ids)
				.ForUsageTypes(UsageType.AdSupportedStreaming)
				.ForShop(34);

			var response = await request.Please();

			Assert.That(response, Is.Not.Null);

			Assert.That(response.Releases, Is.Not.Null);
			Assert.That(response.Releases.Count, Is.EqualTo(1));

			var adSupportedStreaming = response.Releases.Select(r => r.AdSupportedStreaming).ToList();
			Assert.That(adSupportedStreaming.Count, Is.EqualTo(1));
			Assert.That(adSupportedStreaming[0], Is.Not.Null);
			Assert.That(adSupportedStreaming[0].ReleaseDate, Is.Not.EqualTo(default(DateTime)));
		}

		[Test]
		public async Task Should_return_download_when_requested()
		{
			var ids = new List<int> { FirstId, SecondId };
			var request = _api.Create<ReleasesBatch>()
				.WithParameter("releaseids", ids)
				.ForUsageTypes(UsageType.Download)
				.ForShop(34);


			var response = await request.Please();

			Assert.That(response, Is.Not.Null);

			Assert.That(response.Releases, Is.Not.Null);
			Assert.That(response.Releases.Count, Is.EqualTo(2));

			var download = response.Releases.Select(r => r.Download).ToList();
			Assert.That(download.Count, Is.EqualTo(2));
			Assert.That(download, Is.All.Not.Null);
			Assert.That(download.Select(d => d.ReleaseDate), Is.All.Not.EqualTo(default(DateTime)));
			Assert.That(download.Select(d => d.PreviewDate), Is.All.Not.EqualTo(default(DateTime)));
			Assert.That(download.Select(d => d.Packages), Is.All.Not.Empty);
		}

		[Test]
		public async Task Release_has_slug_when_usage_types_are_requested()
		{
			var ids = new List<int> { FirstId, SecondId };
			var request = _api.Create<ReleasesBatch>()
				.WithParameter("releaseids", ids)
				.ForUsageTypes(UsageType.Download, UsageType.SubscriptionStreaming)
				.ForShop(34);

			var response = await request.Please();

			var releaseSlugs = response.Releases.Select(r => r.Slug);
			var artistSlugs = response.Releases.Select(r => r.Artist.Slug);
			Assert.That(releaseSlugs, Is.All.Not.Null);
			Assert.That(artistSlugs, Is.All.Not.Null);
		}

	}
}
