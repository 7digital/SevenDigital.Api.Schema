using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.Matching;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Matching
{
	[TestFixture]
	public class ReleaseMatchTests
	{
		private readonly Guid _matchedMbId = Guid.Parse("B899ABE8-2E14-31E0-AC5C-7FA0064FFF8C");
		private const int MatchedSevenDigitalReleaseId = 6990;

		private readonly IApi _api = new ApiConnection();

		[Test]
		public async Task Should_return_populated_match_by_musicbrainz_id()
		{
			var request = _api.Create<ReleaseMatchResponse>()
				.WithParameter("mbIds", _matchedMbId.ToString());
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Releases.Count, Is.EqualTo(1));

			var resultWithMatch = apiResponse.Releases[0];

			Assert.That(resultWithMatch.Release, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);
			Assert.That(resultWithMatch.Release.Count, Is.GreaterThan(0));

			var matchedRelease = resultWithMatch.Release[0];
			Assert.That(matchedRelease.MbId, Is.EqualTo(_matchedMbId.ToString()));
			Assert.That(matchedRelease.SevenDigitalId, Is.Not.Empty);
		}

		[Test]
		public async Task Should_return_multiple_matches_by_musicbrainz_id()
		{
			var unmatchedMbId = Guid.Parse("00000000-0000-0000-0000-000000000001");
			var mbIds = new List<Guid> { _matchedMbId, unmatchedMbId };

			var request = _api.Create<ReleaseMatchResponse>()
				.WithParameter("mbIds", mbIds);
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Releases.Count, Is.EqualTo(2));

			var resultWithMatch = apiResponse.Releases.Single(
				match => match.MbId.Equals(_matchedMbId.ToString(), StringComparison.InvariantCultureIgnoreCase));
			Assert.That(resultWithMatch.Release, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);

			var resultWithoutMatch = apiResponse.Releases.Single(
				match => match.MbId.Equals(unmatchedMbId.ToString(), StringComparison.InvariantCultureIgnoreCase));
			Assert.That(resultWithoutMatch.Release, Is.Empty);
			Assert.That(resultWithoutMatch.MatchError, Is.Not.Null);
		}

		[Test]
		public async Task Should_return_populated_match_by_sevendigital_id()
		{
			var request = _api.Create<ReleaseMatchResponse>()
				.WithParameter("sevenDigitalIds", MatchedSevenDigitalReleaseId);
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Releases.Count, Is.EqualTo(1));

			var resultWithMatch = apiResponse.Releases[0];

			Assert.That(resultWithMatch.Release, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);
			Assert.That(resultWithMatch.Release.Count, Is.GreaterThan(0));

			var matchedRelease = resultWithMatch.Release[0];
			Assert.That(matchedRelease.MbId, Is.Not.Empty);
			Assert.That(matchedRelease.SevenDigitalId, Is.EqualTo(MatchedSevenDigitalReleaseId.ToString()));
		}

		[Test]
		public async Task Should_return_multiple_matches_by_seven_digital_release_id()
		{
			const int unmatchedId = 0;
			var ids = new List<int> { MatchedSevenDigitalReleaseId, unmatchedId };

			var request = _api.Create<ReleaseMatchResponse>()
				.WithParameter("sevenDigitalIds", ids);
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Releases.Count, Is.EqualTo(2));

			var resultWithMatch = apiResponse.Releases.Single(
				match => match.SevenDigitalId.Equals(MatchedSevenDigitalReleaseId.ToString()));
			Assert.That(resultWithMatch.Release, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);

			var resultWithoutMatch = apiResponse.Releases.Single(
				match => match.SevenDigitalId.Equals(unmatchedId.ToString()));
			Assert.That(resultWithoutMatch.Release, Is.Empty);
			Assert.That(resultWithoutMatch.MatchError, Is.Not.Null);
		}
	}
}