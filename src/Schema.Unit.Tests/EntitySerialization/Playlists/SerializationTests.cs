using NUnit.Framework;
using SevenDigital.Api.Schema.Playlists;
using SevenDigital.Api.Schema.Playlists.Requests;
using SevenDigital.Api.Schema.Playlists.Response.Endpoints;
using SevenDigital.Api.Wrapper.Requests.Serializing;
using SevenDigital.Api.Wrapper.Responses.Parsing;
using System.Collections.Generic;
using System.Diagnostics;

namespace SevenDigital.Api.Schema.Unit.Tests.EntitySerialization.Playlists
{
	[TestFixture]
	public class XmlSerializationTests
	{

		[Test]
		public void Should_serialize_playlist_from_xml()
		{
			var xml = @"<response status=""ok"" version=""1.2"">
  <playlist id=""51ed5cfec9021614f462bb7b"">
    <name>party time</name>
    <description>Hits to get the party started</description>
    <status>Published</status>
    <visibility>Public</visibility>
    <image>http://artwork-cdn.7static.com/static/img/sleeveart/00/004/963/0000496338_$size$.jpg</image>
    <tracks><track id=""524ae1a1c90216252c1837ab""><trackId>5495893</trackId><trackTitle>No You Girls</trackTitle></track></tracks>
	<annotations>
      <annotation key=""key"">value</annotation>
      <annotation key=""anotherkey"">another value</annotation>
    </annotations>
    <lastUpdated>2013-10-02T12:16:04.615Z</lastUpdated>
  </playlist>
</response>";

			var response = new Wrapper.Responses.Response(System.Net.HttpStatusCode.OK, xml);
			var playlist = new ResponseDeserializer().DeserializeResponse<Playlist>(response, true);

			Assert.That(playlist.Id, Is.EqualTo("51ed5cfec9021614f462bb7b"));
			Assert.That(playlist.Name, Is.EqualTo("party time"));
			Assert.That(playlist.Description, Is.EqualTo("Hits to get the party started"));
			Assert.That(playlist.Status, Is.EqualTo(PlaylistStatusType.Published));
			Assert.That(playlist.Visibility, Is.EqualTo(PlaylistVisibilityType.Public));
			Assert.That(playlist.ImageUrl, Is.EqualTo("http://artwork-cdn.7static.com/static/img/sleeveart/00/004/963/0000496338_$size$.jpg"));

			Assert.That(playlist.Tracks.Count, Is.EqualTo(1));
			Assert.That(playlist.Tracks[0].PlaylistItemId, Is.EqualTo("524ae1a1c90216252c1837ab"));
			Assert.That(playlist.Tracks[0].TrackId, Is.EqualTo("5495893"));
			Assert.That(playlist.Tracks[0].TrackTitle, Is.EqualTo("No You Girls"));

			Assert.That(playlist.LastUpdated.ToString("O"), Is.EqualTo("2013-10-02T12:16:04.6150000Z"));

			Assert.That(playlist.Annotations.Count, Is.EqualTo(2));
			Assert.That(playlist.Annotations[0].Key, Is.EqualTo("key"));
			Assert.That(playlist.Annotations[0].Value, Is.EqualTo("value"));

		}

		[Test]
		public void Should_deserialize_from_playlist_request()
		{
			var playlistRequest = new PlaylistDetailsRequest()
			{
				Description = "A New Playlist Description",
				ImageUrl = "an-image-url",
				Name = "New Playlist",
				Status = PlaylistStatusType.Published,
				Tags = new List<Tag>()
				{
					new Tag { Name="Tag", PlaylistPosition=1 }
				},
				Visibility = PlaylistVisibilityType.Private,
				Annotations = new List<Annotation>()
				{
					{ new Annotation("key", "value") },
					{ new Annotation("another key", "another value") }
				}
			};

			var xml = new XmlPayloadSerializer().Serialize(playlistRequest);

			Debug.WriteLine(xml);

			var expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
				"<playlist>" +
				"<name>New Playlist</name>" +
				"<visibility>Private</visibility>" +
				"<status>Published</status>" +
				"<description>A New Playlist Description</description>" +
				"<image>an-image-url</image>" +
				"<tags><tag><name>Tag</name><playlistPosition>1</playlistPosition></tag></tags>" +
				"<annotations><annotation key=\"key\">value</annotation><annotation key=\"another key\">another value</annotation></annotations>" +
				"</playlist>";

			Assert.That(xml, Is.EqualTo(expectedXml));
		}

	}
}