namespace HortImageRenamer.ServiceInterfaces
{
  using System;
  using System.Collections.Generic;
  using HortImageRenamer.Core;
  using HortImageRenamer.Domain;

  public interface IPlantPhotoService
  {
    IEnumerable<PlantPhoto> GetRenameCandidates();

    int RenamePlantPhoto(PlantPhoto photo, DateTime modifiedDate);

    Maybe<PlantPhoto> FindById(string photoId);
  }
}