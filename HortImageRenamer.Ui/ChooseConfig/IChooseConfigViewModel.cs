namespace HortImageRenamer.Ui.ViewModels
{
  using Caliburn.Micro;

  public interface IChooseConfigViewModel : IScreen
  {
    bool IsTest { get; set; }
    bool IsProduction { get; set; }
  }
}