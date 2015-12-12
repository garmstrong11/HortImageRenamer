namespace HortImageRenamer.Ui.Infra
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Windows;
  using Caliburn.Micro;
  using HortImageRenamer.Core;
  using HortImageRenamer.Logging;
  using HortImageRenamer.ServiceImplementations;
  using HortImageRenamer.ServiceInterfaces;
  using HortImageRenamer.Ui.ChooseConfig;
  using HortImageRenamer.Ui.Config;
  using HortImageRenamer.Ui.Shell;
  using HortImageRenamer.Ui.ViewModels;
  using NLog;
  using NLog.Config;
  using NLog.Targets;
  using SimpleInjector;
  using ILogger = HortImageRenamer.Domain.ILogger;
  using LogManager = Caliburn.Micro.LogManager;

  public class SiBootstrapper : BootstrapperBase
  {
    private Container _container;

    public SiBootstrapper()
    {
      LogManager.GetLog = type => new DebugLogger(type);
      Initialize();
    }

    protected override void Configure()
    {
      NLog.LogManager.Configuration = ConfigureLogger();
      _container = new Container();

      _container.RegisterSingleton<IEventAggregator, EventAggregator>();
      _container.Register<IShell, ShellViewModel>();
      _container.RegisterSingleton<ISettingsService, SettingsService>();

      _container.Register<ILogger, NlogLoggerAdapter>();
      _container.Register<IChooseConfigViewModel, ChooseConfigViewModel>();
      _container.RegisterSingleton<IPlantPhotoService, PlantPhotoService>();
      _container.RegisterSingleton<IImageRenameService, ImageRenameService>();
      _container.RegisterSingleton<IRenameOperationsManager, RenameOperationsManager>();

      _container.RegisterInitializer<IImageRenameService>(svc =>
      {
        svc.Logger = new NlogLoggerAdapter(NLog.LogManager.GetLogger("logFile"));
      });

      _container.RegisterInitializer<IRenameOperationsManager>(mgr =>
      {
        mgr.ConsoleLogger = new NlogLoggerAdapter(NLog.LogManager.GetLogger("logConsole"));
      });

      _container.Register<IWindowManager, WindowManager>();

      //_container.Verify();

    }

    protected override void OnStartup(object sender, StartupEventArgs e)
    {
      DisplayRootViewFor<IShell>();
    }

    protected override object GetInstance(Type service, string key)
    {
      return _container.GetInstance(service);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
      IServiceProvider provider = _container;
      var collectionType = typeof (IEnumerable<>).MakeGenericType(service);
      var services = (IEnumerable<object>) provider.GetService(collectionType);

      return services ?? Enumerable.Empty<object>();
    }

    protected override void BuildUp(object instance)
    {
      var registration = _container.GetRegistration(instance.GetType(), true);
      registration.Registration.InitializeInstance(instance);
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

      var consoleTarget = new ConsoleTarget { Name = "logConsole" };

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