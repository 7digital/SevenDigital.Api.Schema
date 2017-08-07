using System.Configuration;
using SevenDigital.Api.Wrapper;

namespace SevenDigital.Api.Schema.Integration.Tests.Infrastructure
{
	public abstract class AppSettingsCredentialsBase : IOAuthCredentials
	{
		public abstract string ConsumerKey { get; protected set; }
		public abstract string ConsumerSecret { get; protected set; }

		protected static string ValueFromEnvOrConfig(string envName, string configName)
		{
			return System.Environment.GetEnvironmentVariable(envName) ?? ConfigurationManager.AppSettings[configName];
		}
	}

	public sealed class AppSettingsCredentials : AppSettingsCredentialsBase
	{
		public AppSettingsCredentials()
		{
			ConsumerKey = ValueFromEnvOrConfig("WRAPPER_INTEGRATION_TEST_CONSUMER_KEY", "Wrapper.ConsumerKey");
			ConsumerSecret = ValueFromEnvOrConfig("WRAPPER_INTEGRATION_TEST_CONSUMER_SECRET", "Wrapper.ConsumerSecret");
		}

		public override string ConsumerKey { get; protected set; }
		public override string ConsumerSecret { get; protected set; }
	}

	public sealed class PurchasingAppSettingsCredentials : AppSettingsCredentialsBase
	{
		public PurchasingAppSettingsCredentials()
		{
			ConsumerKey = ValueFromEnvOrConfig(
				"WRAPPER_PURCHASING_INTEGRATION_TEST_CONSUMER_KEY",
				"Wrapper.Purchasing.ConsumerKey");
			ConsumerSecret = ValueFromEnvOrConfig(
				"WRAPPER_PURCHASING_INTEGRATION_TEST_CONSUMER_SECRET",
				"Wrapper.Purchasing.ConsumerSecret");
		}

		public override string ConsumerKey { get; protected set; }
		public override string ConsumerSecret { get; protected set; }
	}
}