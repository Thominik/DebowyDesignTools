using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;

namespace DebowyDesignTools.DataProviders;

public class ToolsProvider : IToolsProvider
{
    private readonly IRepository<Tool> _toolsRepository;

    public ToolsProvider(IRepository<Tool> toolsRepository)
    {
        _toolsRepository = toolsRepository;
    }

    public List<Tool> OrderByName()
    {
        var tools = _toolsRepository.GetAll();
        return tools.OrderBy(x => x.Name).ToList();
    }

    public List<Tool> WhereStartsWith(string prefix)
    {
        var tools = _toolsRepository.GetAll();
        return tools.Where(x => x.Name.StartsWith(prefix)).ToList();
    }
}