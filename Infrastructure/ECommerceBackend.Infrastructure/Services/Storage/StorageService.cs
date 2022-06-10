﻿using ECommerceBackend.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace ECommerceBackend.Infrastructure.Services.Storage;

public class StorageService : IStorageService
{
    private readonly IStorage _storage;

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }
    public string StorageName => _storage.GetType().Name;

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName,
        IFormFileCollection files)
        => await _storage.UploadAsync(pathOrContainerName, files);

    public async Task DeleteAsync(string pathOrContainerName, string fileName)
        => await _storage.DeleteAsync(pathOrContainerName, fileName);

    public List<string> GetFiles(string pathOrContainerName)
        => _storage.GetFiles(pathOrContainerName);

    public bool HasFile(string pathOrContainerName, string fileName)
        => _storage.HasFile(pathOrContainerName, fileName);
}