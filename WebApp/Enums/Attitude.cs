using System.Text.Json.Serialization;

namespace WebApp.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Attitude
    {
        Aggressive,

        Friendly,

        Tolerant,

        Intolerant,

        Solitary
    }
}