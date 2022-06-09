using ECommerceApp.Application.Services;
using ECommerceApp.Infrastructure.Operations;
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
            var newFileName = await FileRenameAsync(uploadPath, file.FileName);

            var result = await CopyFileAsync($"{uploadPath}\\{newFileName}", file);
            data.Add((newFileName, $"{uploadPath}\\{newFileName}"));
            results.Add(result);
        }

        if (results.TrueForAll(r => r.Equals(true)))
            return data;

        return null;

        //TODO: If the above "if" is not valid, a warning exception should be thrown here that an error is received while uploading files to the server!
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

    private async Task<string> FileRenameAsync(string path, string fileName, bool isFirst = true)
    {
        var newFileName = await Task.Run(async () =>
         {
             var extension = Path.GetExtension(fileName);

             string newFileName;
             if (isFirst)
             {
                 var oldName = Path.GetFileNameWithoutExtension(fileName);
                 newFileName = $"{NameOperation.CharacterRegulator(oldName)}{extension}";
             }
             else
             {
                 newFileName = fileName;
                 var indexOfDash = newFileName.IndexOf('-');
                 if (indexOfDash == -1)
                 {
                     newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                 }
                 else
                 {
                     while (true)
                     {
                         var lastIndex = indexOfDash;
                         indexOfDash = newFileName.IndexOf('-', indexOfDash + 1);
                         if (indexOfDash == -1)
                         {
                             indexOfDash = lastIndex;
                             break;
                         }
                     }

                     var indexOfDot = newFileName.IndexOf('.');
                     var fileNumber = newFileName.Substring(indexOfDash + 1, indexOfDot - indexOfDash - 1);

                     if (int.TryParse(fileNumber, out var parsedFileNumber))
                     {
                         parsedFileNumber++;
                         newFileName = newFileName.Remove(indexOfDash + 1, indexOfDot - indexOfDash - 1)
                             .Insert(indexOfDash + 1, parsedFileNumber.ToString());
                     }
                     else
                     {
                         newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                     }
                 }

             }

             if (File.Exists($"{path}\\{newFileName}"))
                 return await FileRenameAsync(path, newFileName, false);

             return newFileName;
         });

        return newFileName;
    }
}