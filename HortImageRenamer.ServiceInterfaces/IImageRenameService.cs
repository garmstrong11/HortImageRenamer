namespace HortImageRenamer.ServiceInterfaces
{
  using System;
  using HortImageRenamer.Domain;

  public interface IImageRenameService
  {
    void RenameImage(PlantPhoto plantPhoto, DateTime modifiedDate);
    ILogger Logger { get; set; }
  }
}