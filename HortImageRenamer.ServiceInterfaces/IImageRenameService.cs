namespace HortImageRenamer.ServiceInterfaces
{
  using HortImageRenamer.Domain;

  public interface IImageRenameService
  {
    void RenameImageAndUsages(PlantPhoto plantPhoto);
    ILogger Logger { get; set; }
  }
}