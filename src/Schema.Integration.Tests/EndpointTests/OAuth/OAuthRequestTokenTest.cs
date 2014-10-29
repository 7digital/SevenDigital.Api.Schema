using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Schema.OAuth;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.OAuth
{
	[TestFixture]
	public class OAuthRequestTokenTest
	{
		private IApi _api;

		[TestFixtureSetUp]
		public void Setup()
		{
			_api = new ApiConnection();
		}

		[Test]
		public async Task Should_not_throw_unauthorised_exception_if_correct_creds_passed() 
		{
			var oAuthRequestToken = await _api.Create<OAuthRequestToken>().Please();
			Assert.That(oAuthRequestToken.Secret, Is.Not.Empty);
			Assert.That(oAuthRequestToken.Token, Is.Not.Empty);
		}

		[Test]
		public async Task Should_allow_POSTing_to_request_token_endpoint()
		{
			var request = _api.Create<OAuthRequestToken>()
				.WithMethod(HttpMethod.Post)
				.WithParameter("one", "two");

			var requestToken = await request.Please();

			Assert.That(requestToken.Secret, Is.Not.Empty);
			Assert.That(requestToken.Token, Is.Not.Empty);
		}

		[Test]
		public void POSTing_with_no_data_should_be_allowed()
		{
			var request = _api.Create<OAuthRequestToken>()
				.WithMethod(HttpMethod.Post);

			Assert.DoesNotThrow(async () => await request.Please());
		}

		[Test]
		public async Task Can_handle_odd_characters_in_get_signing_process()
		{
			//arbitrary parameter, but should test for errors in signature generation
			var request = _api.Create<OAuthRequestToken>()
				.WithParameter("foo", "%! blah"); 
				
			var oAuthRequestToken = await request.Please();

			Assert.That(oAuthRequestToken.Secret, Is.Not.Empty);
			Assert.That(oAuthRequestToken.Token, Is.Not.Empty);
		}

		[Test]
		public async Task Can_handle_odd_characters_in_post_signing_process()
		{
			var request = _api.Create<OAuthRequestToken>()
				.WithMethod(HttpMethod.Post)
				.WithParameter("foo", "%! blah"); //arbitrary parameter, but should test for errors in signature generation

			var oAuthRequestToken = await request.Please();

			Assert.That(oAuthRequestToken.Secret, Is.Not.Empty);
			Assert.That(oAuthRequestToken.Token, Is.Not.Empty);
		}
	}
}
