using System;

namespace SevenDigital.Api.Schema.Playlists.Requests
{
	[Serializable]
	public class Tag
	{
		public string Name { get; set; }

		public Tag()
		{
		}

		public Tag(string name)
		{
			Name = name;
		}
	}
}