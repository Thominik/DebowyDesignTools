using DebowyDesignTools.Entities;
using Microsoft.EntityFrameworkCore;

namespace DebowyDesignTools.Data;

public class DebowyDesignToolsDbContext : DbContext
{
    public DbSet<Tool> Tools
        => Set<Tool>();

    public DbSet<HandTool> HandTools 
        => Set<HandTool>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseInMemoryDatabase("DebowyDesignTools");
    }
}