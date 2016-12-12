using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Playlists.Response.Endpoints
{
	public class PlaylistTag
	{
		[XmlElement("name")]
		public string Name { get; set; }
	}
}