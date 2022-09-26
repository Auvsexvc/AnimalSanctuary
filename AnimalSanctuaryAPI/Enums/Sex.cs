using System.Text.Json.Serialization;

namespace AnimalSanctuaryAPI.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sex
    {
        Female,

        Male
    }
}