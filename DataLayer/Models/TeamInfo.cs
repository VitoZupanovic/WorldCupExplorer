#nullable enable
#pragma warning disable CS8618
#pragma warning disable CS8601
#pragma warning disable CS8603

namespace WorldCup.DataLayer.Models
{
    using System.Collections.Generic;

    using System.Text.Json;
    using System.Text.Json.Serialization;

    public partial class TeamInfo
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("alternate_name")]
        public object AlternateName { get; set; }

        [JsonPropertyName("fifa_code")]
        public string FifaCode { get; set; }

        [JsonPropertyName("group_id")]
        public long? GroupId { get; set; }

        [JsonPropertyName("group_letter")]
        public string GroupLetter { get; set; }
    }

    public partial class TeamInfo
    {
        public static List<TeamInfo> FromJson(string json) => JsonSerializer.Deserialize<List<TeamInfo>>(json, WorldCup.DataLayer.Models.Converter.Settings);
    }

}
#pragma warning restore CS8618
#pragma warning restore CS8601
#pragma warning restore CS8603
