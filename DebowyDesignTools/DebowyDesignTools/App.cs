using System.Text.RegularExpressions;
using DebowyDesignTools.DataProviders;
using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;
using DebowyDesignTools.Services;

namespace DebowyDesignTools;

public class App : IApp
{
    private readonly IUserCommunication _userCommunication;

    public App(IUserCommunication userCommunication)
    {
        _userCommunication = userCommunication;
    }

    public void Run()
    {
        LoadLastFile();
        Console.WriteLine();
        _userCommunication.CommunicationWithUser();
    }
    
    void LoadLastFile()
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
            PrintToConsole(fileRepo);
        }
        else
        {
            Console.WriteLine("User file not found!");
        }
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