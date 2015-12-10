namespace HortImageRenamer.ServiceInterfaces
{
  using HortImageRenamer.Domain;

  public interface ITransactionalImageRenamer
  {
    void RenameImageAndUsages(PlantPhoto plantPhoto);
    ILogger Logger { get; set; }
  }
}