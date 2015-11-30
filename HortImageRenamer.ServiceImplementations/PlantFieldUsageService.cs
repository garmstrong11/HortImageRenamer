namespace HortImageRenamer.ServiceImplementations
{
  using System;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class PlantFieldUsageService : IPlantFieldUsageService
  {
    private readonly IPlantFieldUsageRepository _usageRepository;
    private readonly IPlantLibraryService _libraryService;
    private string _photoFieldIds;

    public PlantFieldUsageService(IPlantFieldUsageRepository usageRepository, IPlantLibraryService libraryService)
    {
      if (usageRepository == null) throw new ArgumentNullException("usageRepository");
      if (libraryService == null) throw new ArgumentNullException("libraryService");

      _usageRepository = usageRepository;
      _libraryService = libraryService;
    }

    public int UpdateUsagesForPhotoId(string photoId)
    {
      if (string.IsNullOrWhiteSpace(photoId)) throw new ArgumentNullException("photoId");
      if (string.IsNullOrWhiteSpace(_photoFieldIds)) throw new InvalidOperationException("UsageService is not initialized.");

      var rowCount = _usageRepository.UpdatePhotoFieldValues(photoId);
      return rowCount;
    }

    public void Initialize()
    {
      _photoFieldIds = _libraryService.GetImageFieldIds();
      _usageRepository.PrepareUpdateQuery(_photoFieldIds);
    }
  }
}