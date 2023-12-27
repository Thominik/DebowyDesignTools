using DebowyDesignTools.DataProviders.Extensions;
using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;

namespace DebowyDesignTools.DataProviders;

public class ToolsProvider : IToolsProvider
{
    public List<Tool> OrderByName(List<Tool> tools)
    {
        return tools.OrderBy(x => x.Name).ToList();
    }

    public List<Tool> WhereBrandStartsWith(List<Tool> tools, string prefix)
    {
        return tools.Where(x => x.Brand.StartsWith(prefix)).ToList();
    }
}