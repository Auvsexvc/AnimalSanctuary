using System.Text.Json.Serialization;

namespace WebApp.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sex
    {
        Female,

        Male
    }
}