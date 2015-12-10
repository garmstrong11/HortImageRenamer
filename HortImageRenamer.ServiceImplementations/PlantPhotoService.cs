namespace HortImageRenamer.ServiceImplementations
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using HortImageRenamer.Core;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class PlantPhotoService : IPlantPhotoService
  {
    private readonly ISettingsService _settings;
    private readonly IPlantPhotoRepository _repository;

    public PlantPhotoService(ISettingsService settings, IPlantPhotoRepository repository)
    {
      _settings = settings;
      _repository = repository;
    }

    public IEnumerable<PlantPhoto> GetRenameCandidates()
    {
      var blackList = _settings.LegalExtensions.Split(
        new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);

      var result = _repository.GetPlantPhotosForRename()
        .Where(r => !blackList.Contains(r.ExtensionLower));

      return result;
    }

    public int RenamePlantPhoto(PlantPhoto photo, DateTime modifiedDate)
    {
      return _repository.UpdatePlantPhotoId(photo.PhotoId, modifiedDate);
    }

    public Maybe<PlantPhoto> FindById(string photoId)
    {
      return _repository.FindById(photoId);
    }
  }
}