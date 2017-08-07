using System.Collections.Generic;
using NUnit.Framework;
using SevenDigital.Api.Schema.Packages;

namespace SevenDigital.Api.Schema.Unit.Tests.Packages
{
	[TestFixture]
	public class PackagesTests
	{
		[Test]
		public void Should_have_no_primary_package_when_there_are_no_packages()
		{
			var download = new Download();

			var primaryPackage = download.PrimaryPackage();

			Assert.That(primaryPackage, Is.Null);
		}

		[Test]
		public void Should_have_primary_package_when_there_is_one_package()
		{
			var download = new Download
			{
				Packages = new List<Package>
				{
					new Package {Id = 123}
				}
			};

			var primaryPackage = download.PrimaryPackage();

			Assert.That(primaryPackage, Is.Not.Null);
			Assert.That(primaryPackage.Id, Is.EqualTo(123));
		}

		[Test]
		public void Should_pick_standard_package_if_present()
		{
			var download = new Download
			{
				Packages = new List<Package>
				{
					new Package {Id = 123},
					new Package {Id = 456},
					new Package {Id = 2},
					new Package {Id = 789}
				}
			};

			var primaryPackage = download.PrimaryPackage();

			Assert.That(primaryPackage, Is.Not.Null);
			Assert.That(primaryPackage.Id, Is.EqualTo(2));
		}

		[Test]
		public void Should_fall_back_to_first_package()
		{
			var download = new Download
			{
				Packages = new List<Package>
				{
					new Package {Id = 123},
					new Package {Id = 456},
					new Package {Id = 789}
				}
			};

			var primaryPackage = download.PrimaryPackage();

			Assert.That(primaryPackage, Is.Not.Null);
			Assert.That(primaryPackage.Id, Is.EqualTo(123));
		}
	}
}
