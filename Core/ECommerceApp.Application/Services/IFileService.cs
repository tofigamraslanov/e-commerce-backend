using Microsoft.AspNetCore.Http;

namespace ECommerceApp.Application.Services;

public interface IFileService
{
    Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);
    Task<string> RenameFileAsync(string fileName);
    Task<bool> CopyFileAsync(string path, IFormFile file);
}