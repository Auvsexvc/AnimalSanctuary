using System.Text.Json.Serialization;

namespace WebApp.Data
{
    public sealed class Account
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("RoleName")]
        public string Role { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public DateTime ValidTo { get; set; }
    }
}