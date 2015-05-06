using System.Linq;
using SevenDigital.Api.Schema.Media;
using SevenDigital.Api.Schema.Packages;

namespace SevenDigital.Api.Schema.Legacy
{
	public static class FormatLegacyMapper
	{

		public static FormatList PrimaryPackageFormats(Download download)
		{
			var package = download.PrimaryPackage();
			if (package == null)
			{
				return null;
			}

			return new FormatList
			{
				AvailableDrmFree = true,
				Formats = package.Formats.Select(ConvertFormat).ToList()
			};
		}

		private static Format ConvertFormat(PackageFormat packageFormat)
		{
			var desc = packageFormat.Description;
			var fileFormat = desc;
			var bitRate = string.Empty;

			var spaceIndex = desc.IndexOf(' ');
			if (spaceIndex > 0)
			{
				fileFormat = desc.Substring(0, spaceIndex);
				bitRate = desc.Substring(spaceIndex + 1);
			}

			return new Format
			{
				Id = packageFormat.Id,
				DrmFree = true,
				FileFormat = fileFormat,
				BitRate = bitRate
			};
		}
	}
}