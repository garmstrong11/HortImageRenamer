namespace HortImageRenamer.Ui.ChooseConfig
{
  using System;
  using System.Linq;
  using Caliburn.Micro;
  using HortImageRenamer.Core;
  using HortImageRenamer.ServiceInterfaces;
  using HortImageRenamer.Ui.Config;
  using HortImageRenamer.Ui.ViewModels;

  public class ChooseConfigViewModel : Screen, IChooseConfigViewModel
  {
    private readonly ISettingsService _settings;
    private readonly IRenameOperationsManager _manager;
    private bool _isTest;
    private bool _isProduction;
    private string _thumbDir;

    public ChooseConfigViewModel(ISettingsService settings, IRenameOperationsManager manager)
    {
      if (settings == null) throw new ArgumentNullException("settings");
      if (manager == null) throw new ArgumentNullException("manager");

      _settings = settings;
      _manager = manager;
    }

    protected override void OnActivate()
    {
      IsTest = true;
    }

    private void UpdateWindowName(string configName)
    {
      var parentScreen = (Screen) Parent;
      parentScreen.DisplayName = string.Format("HortImage Rename - {0}", configName);
    }

    public void Initialize()
    {
      _manager.Initialize();
    }

    public bool IsTest
    {
      get { return _isTest; }
      set
      {
        if (value == _isTest) return;
        _isTest = value;

        _settings.TargetExtension = TestRenameConfig.TargetExtension;
        _settings.ImageRoots = TestRenameConfig.ImageRoots;
        _settings.ThumbnailRoots = TestRenameConfig.ThumbnailRoots;
        _settings.ConnectionString = TestRenameConfig.ConnectionString;

        if (IsTest) {
          UpdateWindowName("Test");
          ThumbDir = _settings.ThumbnailRoots.First();
        }

        NotifyOfPropertyChange();
      }
    }

    public bool IsProduction
    {
      get { return _isProduction; }
      set
      {
        if (value == _isProduction) return;
        _isProduction = value;

        _settings.TargetExtension = ProductionRenameConfig.TargetExtension;
        _settings.ImageRoots = ProductionRenameConfig.ImageRoots;
        _settings.ThumbnailRoots = ProductionRenameConfig.ThumbnailRoots;
        _settings.ConnectionString = ProductionRenameConfig.ConnectionString;

        if (IsProduction) {
          UpdateWindowName("Production");
          ThumbDir = _settings.ThumbnailRoots.First();
        }

        NotifyOfPropertyChange();
      }
    }

    public string ThumbDir
    {
      get { return _thumbDir; } 
      set
      {
        if (value == _thumbDir) return;
        _thumbDir = value;
        NotifyOfPropertyChange();
      }
    }
  }
}