using ECommerceBackend.Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ECommerceBackend.Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorage
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public LocalStorage(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
    {
        var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> data = new();

        foreach (var file in files)
        {
            // var newFileName = await FileRenameAsync(uploadPath, file.FileName);

            await CopyFileAsync($"{uploadPath}\\{file.Name}", file);
            data.Add((file.Name, $"{path}\\{file.Name}"));
        }

        return data;

        //TODO: If the above "if" is not valid, a warning exception should be thrown here that an error is received while uploading files to the server!
    }

    public async Task DeleteAsync(string path, string fileName)
        => await Task.Run(() => File.Delete($"{path}\\{fileName}"));

    public List<string> GetFiles(string path)
    {
        var directoryInfo = new DirectoryInfo(path);
        return directoryInfo.GetFiles().Select(f => f.Name).ToList();
    }

    public bool HasFile(string path, string fileName)
        => File.Exists($"{path}\\{fileName}");

    private async Task<bool> CopyFileAsync(string path, IFormFile file)
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