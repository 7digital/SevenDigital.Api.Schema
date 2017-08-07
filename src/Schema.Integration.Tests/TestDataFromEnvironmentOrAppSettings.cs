using System.Configuration;

namespace SevenDigital.Api.Schema.Integration.Tests
{
	public class TestDataFromEnvironmentOrAppSettings
	{
		public static string AccessToken
		{
			get
			{
				return ValueFromEnvOrConfig("WRAPPER_INTEGRATION_TEST_ACCESS_TOKEN", "Integration.Tests.AccessToken");
			}
		}
		public static string AccessTokenSecret
		{
			get
			{
				return ValueFromEnvOrConfig("WRAPPER_INTEGRATION_TEST_ACCESS_TOKEN_SECRET", "Integration.Tests.AccessTokenSecret");
			}
		}

		public static string PurchasingAccessToken
		{
			get
			{
				return ValueFromEnvOrConfig("WRAPPER_PURCHASING_INTEGRATION_TEST_ACCESS_TOKEN", "Integration.Tests.Purchasing.AccessToken");
			}
		}
		public static string PurchasingAccessTokenSecret
		{
			get
			{
				return ValueFromEnvOrConfig("WRAPPER_PURCHASING_INTEGRATION_TEST_ACCESS_TOKEN_SECRET", "Integration.Tests.Purchasing.AccessTokenSecret");
			}
		}

		public static string PurchasingReleaseId
		{
			get
			{
				return ValueFromEnvOrConfig("WRAPPER_PURCHASING_INTEGRATION_TEST_RELEASE_ID", "Integration.Tests.Purchasing.ReleaseId");
			}
		}

		private static string ValueFromEnvOrConfig(string envName, string configName)
		{
			return System.Environment.GetEnvironmentVariable(envName) ?? ConfigurationManager.AppSettings[configName];
		}
	}
}