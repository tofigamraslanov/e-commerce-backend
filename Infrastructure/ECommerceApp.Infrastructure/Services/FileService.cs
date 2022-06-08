using ECommerceApp.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceApp.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
    {
        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> data = new();
        List<bool> results = new();

        foreach (var file in files)
        {
            var fileNewName = await RenameFileAsync(file.FileName);

            var result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
            data.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
            results.Add(result);
        }

        if (results.TrueForAll(r => r.Equals(true)))
            return data;

        return null;

        //TODO: If the above "if" is not valid, a warning exception should be thrown here that an error is received while uploading files to the server!
    }

    public async Task<string> RenameFileAsync(string fileName)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            await using var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;
        }
        catch (Exception e)
        {
            //TODO: Log!
            throw e;
        }
    }
}