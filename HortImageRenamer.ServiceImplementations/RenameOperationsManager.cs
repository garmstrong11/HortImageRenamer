namespace HortImageRenamer.ServiceImplementations
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class RenameOperationsManager : IRenameOperationsManager
  {
    private readonly IImageRenameService _imageRenameSvc;
    private readonly IPlantPhotoService _plantPhotoSvc;

    public RenameOperationsManager(IImageRenameService imageRenameSvc, IPlantPhotoService plantPhotoSvc)
    {
      _imageRenameSvc = imageRenameSvc;
      _plantPhotoSvc = plantPhotoSvc;
    }

    #region Implementation of IRenameOperationsManager

    public void Initialize()
    {
      _plantPhotoSvc.Initialize();
    }

    public void RenameAll()
    {
      foreach (var plantPhoto in _plantPhotoSvc.PlantPhotos) {
        _imageRenameSvc.RenameImageAndUsages(plantPhoto);
      }
    }

    public void RenameList(IEnumerable<string> photoIds)
    {
      if (photoIds == null) throw new ArgumentNullException("photoIds");
      var photoList = photoIds.ToList();

      var counter = 1;

      foreach (var photoId in photoList) {
        PlantPhoto foundPlantPhoto;

        if (!_plantPhotoSvc.TryFindPlantPhoto(photoId, out foundPlantPhoto)) {
          ConsoleLogger.Error(
            string.Format("{0} of {1}: Unable to find the PhotoID '{2}' in the database", counter++, photoList.Count, photoId));
          continue;
        }

        _imageRenameSvc.RenameImageAndUsages(foundPlantPhoto);
        ConsoleLogger.Info(string.Format("{0} of {1}: Processed {2}", counter++, photoList.Count, photoId));
      }
    }

    public void RenameSingle(string photoId)
    {
      PlantPhoto foundPlantPhoto;

      if (!_plantPhotoSvc.TryFindPlantPhoto(photoId, out foundPlantPhoto))
      {
        ConsoleLogger.Error(string.Format("Unable to find the PhotoID '{0}' in the database", photoId));
      }

      _imageRenameSvc.RenameImageAndUsages(foundPlantPhoto);
      ConsoleLogger.Info(string.Format("Processed {0}", photoId));
    }

    public void RenameCount(int count)
    {
      var counter = 1;

      var renamers = _plantPhotoSvc.PlantPhotos.Take(count);
      foreach (var plantPhoto in renamers) {
        _imageRenameSvc.RenameImageAndUsages(plantPhoto);
        ConsoleLogger.Info(string.Format("{0} of {1}: Processed {2}", counter++, count, plantPhoto.PhotoId));
      }
    }

    public ILogger FileLogger { get; set; }
    public ILogger ConsoleLogger { get; set; }

    #endregion
  }
}