﻿namespace AnimalSanctuaryAPI.Dtos
{
    public sealed class ImageDto
    {
        public string Path { get; set; } = string.Empty;
        public Guid ContextId { get; set; }
    }
}