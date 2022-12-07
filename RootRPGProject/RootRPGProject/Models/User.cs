﻿using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using RootRPGProject.Model;
//
//    var user = User.FromJson(jsonString);

namespace RootRPGProject.Models
{

	public partial class User
	{
		[JsonProperty("userName")]
		public string Username { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }
	}

	public partial class User
	{
		public static User FromJson(string json) => JsonConvert.DeserializeObject<User>(json, RootRPGProject.Models.UserConverter.Settings);
	}

	public static class SerializeUser
	{
		public static string ToJson(this User self) => JsonConvert.SerializeObject(self, RootRPGProject.Models.UserConverter.Settings);
	}

	internal static class UserConverter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}
}

