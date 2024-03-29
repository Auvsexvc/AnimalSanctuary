﻿using System.Text.Json.Serialization;

namespace AnimalSanctuaryAPI.Enums
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