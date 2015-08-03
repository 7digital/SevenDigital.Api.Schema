using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Packages
{
	[Serializable]
	public class Download
	{
		[XmlElement("releaseDate")]
		public DateTime ReleaseDate { get; set; }

		[XmlElement("previewDate")]
		public DateTime? PreviewDate { get; set; }

		[XmlArray("packages")]
		[XmlArrayItem("package")]
		public List<Package> Packages { get; set; }

		public Package PrimaryPackage()
		{
			const int standardPackageId = 2;

			if (Packages == null || Packages.Count == 0)
			{
				return null;
			}

			if (Packages.Count == 1)
			{
				return Packages[0];
			}

			var result = Packages.FirstOrDefault(p => p.Id == standardPackageId);
			result = result ?? Packages.First();
			return result;
		}
	}
}