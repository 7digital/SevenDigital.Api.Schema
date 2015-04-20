using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.Matching;
using SevenDigital.Api.Wrapper;
using SevenDigital.Api.Wrapper.Requests;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Matching
{
	[TestFixture]
	public class ArtistMatchTests
	{
		private readonly Guid _matchedMbId = Guid.Parse("DFE9A7C4-8CF2-47F4-9DCB-D233C2B86EC3");
		private const int MatchedSevenDigitalArtistId = 4;

		private readonly IApi _api = new ApiConnection();

		[Test]
		public async Task Should_return_populated_match_by_musicbrainz_id()
		{
			var request = _api.Create<ArtistMatchResponse>()
				.WithParameter("mbIds", _matchedMbId.ToString());
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Artists.Count, Is.EqualTo(1));

			var resultWithMatch = apiResponse.Artists[0];

			Assert.That(resultWithMatch.Artist, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);
			Assert.That(resultWithMatch.Artist.Count, Is.GreaterThan(0));

			var matchedArtist = resultWithMatch.Artist[0];
			Assert.That(matchedArtist.MbId, Is.EqualTo(_matchedMbId.ToString()));
			Assert.That(matchedArtist.SevenDigitalId, Is.Not.Empty);
		}

		[Test]
		public async Task Should_return_multiple_matches_by_musicbrainz_id()
		{
			var unmatchedMbId = Guid.Parse("00000000-0000-0000-0000-000000000001");
			var mbIds = new List<Guid> { _matchedMbId, unmatchedMbId };

			var request = _api.Create<ArtistMatchResponse>()
				.WithParameter("mbIds", mbIds);
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Artists.Count, Is.EqualTo(2));

			var resultWithMatch = apiResponse.Artists.Single(
				match => match.MbId.Equals(_matchedMbId.ToString(), StringComparison.InvariantCultureIgnoreCase));
			Assert.That(resultWithMatch.Artist, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);

			var resultWithoutMatch = apiResponse.Artists.Single(
				match => match.MbId.Equals(unmatchedMbId.ToString(), StringComparison.InvariantCultureIgnoreCase));
			Assert.That(resultWithoutMatch.Artist, Is.Empty);
			Assert.That(resultWithoutMatch.MatchError, Is.Not.Null);
		}

		[Test]
		public async Task Should_return_populated_match_by_sevendigital_id()
		{
			var request = _api.Create<ArtistMatchResponse>()
				.WithParameter("sevenDigitalIds", MatchedSevenDigitalArtistId);
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Artists.Count, Is.EqualTo(1));

			var resultWithMatch = apiResponse.Artists[0];

			Assert.That(resultWithMatch.Artist, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);
			Assert.That(resultWithMatch.Artist.Count, Is.GreaterThan(0));

			var matchedArtist = resultWithMatch.Artist[0];
			Assert.That(matchedArtist.MbId, Is.Not.Empty);
			Assert.That(matchedArtist.SevenDigitalId, Is.EqualTo(MatchedSevenDigitalArtistId.ToString()));
		}

		[Test]
		public async Task Should_return_multiple_matches_by_seven_digital_artist_id()
		{
			const int unmatchedId = 0;
			var ids = new List<int> { MatchedSevenDigitalArtistId, unmatchedId };

			var request = _api.Create<ArtistMatchResponse>()
				.WithParameter("sevenDigitalIds", ids);
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Artists.Count, Is.EqualTo(2));

			var resultWithMatch = apiResponse.Artists.Single(
				match => match.SevenDigitalId.Equals(MatchedSevenDigitalArtistId.ToString()));
			Assert.That(resultWithMatch.Artist, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);

			var resultWithoutMatch = apiResponse.Artists.Single(
				match => match.SevenDigitalId.Equals(unmatchedId.ToString()));
			Assert.That(resultWithoutMatch.Artist, Is.Empty);
			Assert.That(resultWithoutMatch.MatchError, Is.Not.Null);
		}
	}
}