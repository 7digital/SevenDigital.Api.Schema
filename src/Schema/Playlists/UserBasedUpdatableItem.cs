﻿using System;
using System.Xml.Serialization;

namespace SevenDigital.Api.Schema.Playlists
{
	public class UserBasedUpdatableItem
	{
		public string UserId { get; set; }

		[XmlElement("lastUpdated")]
		public DateTime LastUpdated { get; set; }
	}
}