namespace HortImageRenamer.Ui.Shell
{
  using Caliburn.Micro;
  using HortImageRenamer.Ui.ViewModels;

  public class ShellViewModel : Conductor<IScreen>, IShell
  {
    private readonly IWindowManager _windowManager;

    public ShellViewModel(IWindowManager windowManager)
    {
      _windowManager = windowManager;
    }

    protected override void OnActivate()
    {
      DisplayName = "Hort Image Renamer";
      ActivateItem(IoC.Get<IChooseConfigViewModel>());
    }
  }
}