using DebowyDesignTools.Data;
using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;
using DebowyDesignTools.Repositories.Extensions;

var toolRepo = new SqlRepository<Tool>(new DebowyDesignToolsDbContext());
toolRepo.ItemAdded += ToolRepositoryOnItemAdded;
toolRepo.ItemRemoved += ToolRepositoryOnItemRemoved;

static void ToolRepositoryOnItemAdded(object? sender, Tool tool)
{
    Console.WriteLine($"{CurrentDateTime()} --- Tool added => {tool.Name}");
}

static void ToolRepositoryOnItemRemoved(object? sender, Tool tool)
{
    Console.WriteLine($"{CurrentDateTime()} --- Tool removed => {tool.Name}");
}

AddTools(toolRepo);
RemoveTools(toolRepo);
PrintToConsole(toolRepo);

static void AddTools(IRepository<Tool> toolRepo)
{
    var tools = new[]
    {
        new PowerTool {Brand = "Makita", Name = "Screwdriver", Model = "DHP450", Battery = "18v"},
        new Tool {Brand = "Scheppach", Name = "Saw", Model = "DCP900"},
        new PowerTool {Brand = "Makita", Name = "MillingMachine", Model = "DSC100", Battery = "18v"},
        new HandTool {Brand = "Fiskars", Name = "Hammer"}
    };
    
    toolRepo.AddBatch(tools);
}

static void RemoveTools(IRepository<Tool> toolRepo)
{
    var toolId = toolRepo.GetById(2);
    toolRepo.Remove(toolId);
}

void PrintToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();

    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

static DateTime CurrentDateTime()
{
    return DateTime.Now;
}
