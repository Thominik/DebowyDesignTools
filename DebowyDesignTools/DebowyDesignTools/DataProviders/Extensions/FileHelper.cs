using System.Text.RegularExpressions;
using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;

namespace DebowyDesignTools.DataProviders.Extensions;

public class FileHelper : IFileHelper
{
    public void LoadLastFile()
    {
        var directoryPath =
            @"C:\Users\Dudi\Documents\Projekty\DebowyDesignTools\DebowyDesignTools\bin\Debug\net6.0";
        var userFilePattern = @"\d{4}-\d{2}-\d{2}_\d{2}-\d{2}-\d{2}\.json";
        var latestUserFile = GetLatestUserFile(directoryPath, userFilePattern);

        if (latestUserFile != null)
        {
            Console.WriteLine();
            Console.WriteLine($"Last user file: {latestUserFile.Name}");
            Console.WriteLine();
            var fileWithoutExtension = Path.GetFileNameWithoutExtension(latestUserFile.Name);
            var fileRepo = new FileRepository<Tool>(fileWithoutExtension);
            PrintToConsole(fileRepo);
        }
        else
        {
            Console.WriteLine("User file not found!");
        }
    }

    static FileInfo? GetLatestUserFile(string directoryPath, string pattern)
    {
        var directoryInfo = new DirectoryInfo(directoryPath);
        var regex = new Regex(pattern);

        return directoryInfo.GetFiles("*.json")
            .Where(f => regex.IsMatch(f.Name))
            .OrderByDescending(f => f.LastWriteTime)
            .FirstOrDefault();
    }
    
    void PrintToConsole(IReadRepository<IEntity> repository)
    {
        var items = repository.GetAll();

        foreach (var item in items)
        {
            Console.WriteLine(item);
        }
    }
}