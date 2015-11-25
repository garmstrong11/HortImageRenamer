namespace HortImageRenamer.Domain
{
  using System.Collections.Generic;

  public interface IPlantPhotoRepository
  {
    IEnumerable<PlantPhoto> GetPlantPhotosForRename();

    int UpdatePlantPhotoId(string photoId);
  }
}