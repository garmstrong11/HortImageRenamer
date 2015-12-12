namespace HortImageRenamer.ServiceInterfaces
{
  using System.Collections.Generic;
  using HortImageRenamer.Domain;

  public interface IPlantPhotoService
  {
    //IEnumerable<PlantPhoto> GetRenameCandidates();


    void Initialize();

    IEnumerable<PlantPhoto> PlantPhotos { get; }

    //Maybe<PlantPhoto> FindById(string photoId);

    bool TryFindPlantPhoto(string photoId, out PlantPhoto plantPhoto);
  }
}