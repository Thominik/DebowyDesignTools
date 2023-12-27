using DebowyDesignTools.Entities;

namespace DebowyDesignTools.DataProviders.Extensions;

public interface IFileHelper
{
    List<Tool> LoadLastFile();
}