﻿using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Tags;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Tags
{
	[TestFixture]
	public class ReleaseTagsTests
	{
		private const int LilyAllenAirBalloon = 3228088;

		private readonly IApi _api = new ApiConnection();

		[Test]
		public async Task Can_hit_endpoint()
		{
			const string ExpectedTagId = "pop";

			var request = _api.Create<ReleaseTags>()
				.ForReleaseId(LilyAllenAirBalloon);

			var releaseTags = await request.Please();

			Assert.That(releaseTags, Is.Not.Null);
			Assert.That(releaseTags.TagList, Is.Not.Null);
			Assert.That(releaseTags.TagList.Count, Is.GreaterThan(0));

			Assert.That(releaseTags.TagList[0].Id, Is.Not.Empty);

			var expectedTag = releaseTags.TagList.First(x => x.Id == ExpectedTagId);
			Assert.That(expectedTag.Text, Is.EqualTo(ExpectedTagId));
		}

		[Test]
		public async Task Can_hit_endpoint_with_paging()
		{
			var request = _api.Create<ReleaseTags>()
				.ForReleaseId(LilyAllenAirBalloon)
				.WithPageNumber(2)
				.WithPageSize(1);
			var releaseTags = await request.Please();

			Assert.That(releaseTags, Is.Not.Null);
			Assert.That(releaseTags.Page, Is.EqualTo(2));
			Assert.That(releaseTags.PageSize, Is.EqualTo(1));
		}
	}
}