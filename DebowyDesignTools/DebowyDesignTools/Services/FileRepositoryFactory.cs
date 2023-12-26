using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;

namespace DebowyDesignTools.Services;

public class FileRepositoryFactory : IFileRepositoryFactory
{
    public IRepository<Tool> Create(string filePath)
    {
        return new FileRepository<Tool>(filePath);
    }
}