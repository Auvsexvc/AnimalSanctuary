using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AnimalSanctuaryAPI.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sex
    {
        Male,

        Female
    }
}