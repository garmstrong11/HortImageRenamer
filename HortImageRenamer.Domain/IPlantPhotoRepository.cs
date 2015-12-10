namespace HortImageRenamer.Domain
{
  using System;
  using System.Collections.Generic;
  using HortImageRenamer.Core;

  public interface IPlantPhotoRepository
  {
    IEnumerable<PlantPhoto> GetPlantPhotosForRename();

    int UpdatePlantPhotoId(string photoId, DateTime modifiedDate);

    Maybe<PlantPhoto> FindById(string photoId);
  }
}