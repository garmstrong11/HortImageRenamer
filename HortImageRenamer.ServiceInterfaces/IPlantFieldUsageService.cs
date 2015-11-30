namespace HortImageRenamer.ServiceInterfaces
{
  public interface IPlantFieldUsageService
  {
    /// <summary>
    /// Updates all usages for a given plantID.
    /// </summary>
    /// <param name="photoId"></param>
    /// <returns></returns>
    int UpdateUsagesForPhotoId(string photoId);

    /// <summary>
    /// Prepares the service for use.
    /// </summary>
    void Initialize();
  }
}