using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SevenDigital.Api.Schema.Baskets;
using SevenDigital.Api.Schema.Integration.Tests.Infrastructure;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.EndpointTests.Baskets
{
	[TestFixture]
	public class BasketCreateTests
	{
		private const int EXPECTED_RELEASE_ID = 160553;
		private const int EXPECTED_TRACK_ID = 1693930;
		private const int STANDARD_PACKAGE_ID = 2;

		private IApi _api;

		[TestFixtureSetUp]
		public void Setup()
		{
			_api = new ApiConnection();
		}

		[Test]
		public async Task Can_retrieve_a_basket()
		{
			var basketId = await MakeBasket();

			var request = _api.Create<Basket>()
				.UseBasketId(basketId);
			var basket = await request.Please();

			Assert.That(basket, Is.Not.Null);
			Assert.That(basket.Id, Is.EqualTo(basketId));
		}

		[Test]
		public async Task Can_add_and_remove_release_to_a_basket()
		{
			var basketId = await MakeBasket();

			var request = _api.Create<AddItemToBasket>()
				.UseBasketId(basketId)
				.ForReleaseId(EXPECTED_RELEASE_ID)
				.WithParameter("packageId", STANDARD_PACKAGE_ID);
			var basketAdded = await request.Please();

			Assert.That(basketAdded, Is.Not.Null);
			Assert.That(basketAdded.Id, Is.EqualTo(basketId));
			Assert.That(basketAdded.BasketItems.Items.Count, Is.GreaterThan(0));
			Assert.That(basketAdded.BasketItems.Items.First().ReleaseId, Is.EqualTo(EXPECTED_RELEASE_ID.ToString()));
			Assert.That(basketAdded.BasketItems.Items.First().Package.Id, Is.EqualTo(STANDARD_PACKAGE_ID));

			int toRemove = basketAdded.BasketItems.Items.First().Id;

			var removeRequest = _api.Create<RemoveItemFromBasket>()
				.UseBasketId(basketId)
				.BasketItemId(toRemove);
			var basketRemoved = await removeRequest.Please();

			Assert.That(basketRemoved, Is.Not.Null);
			Assert.That(basketRemoved.Id, Is.EqualTo(basketId));
			Assert.That(basketRemoved.BasketItems.Items.Count(x => x.Id == toRemove), Is.EqualTo(0));
		}

		[Test]
		public async Task Can_add_and_remove_track_to_a_basket()
		{
			var basketId = await MakeBasket();

			var addRequest = _api.Create<AddItemToBasket>()
				.UseBasketId(basketId)
				.ForReleaseId(EXPECTED_RELEASE_ID)
				.ForTrackId(EXPECTED_TRACK_ID)
				.WithParameter("packageId", STANDARD_PACKAGE_ID);
			var basketAdded = await addRequest.Please();

			Assert.That(basketAdded, Is.Not.Null); Assert.That(basketAdded.Id, Is.EqualTo(basketId));
			Assert.That(basketAdded.BasketItems.Items.Count, Is.GreaterThan(0));
			Assert.That(basketAdded.BasketItems.Items.First().TrackId, Is.EqualTo(EXPECTED_TRACK_ID.ToString()));
			Assert.That(basketAdded.BasketItems.Items.First().Package.Id, Is.EqualTo(STANDARD_PACKAGE_ID));


			int toRemove = basketAdded.BasketItems.Items.First().Id;

			var request = _api.Create<RemoveItemFromBasket>()
				.UseBasketId(basketId)
				.BasketItemId(toRemove);
			var basketRemoved = await request.Please();

			Assert.That(basketRemoved, Is.Not.Null);
			Assert.That(basketRemoved.Id, Is.EqualTo(basketId));
			Assert.That(basketRemoved.BasketItems.Items.Count(x => x.Id == toRemove), Is.EqualTo(0));
		}

		[Test]
		public async Task Should_show_amount_due()
		{
			var basketId = await MakeBasket();

			var request = _api.Create<AddItemToBasket>()
				.UseBasketId(basketId)
				.ForReleaseId(EXPECTED_RELEASE_ID);
			var basket = await request.Please();

			Assert.That(basket.BasketItems.Items.First().AmountDue.Amount, Is.EqualTo("7.99"));
			Assert.That(basket.BasketItems.Items.First().AmountDue.FormattedAmount, Is.EqualTo("£7.99"));
			Assert.That(basket.AmountDue.Amount, Is.EqualTo("7.99"));
			Assert.That(basket.AmountDue.FormattedAmount, Is.EqualTo("£7.99"));
		}

		private async Task<string> MakeBasket()
		{
			var createBasketRequest = _api.Create<CreateBasket>()
				.WithParameter("country", "GB");
			var basketCreate = await createBasketRequest.Please();

			Assert.That(basketCreate, Is.Not.Null);
			Assert.That(basketCreate.Id, Is.Not.Empty);
			
			return basketCreate.Id;
		}
	}
}
