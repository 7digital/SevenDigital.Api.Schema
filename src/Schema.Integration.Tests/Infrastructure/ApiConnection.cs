using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.Infrastructure
{
	public class ApiConnection : IApi
	{
		private readonly ApiFactory _apiFactory;

		public ApiConnection()
		{
			_apiFactory = new ApiFactory(new ApiUri(), new AppSettingsCredentials());
		}

		public IFluentApi<T> Create<T>() where T : class, new()
		{
			return _apiFactory.Create<T>();
		}
	}
}
