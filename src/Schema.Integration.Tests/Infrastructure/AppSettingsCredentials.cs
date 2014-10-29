using System.Configuration;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.Infrastructure
{
	public class AppSettingsCredentials : IOAuthCredentials
	{
		public AppSettingsCredentials()
		{
			ConsumerKey = ValueFromEnvOrConfig("WRAPPER_INTEGRATION_TEST_CONSUMER_KEY", "Wrapper.ConsumerKey");
			ConsumerSecret = ValueFromEnvOrConfig("WRAPPER_INTEGRATION_TEST_CONSUMER_SECRET", "Wrapper.ConsumerSecret");
		}

		public string ConsumerKey { get; private set; }
		public string ConsumerSecret { get; private set; }

		private static string ValueFromEnvOrConfig(string envName, string configName)
		{
			return System.Environment.GetEnvironmentVariable(envName) ?? ConfigurationManager.AppSettings[configName];
		}
	}
}