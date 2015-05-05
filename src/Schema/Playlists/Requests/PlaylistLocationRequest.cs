using System;

namespace SevenDigital.Api.Schema.Playlists.Requests
{
	[Serializable]
	public class PlaylistLocationRequest : HasPaging
	{
		public string User { get; set; }
	}
}