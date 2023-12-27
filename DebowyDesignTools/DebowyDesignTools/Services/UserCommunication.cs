using DebowyDesignTools.DataProviders;
using DebowyDesignTools.DataProviders.Extensions;
using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;

namespace DebowyDesignTools.Services;

public class UserCommunication : IUserCommunication
{
    private readonly IToolsProvider _toolsProvider;
    private readonly IFileHelper _fileHelper;

    public UserCommunication
    (
        IToolsProvider toolsProvider,
        IFileHelper fileHelper
    )
    {
        _toolsProvider = toolsProvider;
        _fileHelper = fileHelper;
    }

    public void CommunicationWithUser()
    {
        #region old Program.cs

        WelcomeScreenPrint();
        
        Console.ReadKey();

        var toolRepo = new FileRepository<Tool>(CurrentDateTimeString());
        toolRepo.ItemAdded += ToolRepositoryOnItemAdded;
        toolRepo.ItemRemoved += ToolRepositoryOnItemRemoved;

        Console.Clear();
        bool exitProgram = false;
        while (!exitProgram)
        {
            Menu();
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AddTool(toolRepo);
                    break;
                case "2":
                    Console.Clear();
                    PrintToConsole(toolRepo);
                    RemoveTool(toolRepo);
                    break;
                case "3":
                    DayInputPrint();
                    var day = Console.ReadLine();
                    var fileRepo = new FileRepository<Tool>(day);
                    PrintToConsole(fileRepo);
                    break;
                case "4":
                    Console.Clear();
                    var toolLoad = _fileHelper.LoadLastFile();
                    var orderedTools = _toolsProvider.OrderByName(toolLoad);
                    PrintToConsoleWithLinq(orderedTools);
                    break;
                case "5":
                    Console.Clear();
                    toolLoad = _fileHelper.LoadLastFile();
                    BrandInputPrint();
                    var brand = Console.ReadLine();
                    var toolsWithPrefix = _toolsProvider.WhereBrandStartsWith(toolLoad, brand);
                    PrintToConsoleWithLinq(toolsWithPrefix);
                    break;
                case "q":
                    exitProgram = true;
                    break;
                default:
                    Console.WriteLine("Incorrect selection, please try again.");
                    break;
            }
        }

        static void WelcomeScreenPrint()
        {
            Console.WriteLine("Welcome to the program for monitoring the inventory of equipment in the DębowyDesign " +
                              "carpentry shop!");
            Console.WriteLine("Remember that if you want to exit, type 'q'");
            Console.WriteLine("Ready? (confirm with any key)");
            Console.WriteLine();
        }

        static void Menu()
        {
            Console.WriteLine("Select what you want to do (confirm with enter):");
            Console.WriteLine("1. Add equipment to the warehouse");
            Console.WriteLine("2. Remove equipment from storage");
            Console.WriteLine("3. View the stock level for the day");
            Console.WriteLine("4. Order by name (last day)");
            Console.WriteLine("5. Tool where prefix (last day)");
            Console.WriteLine();
        }

        static void DayInputPrint()
        {
            Console.Clear();
            Console.WriteLine("Enter the day for stock (format: YYYY-MM-DD_HH-MM-SS):");
            Console.WriteLine();
        }
        
        static void BrandInputPrint()
        {
            Console.Clear();
            Console.WriteLine("Enter the brand prefix:");
            Console.WriteLine();
        }

        void PrintToConsole(IReadRepository<IEntity> repository)
        {
            var items = repository.GetAll();

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
        
        void PrintToConsoleWithLinq(List<Tool> tools)
        {
            foreach (var tool in tools)
            {
                Console.WriteLine(tool);
            }
        }

        static void AddTool(FileRepository<Tool> toolRepo)
        {
            Console.WriteLine("Adding a new tool");

            Console.Write("Enter the name: ");
            string? name = Console.ReadLine();

            Console.Write("Enter the brand: ");
            string? brand = Console.ReadLine();

            Console.Write("Enter the model ");
            string? model = Console.ReadLine();

            Console.Write("Is this a power tool? (yes/no): ");
            bool isPowerTool = Console.ReadLine().ToLower() == "yes";

            Tool tool;
            if (isPowerTool)
            {
                Console.Write("Enter battery type: ");
                string battery = Console.ReadLine();

                tool = new PowerTool { Brand = brand, Name = name, Model = model, Battery = battery };
            }
            else
            {
                tool = new Tool { Brand = brand, Name = name, Model = model };
            }

            toolRepo.Add(tool);
            toolRepo.Save();

            Console.WriteLine("Tool added successfully.");
        }

        static void RemoveTool(FileRepository<Tool> toolRepo)
        {
            Console.Write("Enter the ID of the tool to remove: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var tool = toolRepo.GetById(id);
            if (tool != null)
            {
                toolRepo.Remove(tool);
                toolRepo.Save();
                Console.WriteLine("Tool removed successfully.");
            }
            else
            {
                Console.WriteLine("No tool with this ID was found.");
            }
        }

        static void ToolRepositoryOnItemAdded(object? sender, Tool tool)
        {
            Console.WriteLine($"{CurrentDateTime()} --- Tool added => {tool.Name}");
        }

        static void ToolRepositoryOnItemRemoved(object? sender, Tool tool)
        {
            Console.WriteLine($"{CurrentDateTime()} --- Tool removed => {tool.Name}");
        }

        static DateTime CurrentDateTime()
        {
            return DateTime.Now;
        }

        static string CurrentDateTimeString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        }
        #endregion
    }
}