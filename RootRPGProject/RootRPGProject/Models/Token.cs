using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RootRPGProject.Models
{
	public partial class Token
	{
		[JsonProperty("token")]
		public string TokenToken { get; set; }
		[JsonProperty("expiration")]
		public DateTimeOffset Expiration { get; set; }

	}
	public partial class Token
	{
		public static Token FromJson(string json) => JsonConvert.DeserializeObject<Token>(json, RootRPGProject.Models.TokenConverter.Settings);
	}

	public static class SerializeToken
	{
		public static string ToJson(this Token self) => JsonConvert.SerializeObject(self, RootRPGProject.Models.TokenConverter.Settings);
	}

	internal static class TokenConverter
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
