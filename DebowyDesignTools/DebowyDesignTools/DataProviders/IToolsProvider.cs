using DebowyDesignTools.Entities;

namespace DebowyDesignTools.DataProviders;

public interface IToolsProvider
{
    List<Tool> OrderByName();
    List<Tool> WhereStartsWith(string prefix);
}