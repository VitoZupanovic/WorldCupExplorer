#nullable enable
#pragma warning disable CS8618
#pragma warning disable CS8601
#pragma warning disable CS8603

namespace WorldCup.DataLayer.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public partial class Match
    {
        [JsonPropertyName("venue")]
        public string Venue { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("time")]
        public Time Time { get; set; }

        [JsonPropertyName("fifa_id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? FifaId { get; set; }

        [JsonPropertyName("weather")]
        public Weather Weather { get; set; }

        [JsonPropertyName("attendance")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Attendance { get; set; }

        [JsonPropertyName("officials")]
        public List<string> Officials { get; set; }

        [JsonPropertyName("stage_name")]
        public StageName StageName { get; set; }

        [JsonPropertyName("home_team_country")]
        public string HomeTeamCountry { get; set; }

        [JsonPropertyName("away_team_country")]
        public string AwayTeamCountry { get; set; }

        [JsonPropertyName("datetime")]
        public DateTimeOffset Datetime { get; set; }

        [JsonPropertyName("winner")]
        public string Winner { get; set; }

        [JsonPropertyName("winner_code")]
        public string WinnerCode { get; set; }

        [JsonPropertyName("home_team")]
        public Team HomeTeam { get; set; }

        [JsonPropertyName("away_team")]
        public Team AwayTeam { get; set; }

        [JsonPropertyName("home_team_events")]
        public List<TeamEvent> HomeTeamEvents { get; set; }

        [JsonPropertyName("away_team_events")]
        public List<TeamEvent> AwayTeamEvents { get; set; }

        [JsonPropertyName("home_team_statistics")]
        public TeamStatistics HomeTeamStatistics { get; set; }

        [JsonPropertyName("away_team_statistics")]
        public TeamStatistics AwayTeamStatistics { get; set; }

        [JsonPropertyName("last_event_update_at")]
        public DateTimeOffset LastEventUpdateAt { get; set; }

        [JsonPropertyName("last_score_update_at")]
        public DateTimeOffset? LastScoreUpdateAt { get; set; }
    }

    public partial class Team
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("goals")]
        public long? Goals { get; set; }

        [JsonPropertyName("penalties")]
        public long? Penalties { get; set; }
    }

    public partial class TeamEvent
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("type_of_event")]
        public TypeOfEvent TypeOfEvent { get; set; }

        [JsonPropertyName("player")]
        public string Player { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }
    }

    public partial class TeamStatistics
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("attempts_on_goal")]
        public long? AttemptsOnGoal { get; set; }

        [JsonPropertyName("on_target")]
        public long? OnTarget { get; set; }

        [JsonPropertyName("off_target")]
        public long? OffTarget { get; set; }

        [JsonPropertyName("blocked")]
        public long? Blocked { get; set; }

        [JsonPropertyName("woodwork")]
        public long? Woodwork { get; set; }

        [JsonPropertyName("corners")]
        public long? Corners { get; set; }

        [JsonPropertyName("offsides")]
        public long? Offsides { get; set; }

        [JsonPropertyName("ball_possession")]
        public long? BallPossession { get; set; }

        [JsonPropertyName("pass_accuracy")]
        public long? PassAccuracy { get; set; }

        [JsonPropertyName("num_passes")]
        public long? NumPasses { get; set; }

        [JsonPropertyName("passes_completed")]
        public long? PassesCompleted { get; set; }

        [JsonPropertyName("distance_covered")]
        public long? DistanceCovered { get; set; }

        [JsonPropertyName("balls_recovered")]
        public long? BallsRecovered { get; set; }

        [JsonPropertyName("tackles")]
        public long? Tackles { get; set; }

        [JsonPropertyName("clearances")]
        public long? Clearances { get; set; }

        [JsonPropertyName("yellow_cards")]
        public long? YellowCards { get; set; }

        [JsonPropertyName("red_cards")]
        public long? RedCards { get; set; }

        [JsonPropertyName("fouls_committed")]
        public long? FoulsCommitted { get; set; }

        [JsonPropertyName("tactics")]
        public Tactics Tactics { get; set; }

        [JsonPropertyName("starting_eleven")]
        public List<StartingEleven> StartingEleven { get; set; }

        [JsonPropertyName("substitutes")]
        public List<StartingEleven> Substitutes { get; set; }
    }

    public partial class StartingEleven
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("captain")]
        public bool Captain { get; set; }

        [JsonPropertyName("shirt_number")]
        public long? ShirtNumber { get; set; }

        [JsonPropertyName("position")]
        public Position Position { get; set; }
    }

    public partial class Weather
    {
        [JsonPropertyName("humidity")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Humidity { get; set; }

        [JsonPropertyName("temp_celsius")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TempCelsius { get; set; }

        [JsonPropertyName("temp_farenheit")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TempFarenheit { get; set; }

        [JsonPropertyName("wind_speed")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? WindSpeed { get; set; }

        [JsonPropertyName("description")]
        public Description Description { get; set; }
    }

    public enum TypeOfEvent { Goal, GoalOwn, GoalPenalty, RedCard, SubstitutionIn, SubstitutionOut, YellowCard, YellowCardSecond };

    public enum Position { Defender, Forward, Goalie, Midfield };

    public enum Tactics { The3421, The343, The352, The4231, The4321, The433, The442, The451, The532, The541 };

    public enum StageName { Final, FirstStage, PlayOffForThirdPlace, QuarterFinals, RoundOf16, SemiFinals };

    public enum Status { Completed };

    public enum Time { FullTime };

    public enum Description { ClearNight, Cloudy, PartlyCloudy, PartlyCloudyNight, Sunny, CloudyNight };

    public partial class Match
    {
        public static List<Match> FromJson(string json) => JsonSerializer.Deserialize<List<Match>>(json, WorldCup.DataLayer.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Match> self) => JsonSerializer.Serialize(self, WorldCup.DataLayer.Models.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerOptions Settings = new(JsonSerializerDefaults.General)
        {
            Converters =
            {
                TypeOfEventConverter.Singleton,
                PositionConverter.Singleton,
                TacticsConverter.Singleton,
                StageNameConverter.Singleton,
                StatusConverter.Singleton,
                TimeConverter.Singleton,
                DescriptionConverter.Singleton,
                new DateOnlyConverter(),
                new TimeOnlyConverter(),
                IsoDateTimeOffsetConverter.Singleton
            },
        };
    }

    internal class ParseStringConverter : JsonConverter<long>
    {
        public override bool CanConvert(Type t) => t == typeof(long);

        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.ToString(), options);
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class TypeOfEventConverter : JsonConverter<TypeOfEvent>
    {
        public override bool CanConvert(Type t) => t == typeof(TypeOfEvent);

        public override TypeOfEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "goal":
                    return TypeOfEvent.Goal;
                case "goal-own":
                    return TypeOfEvent.GoalOwn;
                case "goal-penalty":
                    return TypeOfEvent.GoalPenalty;
                case "red-card":
                    return TypeOfEvent.RedCard;
                case "substitution-in":
                    return TypeOfEvent.SubstitutionIn;
                case "substitution-out":
                    return TypeOfEvent.SubstitutionOut;
                case "yellow-card":
                    return TypeOfEvent.YellowCard;
                case "yellow-card-second":
                    return TypeOfEvent.YellowCardSecond;
            }
            throw new Exception("Cannot unmarshal type TypeOfEvent");
        }

        public override void Write(Utf8JsonWriter writer, TypeOfEvent value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case TypeOfEvent.Goal:
                    JsonSerializer.Serialize(writer, "goal", options);
                    return;
                case TypeOfEvent.GoalOwn:
                    JsonSerializer.Serialize(writer, "goal-own", options);
                    return;
                case TypeOfEvent.GoalPenalty:
                    JsonSerializer.Serialize(writer, "goal-penalty", options);
                    return;
                case TypeOfEvent.RedCard:
                    JsonSerializer.Serialize(writer, "red-card", options);
                    return;
                case TypeOfEvent.SubstitutionIn:
                    JsonSerializer.Serialize(writer, "substitution-in", options);
                    return;
                case TypeOfEvent.SubstitutionOut:
                    JsonSerializer.Serialize(writer, "substitution-out", options);
                    return;
                case TypeOfEvent.YellowCard:
                    JsonSerializer.Serialize(writer, "yellow-card", options);
                    return;
                case TypeOfEvent.YellowCardSecond:
                    JsonSerializer.Serialize(writer, "yellow-card-second", options);
                    return;
            }
            throw new Exception("Cannot marshal type TypeOfEvent");
        }

        public static readonly TypeOfEventConverter Singleton = new TypeOfEventConverter();
    }

    internal class PositionConverter : JsonConverter<Position>
    {
        public override bool CanConvert(Type t) => t == typeof(Position);

        public override Position Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "Defender":
                    return Position.Defender;
                case "Forward":
                    return Position.Forward;
                case "Goalie":
                    return Position.Goalie;
                case "Midfield":
                    return Position.Midfield;
            }
            throw new Exception("Cannot unmarshal type Position");
        }

        public override void Write(Utf8JsonWriter writer, Position value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case Position.Defender:
                    JsonSerializer.Serialize(writer, "Defender", options);
                    return;
                case Position.Forward:
                    JsonSerializer.Serialize(writer, "Forward", options);
                    return;
                case Position.Goalie:
                    JsonSerializer.Serialize(writer, "Goalie", options);
                    return;
                case Position.Midfield:
                    JsonSerializer.Serialize(writer, "Midfield", options);
                    return;
            }
            throw new Exception("Cannot marshal type Position");
        }

        public static readonly PositionConverter Singleton = new PositionConverter();
    }

    internal class TacticsConverter : JsonConverter<Tactics>
    {
        public override bool CanConvert(Type t) => t == typeof(Tactics);

        public override Tactics Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "3-4-2-1":
                    return Tactics.The3421;
                case "3-4-3":
                    return Tactics.The343;
                case "3-5-2":
                    return Tactics.The352;
                case "4-2-3-1":
                    return Tactics.The4231;
                case "4-3-2-1":
                    return Tactics.The4321;
                case "4-3-3":
                    return Tactics.The433;
                case "4-4-2":
                    return Tactics.The442;
                case "4-5-1":
                    return Tactics.The451;
                case "5-3-2":
                    return Tactics.The532;
                case "5-4-1":
                    return Tactics.The541;
            }
            throw new Exception("Cannot unmarshal type Tactics");
        }

        public override void Write(Utf8JsonWriter writer, Tactics value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case Tactics.The3421:
                    JsonSerializer.Serialize(writer, "3-4-2-1", options);
                    return;
                case Tactics.The343:
                    JsonSerializer.Serialize(writer, "3-4-3", options);
                    return;
                case Tactics.The352:
                    JsonSerializer.Serialize(writer, "3-5-2", options);
                    return;
                case Tactics.The4231:
                    JsonSerializer.Serialize(writer, "4-2-3-1", options);
                    return;
                case Tactics.The4321:
                    JsonSerializer.Serialize(writer, "4-3-2-1", options);
                    return;
                case Tactics.The433:
                    JsonSerializer.Serialize(writer, "4-3-3", options);
                    return;
                case Tactics.The442:
                    JsonSerializer.Serialize(writer, "4-4-2", options);
                    return;
                case Tactics.The451:
                    JsonSerializer.Serialize(writer, "4-5-1", options);
                    return;
                case Tactics.The532:
                    JsonSerializer.Serialize(writer, "5-3-2", options);
                    return;
                case Tactics.The541:
                    JsonSerializer.Serialize(writer, "5-4-1", options);
                    return;
            }
            throw new Exception("Cannot marshal type Tactics");
        }

        public static readonly TacticsConverter Singleton = new TacticsConverter();
    }

    internal class StageNameConverter : JsonConverter<StageName>
    {
        public override bool CanConvert(Type t) => t == typeof(StageName);

        public override StageName Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "Final":
                    return StageName.Final;
                case "First stage":
                case "First Stage":
                    return StageName.FirstStage;
                case "Play-off for third place":
                case "Match for third place":
                    return StageName.PlayOffForThirdPlace;
                case "Quarter-finals":
                case "Quarter-final":
                    return StageName.QuarterFinals;
                case "Round of 16":
                    return StageName.RoundOf16;
                case "Semi-finals":
                case "Semi-final":
                    return StageName.SemiFinals;
            }
            throw new Exception("Cannot unmarshal type StageName");
        }

        public override void Write(Utf8JsonWriter writer, StageName value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case StageName.Final:
                    JsonSerializer.Serialize(writer, "Final", options);
                    return;
                case StageName.FirstStage:
                    JsonSerializer.Serialize(writer, "First stage", options);
                    return;
                case StageName.PlayOffForThirdPlace:
                    JsonSerializer.Serialize(writer, "Play-off for third place", options);
                    return;
                case StageName.QuarterFinals:
                    JsonSerializer.Serialize(writer, "Quarter-finals", options);
                    return;
                case StageName.RoundOf16:
                    JsonSerializer.Serialize(writer, "Round of 16", options);
                    return;
                case StageName.SemiFinals:
                    JsonSerializer.Serialize(writer, "Semi-finals", options);
                    return;
            }
            throw new Exception("Cannot marshal type StageName");
        }

        public static readonly StageNameConverter Singleton = new StageNameConverter();
    }

    internal class StatusConverter : JsonConverter<Status>
    {
        public override bool CanConvert(Type t) => t == typeof(Status);

        public override Status Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == "completed")
            {
                return Status.Completed;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void Write(Utf8JsonWriter writer, Status value, JsonSerializerOptions options)
        {
            if (value == Status.Completed)
            {
                JsonSerializer.Serialize(writer, "completed", options);
                return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }

    internal class TimeConverter : JsonConverter<Time>
    {
        public override bool CanConvert(Type t) => t == typeof(Time);

        public override Time Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == "full-time")
            {
                return Time.FullTime;
            }
            throw new Exception("Cannot unmarshal type Time");
        }

        public override void Write(Utf8JsonWriter writer, Time value, JsonSerializerOptions options)
        {
            if (value == Time.FullTime)
            {
                JsonSerializer.Serialize(writer, "full-time", options);
                return;
            }
            throw new Exception("Cannot marshal type Time");
        }

        public static readonly TimeConverter Singleton = new TimeConverter();
    }

    internal class DescriptionConverter : JsonConverter<Description>
    {
        public override bool CanConvert(Type t) => t == typeof(Description);

        public override Description Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "Clear Night":
                case "Clear night":
                    return Description.ClearNight;
                case "Cloudy":
                    return Description.Cloudy;
                case "Partly Cloudy":
                    return Description.PartlyCloudy;
                case "Partly Cloudy Night":
                    return Description.PartlyCloudyNight;
                case "Sunny":
                    return Description.Sunny;
                case "Cloudy Night":
                case "Cloudy night":
                    return Description.ClearNight;
            }
            throw new Exception("Cannot unmarshal type Description");
        }

        public override void Write(Utf8JsonWriter writer, Description value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case Description.ClearNight:
                    JsonSerializer.Serialize(writer, "Clear Night", options);
                    return;
                case Description.Cloudy:
                    JsonSerializer.Serialize(writer, "Cloudy", options);
                    return;
                case Description.PartlyCloudy:
                    JsonSerializer.Serialize(writer, "Partly Cloudy", options);
                    return;
                case Description.PartlyCloudyNight:
                    JsonSerializer.Serialize(writer, "Partly Cloudy Night", options);
                    return;
                case Description.Sunny:
                    JsonSerializer.Serialize(writer, "Sunny", options);
                    return;
                case Description.CloudyNight:
                    JsonSerializer.Serialize(writer, "Cloudy Night", options);
                    return;
            }
            throw new Exception("Cannot marshal type Description");
        }

        public static readonly DescriptionConverter Singleton = new DescriptionConverter();
    }

    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        private readonly string serializationFormat;
        public DateOnlyConverter() : this(null) { }

        public DateOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
                => writer.WriteStringValue(value.ToString(serializationFormat));
    }

    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        private readonly string serializationFormat;

        public TimeOnlyConverter() : this(null) { }

        public TimeOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "HH:mm:ss.fff";
        }

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
                => writer.WriteStringValue(value.ToString(serializationFormat));
    }

    internal class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override bool CanConvert(Type t) => t == typeof(DateTimeOffset);

        private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

        private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
        private string? _dateTimeFormat;
        private CultureInfo? _culture;

        public DateTimeStyles DateTimeStyles
        {
            get => _dateTimeStyles;
            set => _dateTimeStyles = value;
        }

        public string? DateTimeFormat
        {
            get => _dateTimeFormat ?? string.Empty;
            set => _dateTimeFormat = (string.IsNullOrEmpty(value)) ? null : value;
        }

        public CultureInfo Culture
        {
            get => _culture ?? CultureInfo.CurrentCulture;
            set => _culture = value;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            string text;


            if ((_dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
                    || (_dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
            {
                value = value.ToUniversalTime();
            }

            text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);

            writer.WriteStringValue(text);
        }

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? dateText = reader.GetString();

            if (string.IsNullOrEmpty(dateText) == false)
            {
                if (!string.IsNullOrEmpty(_dateTimeFormat))
                {
                    return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);
                }
                else
                {
                    return DateTimeOffset.Parse(dateText, Culture, _dateTimeStyles);
                }
            }
            else
            {
                return default(DateTimeOffset);
            }
        }


        public static readonly IsoDateTimeOffsetConverter Singleton = new IsoDateTimeOffsetConverter();
    }
}
#pragma warning restore CS8618
#pragma warning restore CS8601
#pragma warning restore CS8603
