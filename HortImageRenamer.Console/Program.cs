namespace HortImageRenamer.Console
{
  using System;
  using HortImageRenamer.Core;
  using HortImageRenamer.Logging;
  using HortImageRenamer.ServiceImplementations;
  using HortImageRenamer.ServiceInterfaces;
  using NLog;
  using NLog.Config;
  using NLog.Targets;
  using SimpleInjector;
  using ILogger = HortImageRenamer.Domain.ILogger;

  class Program
  {
    static void Main(string[] args)
    {
      LogManager.Configuration = ConfigureLogger();
      var container = ConfigureContainer();
      var count = 100;
      var opsManager = container.GetInstance<IRenameOperationsManager>();
      opsManager.Initialize();
     // opsManager.RenameCount(count);
      opsManager.RenameAll();
      //opsManager.RenameSingle("1746");

      Console.ReadLine();
    }

    private static Container ConfigureContainer()
    {
      var container = new Container();

      // Reset this line to reconfigure for production
      container.RegisterSingleton<ISettingsService>(new TestSettingsService());
      //container.RegisterSingleton<ISettingsService>(new ProductionSettingsService());

      container.Register<ILogger, NlogLoggerAdapter>();
      container.RegisterSingleton<IPlantPhotoService, PlantPhotoService>();
      container.RegisterSingleton<IImageRenameService, ImageRenameService>();
      container.RegisterSingleton<IRenameOperationsManager, RenameOperationsManager>();

      container.RegisterInitializer<IImageRenameService>(svc =>
      {
        svc.Logger = new NlogLoggerAdapter(LogManager.GetLogger("logFile"));
      });

      container.RegisterInitializer<IRenameOperationsManager>(mgr =>
      {
        mgr.ConsoleLogger = new NlogLoggerAdapter(LogManager.GetLogger("logConsole"));
      });

      return container;
    }

    private static LoggingConfiguration ConfigureLogger()
    {
      var config = new LoggingConfiguration();
      var fileTarget = new FileTarget
      {
        Name = "logFile",
        FileName = @"\\Storage1\Users\garmstrong\RenameLogs\SimpleLog.txt",
        Layout = "[${level}] ${message}",
        KeepFileOpen = true,
        //DeleteOldFileOnStartup = true,
      };

      var consoleTarget = new ConsoleTarget {Name = "logConsole"};

      var fileRule = new LoggingRule("logFile", LogLevel.Debug, fileTarget);
      var consoleRule = new LoggingRule("logConsole", LogLevel.Debug, consoleTarget);

      config.AddTarget(fileTarget);
      config.AddTarget(consoleTarget);
      config.LoggingRules.Add(fileRule);
      config.LoggingRules.Add(consoleRule);

      return config;
    }
  }
}
