using DebowyDesignTools;
using DebowyDesignTools.DataProviders;
using DebowyDesignTools.DataProviders.Extensions;
using DebowyDesignTools.Entities;
using DebowyDesignTools.Repositories;
using DebowyDesignTools.Services;
using Microsoft.Extensions.DependencyInjection;

var todayPath = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

var services = new ServiceCollection();

services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Tool>>(sp => {
    var repo = new FileRepository<Tool>(todayPath);
    repo.Load(); 
    return repo;
});
services.AddSingleton<IToolsProvider, ToolsProvider>();
services.AddSingleton<IFileHelper, FileHelper>();
services.AddSingleton<IUserCommunication, UserCommunication>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();