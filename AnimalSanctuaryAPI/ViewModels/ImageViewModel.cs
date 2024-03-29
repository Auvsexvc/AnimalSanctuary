﻿namespace AnimalSanctuaryAPI.ViewModels
{
    public sealed class ImageViewModel
    {
        public Guid Id { get; set; }

        public string Path { get; set; } = string.Empty;

        public Guid ContextId { get; set; }
    }
}