using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SevenDigital.Api.Schema.Playlists.Response.Endpoints;

namespace SevenDigital.Api.Schema.Playlists
{
	internal class AnnotationsJsonConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var annotations = (List<Annotation>) value;
			serializer.Serialize(writer, annotations.ToDictionary(a => a.Key, a => a.Value));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var annotationPairs = serializer.Deserialize<Dictionary<string, string>>(reader);
			return annotationPairs.Select(kv => new Annotation(kv.Key, kv.Value)).ToList();
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(List<Annotation>);
		}
	}
}