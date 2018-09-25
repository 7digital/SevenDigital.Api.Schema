using System;
using System.Xml.Serialization;
using SevenDigital.Api.Schema.Attributes;
using SevenDigital.Api.Schema.ParameterDefinitions.Get;

namespace SevenDigital.Api.Schema.Artists
{
	[Serializable]
	[ApiEndpoint("artist/details")]
	[XmlRoot("artist")]
	public class Artist : HasArtistIdParameter
	{
		[XmlAttribute("id")]
		public int Id { get; set; }

		[XmlElement("name")]
		public string Name { get; set; }

		[XmlElement("sortName")]
		public string SortName { get; set; }

		[XmlElement("appearsAs")]
		public string AppearsAs { get; set; }

		[XmlElement("image")]
		public string Image { get; set; }

		[XmlElement("slug")]
		public string Slug { get; set; }

		[XmlElement("isPlaceholderImage")]
		public bool? IsPlaceholderImage { get; set; }

		public override string ToString()
		{
			return string.Format("{0}: {1}", Id, Name);
		}
	}
}