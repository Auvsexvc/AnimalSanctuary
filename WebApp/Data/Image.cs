﻿namespace WebClientApp.Data
{
    public sealed class Image
    {
        public Guid Id { get; set; }

        public string Path { get; set; } = string.Empty;

        public Guid ContextId { get; set; }
    }
}