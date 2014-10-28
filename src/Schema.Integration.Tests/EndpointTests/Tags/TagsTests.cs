﻿using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Schema.Tags;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Tags
{
	[TestFixture]
	public class TagsTests 
	{
		[Test]
		public async Task Can_hit_endpoint() 
		{
			var tags = await Api<TagsResponse>.Create.Please();

			Assert.That(tags, Is.Not.Null);
			Assert.That(tags.TagList.Count, Is.GreaterThan(0));
			Assert.That(tags.TagList.FirstOrDefault().Id, Is.Not.Empty);
			Assert.That(tags.TagList.Where(x=>x.Id == "rock").FirstOrDefault().Text, Is.EqualTo("rock"));
		}

		[Test]
		public async Task an_hit_endpoint_with_paging()
		{
			var request = Api<TagsResponse>.Create
				.WithParameter("page", "2")
				.WithParameter("pageSize", "20");
			var tags = await request.Please();

			Assert.That(tags, Is.Not.Null);
			Assert.That(tags.Page, Is.EqualTo(2));
			Assert.That(tags.PageSize, Is.EqualTo(20));
		}
	}
}