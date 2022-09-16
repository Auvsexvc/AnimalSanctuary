using System.Text.Json.Serialization;

namespace AnimalSanctuaryAPI.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Order
    {
        Asc,
        Desc
    }
}
