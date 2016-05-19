using System;

namespace SevenDigital.Api.Schema.Playlists
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