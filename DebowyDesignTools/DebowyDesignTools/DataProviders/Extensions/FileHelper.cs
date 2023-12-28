using System.Text.RegularExpressions;
using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;
using DebowyDesignTools.Services;

namespace DebowyDesignTools.DataProviders.Extensions;

public class FileHelper : IFileHelper
{
    public List<Tool> LoadLastFile()
    {
        var userFilePattern = @"\d{4}-\d{2}-\d{2}_\d{2}-\d{2}-\d{2}\.json";
        var latestUserFile = PathSettings.GetLatestUserFile(userFilePattern);

        if (latestUserFile != null)
        {
            Console.WriteLine();
            Console.WriteLine($"Last user file: {latestUserFile.Name}");
            Console.WriteLine();
            var fileWithoutExtension = Path.GetFileNameWithoutExtension(latestUserFile.Name);
            var fileRepo = new FileRepository<Tool>(fileWithoutExtension);
            return fileRepo.GetAll().ToList();
        }

        Console.WriteLine("User file not found!");
        return new List<Tool>();
    }
}