using System.Text.Json.Serialization;

namespace WebClientApp.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sex
    {
        Female,

        Male
    }
}