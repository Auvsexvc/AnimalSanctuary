using System.Text.Json.Serialization;

namespace WebClientApp.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Attitude
    {
        Aggressive,

        Friendly,

        Intolerant,

        Tolerant,

        Solitary
    }
}