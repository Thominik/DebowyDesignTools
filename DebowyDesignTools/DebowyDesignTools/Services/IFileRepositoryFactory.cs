using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;

namespace DebowyDesignTools.Services;

public interface IFileRepositoryFactory
{
    IRepository<Tool> Create(string filePath);
}