using DebowyDesignTools.Entities;

namespace DebowyDesignTools.DataProviders;

public interface IToolsProvider
{
    List<Tool> OrderByName(List<Tool> tools);
    List<Tool> WhereBrandStartsWith(List<Tool> tools, string prefix);
}