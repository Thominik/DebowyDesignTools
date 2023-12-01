using DebowyDesignTools.Data;
using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;

var toolRepo = new SqlRepository<Tool>(new DebowyDesignToolsDbContext());

AddTools(toolRepo);
AddHandTools(toolRepo);
PrintToConsole(toolRepo);


static void AddTools(IRepository<Tool> toolRepo)
{
    toolRepo.Add(new PowerTool {Brand = "Makita", Name = "Screwdriver", Model = "DHP450", Battery = "18v"});
    toolRepo.Add(new PowerTool {Brand = "Scheppach", Name = "Saw", Model = "DCP900"});
    toolRepo.Add(new PowerTool {Brand = "Makita", Name = "MillingMachine", Model = "DSC100", Battery = "18v"});
}

void AddHandTools(IWriteRepository<HandTool> handToolRepo)
{
    handToolRepo.Add(new HandTool {Brand = "Fiskars", Name = "Hammer"});
    handToolRepo.Save();
}

void PrintToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();

    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}
