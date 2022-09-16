using System.Text.Json.Serialization;

namespace WebApp.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Order
    {
        Asc,
        Desc
    }
}
