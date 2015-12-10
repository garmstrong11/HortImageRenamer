namespace HortImageRenamer.Console
{
  using System;
  using System.Linq;
  using HortImageRenamer.Core;
  using HortImageRenamer.DapperRepositories;
  using HortImageRenamer.Domain;
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
      var log = LogManager.GetLogger("logConsole");

      var container = ConfigureContainer();
      var count = 1;
      var libSvc = container.GetInstance<IPlantPhotoService>();
      var renameSvc = container.GetInstance<ITransactionalImageRenamer>();

      var candidates = libSvc.GetRenameCandidates().Take(count).ToList();
      log.Info("There are {0} instances to rename", candidates.Count);
      var counter = 1;

      foreach (var plantPhoto in candidates) {
        renameSvc.RenameImageAndUsages(plantPhoto);
        log.Info("Renamed {0} of {1}", counter++, count);
      }

      Console.ReadLine();
    }

    private static Container ConfigureContainer()
    {
      var container = new Container();

      // Reset this line to reconfigure for production
      container.RegisterSingleton<ISettingsService>(new TestSettingsService());
      //container.RegisterSingleton<ISettingsService>(new ProductionSettingsService());

      container.Register<ILogger, NlogLoggerAdapter>();
      container.RegisterSingleton<IPlantLibraryRepository, DapperPlantLibraryRepository>();
      container.RegisterSingleton<IPlantPhotoRepository, DapperPlantPhotoRepository>();
      container.RegisterSingleton<IPlantFieldUsageRepository, DapperPlantFieldUsageRepository>();
      container.RegisterSingleton<IPlantLibraryService, PlantLibraryService>();
      container.RegisterSingleton<IPlantPhotoService, PlantPhotoService>();
      container.RegisterSingleton<IPlantFieldUsageService, PlantFieldUsageService>();
      container.RegisterSingleton<ITransactionalImageRenamer, TransactionalImageRenamer>();

      container.RegisterInitializer<ITransactionalImageRenamer>(svc =>
      {
        svc.Logger = new NlogLoggerAdapter(LogManager.GetLogger("logFile"));
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
