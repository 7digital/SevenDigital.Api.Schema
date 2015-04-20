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
	public class TrackMatchTests
	{
		private readonly Guid _matchedMbId = Guid.Parse("46E22688-BF7D-4EE2-8F67-E498581408E9");
		private const int MatchedSevenDigitalTrackId = 4;

		private readonly IApi _api = new ApiConnection();

		[Test]
		public async Task Should_return_populated_match_by_musicbrainz_id()
		{
			var request = _api.Create<TrackMatchResponse>()
				.WithParameter("mbIds", _matchedMbId.ToString());
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Tracks.Count, Is.EqualTo(1));

			var resultWithMatch = apiResponse.Tracks[0];

			Assert.That(resultWithMatch.Track, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);
			Assert.That(resultWithMatch.Track.Count, Is.GreaterThan(0));

			var matchedTrack = resultWithMatch.Track[0];
			Assert.That(matchedTrack.MbId, Is.EqualTo(_matchedMbId.ToString()));
			Assert.That(matchedTrack.SevenDigitalId, Is.Not.Empty);
		}

		[Test]
		public async Task Should_return_multiple_matches_by_musicbrainz_id()
		{
			var unmatchedMbId = Guid.Parse("00000000-0000-0000-0000-000000000001");
			var mbIds = new List<Guid> { _matchedMbId, unmatchedMbId };

			var request = _api.Create<TrackMatchResponse>()
				.WithParameter("mbIds", mbIds);
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Tracks.Count, Is.EqualTo(2));

			var resultWithMatch = apiResponse.Tracks.Single(
				match => match.MbId.Equals(_matchedMbId.ToString(), StringComparison.InvariantCultureIgnoreCase));
			Assert.That(resultWithMatch.Track, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);

			var resultWithoutMatch = apiResponse.Tracks.Single(
				match => match.MbId.Equals(unmatchedMbId.ToString(), StringComparison.InvariantCultureIgnoreCase));
			Assert.That(resultWithoutMatch.Track, Is.Empty);
			Assert.That(resultWithoutMatch.MatchError, Is.Not.Null);
		}

		[Test]
		public async Task Should_return_populated_match_by_sevendigital_id()
		{
			var request = _api.Create<TrackMatchResponse>()
				.WithParameter("sevenDigitalIds", MatchedSevenDigitalTrackId);
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Tracks.Count, Is.EqualTo(1));

			var resultWithMatch = apiResponse.Tracks[0];

			Assert.That(resultWithMatch.Track, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);
			Assert.That(resultWithMatch.Track.Count, Is.GreaterThan(0));

			var matchedTrack = resultWithMatch.Track[0];
			Assert.That(matchedTrack.MbId, Is.Not.Empty);
			Assert.That(matchedTrack.SevenDigitalId, Is.EqualTo(MatchedSevenDigitalTrackId.ToString()));
		}

		[Test]
		public async Task Should_return_multiple_matches_by_seven_digital_track_id()
		{
			const int unmatchedId = 0;
			var ids = new List<int> { MatchedSevenDigitalTrackId, unmatchedId };

			var request = _api.Create<TrackMatchResponse>()
				.WithParameter("sevenDigitalIds", ids);
			var apiResponse = await request.Please();

			Assert.That(apiResponse.Tracks.Count, Is.EqualTo(2));

			var resultWithMatch = apiResponse.Tracks.Single(
				match => match.SevenDigitalId.Equals(MatchedSevenDigitalTrackId.ToString()));
			Assert.That(resultWithMatch.Track, Is.Not.Empty);
			Assert.That(resultWithMatch.MatchError, Is.Null);

			var resultWithoutMatch = apiResponse.Tracks.Single(
				match => match.SevenDigitalId.Equals(unmatchedId.ToString()));
			Assert.That(resultWithoutMatch.Track, Is.Empty);
			Assert.That(resultWithoutMatch.MatchError, Is.Not.Null);
		}
	}
}