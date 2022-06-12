using ECommerceBackend.Infrastructure.Operations;
using Microsoft.Extensions.Configuration;

namespace ECommerceBackend.Infrastructure.Services.Storage;

public abstract class Storage
{
    protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, Func<string, string, bool> hasFileMethod, bool isFirst = true)
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
                        if (indexOfDash != -1) continue;
                        indexOfDash = lastIndex;
                        break;
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

            if (hasFileMethod(pathOrContainerName, newFileName))
                return await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod, false);

            return newFileName;
        });

        return newFileName;
    }
}