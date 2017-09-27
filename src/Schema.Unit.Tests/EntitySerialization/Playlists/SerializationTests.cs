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
	public class XmlSerializationTests
	{
		[Test]
		public void Should_deserialize_playlist_from_xml()
		{
			const string xml = @"
			<response status=""ok"" version=""1.2"">
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
			Assert.That(playlist.Status, Is.EqualTo("Published"));
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
		public void Should_deserialize_playlist_from_json()
		{
			const string json = @"{
				""id"": ""51ed5cfec9021614f462bb7b"",
				""name"": ""party time"",
				""status"": ""published"",
				""visibility"": ""Public"",
				""imageUrl"": ""http://artwork-cdn.7static.com/static/img/sleeveart/00/004/963/0000496338_$size$.jpg"",
				""description"": ""Hits to get the party started"",
				""lastUpdated"": ""2013-10-02T12:16:04.615Z"",
				""tracks"": [{
					""playlistItemId"": ""52cd88c2c902161660aeab80"",
					""trackId"": ""5495893"",
					""trackTitle"": ""No You Girls""
				}],
				""annotations"": {
					""key"": ""value"",
					""another key"": ""another value""
				},
				""tags"": [{ ""name"": ""tag1"", ""playlistPosition"": 1 }, { ""name"": ""tag2"" }]
			}";

			var response = new Wrapper.Responses.Response(System.Net.HttpStatusCode.OK, new Dictionary<string, string>{ { "Content-Type", "application/json" } }, json);
			var playlist = new ResponseDeserializer().DeserializeResponse<Playlist>(response, true);

			Assert.That(playlist.Id, Is.EqualTo("51ed5cfec9021614f462bb7b"));
			Assert.That(playlist.Name, Is.EqualTo("party time"));
			Assert.That(playlist.Description, Is.EqualTo("Hits to get the party started"));
			Assert.That(playlist.Status, Is.EqualTo("published"));
			Assert.That(playlist.Visibility, Is.EqualTo(PlaylistVisibilityType.Public));
			Assert.That(playlist.ImageUrl, Is.EqualTo("http://artwork-cdn.7static.com/static/img/sleeveart/00/004/963/0000496338_$size$.jpg"));

			Assert.That(playlist.Tracks.Count, Is.EqualTo(1));
			Assert.That(playlist.Tracks[0].PlaylistItemId, Is.EqualTo("52cd88c2c902161660aeab80"));
			Assert.That(playlist.Tracks[0].TrackId, Is.EqualTo("5495893"));
			Assert.That(playlist.Tracks[0].TrackTitle, Is.EqualTo("No You Girls"));

			Assert.That(playlist.LastUpdated.ToString("O"), Is.EqualTo("2013-10-02T12:16:04.6150000Z"));

			Assert.That(playlist.Annotations.Count, Is.EqualTo(2));
			Assert.That(playlist.Annotations[0].Key, Is.EqualTo("key"));
			Assert.That(playlist.Annotations[0].Value, Is.EqualTo("value"));
			Assert.That(playlist.Annotations[1].Key, Is.EqualTo("another key"));
			Assert.That(playlist.Annotations[1].Value, Is.EqualTo("another value"));

			Assert.That(playlist.Tags.Count, Is.EqualTo(2));
			Assert.That(playlist.Tags[0].Name, Is.EqualTo("tag1"));
			Assert.That(playlist.Tags[0].PlaylistPosition, Is.EqualTo(1));
			Assert.That(playlist.Tags[1].Name, Is.EqualTo("tag2"));
			Assert.That(playlist.Tags[1].PlaylistPosition, Is.Null);
		}

		[Test]
		public void Should_serialize_playlist_request_to_xml()
		{
			var playlistRequest = new PlaylistRequest
			{
				Description = "A New Playlist Description",
				ImageUrl = "an-image-url",
				Name = "New Playlist",
				Tracks = new List<Product> {
					new Product { TrackId = "12345" },
					new Product { TrackId = "98765"}
				},
				Status = "Published",
				Tags = new List<Tag>
				{
					new Tag { Name = "tag1", PlaylistPosition = 1 },
					new Tag { Name = "tag2" }
				},
				Visibility = PlaylistVisibilityType.Private,
				Annotations = new List<Annotation>
				{
					new Annotation("key", "value"),
					new Annotation("another key", "another value")
				}
			};

			var xml = new XmlPayloadSerializer().Serialize(playlistRequest);

			Debug.WriteLine(xml);

			const string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
				"<playlist>" +
				"<name>New Playlist</name>" +
				"<visibility>Private</visibility>" +
				"<status>Published</status>" +
				"<description>A New Playlist Description</description>" +
				"<image>an-image-url</image>" +
				"<tags><tag><name>tag1</name><playlistPosition>1</playlistPosition></tag><tag><name>tag2</name><playlistPosition p4:nil=\"true\" xmlns:p4=\"http://www.w3.org/2001/XMLSchema-instance\" /></tag></tags>" +
				"<annotations><annotation key=\"key\">value</annotation><annotation key=\"another key\">another value</annotation></annotations>" +
				"<tracks><track><trackId>12345</trackId></track><track><trackId>98765</trackId></track></tracks>" +
				"</playlist>";

			Assert.That(xml, Is.EqualTo(expectedXml));
		}

		[Test]
		public void Should_serialize_playlist_request_to_json()
		{
			var playlistRequest = new PlaylistRequest
			{
				Description = "A New Playlist Description",
				ImageUrl = "an-image-url",
				Name = "New Playlist",
				Tracks = new List<Product> {
					new Product { TrackId = "12345" },
					new Product { TrackId = "98765"}
				},
				Status = "Published",
				Tags = new List<Tag>
				{
					new Tag { Name="tag1", PlaylistPosition=1 },
					new Tag { Name="tag2" }
				},
				Visibility = PlaylistVisibilityType.Private,
				Annotations = new List<Annotation>
				{
					new Annotation("key", "value"),
					new Annotation("another key", "another value")
				}
			};

			var json = new JsonPayloadSerializer().Serialize(playlistRequest);

			Debug.WriteLine(json);

			var expectedJson = string.Join("",
				"{",
					"\"tracks\":[{\"trackId\":\"12345\"},{\"trackId\":\"98765\"}],",
					"\"name\":\"New Playlist\",",
					"\"visibility\":\"Private\",",
					"\"status\":\"Published\",",
					"\"description\":\"A New Playlist Description\",",
					"\"imageUrl\":\"an-image-url\",",
					"\"tags\":[{\"name\":\"tag1\",\"playlistPosition\":1},{\"name\":\"tag2\",\"playlistPosition\":null}],",
					"\"annotations\":{",
						"\"key\":\"value\",",
						"\"another key\":\"another value\"",
					"}",
				"}"
			);

			Assert.That(json, Is.EqualTo(expectedJson));
		}

		[Test]
		public void Should_initialize_playlist_collections()
		{
			var details = new PlaylistDetails();
			Assert.That(details.Tags, Is.EqualTo(new List<Tag>()));
			Assert.That(details.Annotations, Is.EqualTo(new List<Annotation>()));

			var detailsRequest = new PlaylistDetailsRequest();
			Assert.That(detailsRequest.Tags, Is.EqualTo(new List<Tag>()));
			Assert.That(detailsRequest.Annotations, Is.EqualTo(new List<Annotation>()));
		}
	}
}