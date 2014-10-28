using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Releases
{
	public enum ReleaseType
	{
		Single,
		Album,
		Video,
		Exclusive,
		Item,

		[XmlEnum(Name = "")]
		Unknown,
	}
}