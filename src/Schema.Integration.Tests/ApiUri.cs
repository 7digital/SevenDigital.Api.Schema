using System.Configuration;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests
{
	public class ApiUri : IApiUri
	{
		public string Uri
		{
			get { return ConfigurationManager.AppSettings["Wrapper.BaseUrl"]; }
		}

		public string SecureUri
		{
			get { return ConfigurationManager.AppSettings["Wrapper.SecureBaseUrl"]; }
		}
	}
}