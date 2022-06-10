using ECommerceBackend.Infrastructure.Operations;

namespace ECommerceBackend.Infrastructure.Services;

public class FileService
{
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