﻿namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IDbInitializer
    {
        Task EnsureDbCreatedIfPossibleAsync();
    }
}