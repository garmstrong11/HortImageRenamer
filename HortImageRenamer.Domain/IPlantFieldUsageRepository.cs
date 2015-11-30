namespace HortImageRenamer.Domain
{
  public interface IPlantFieldUsageRepository
  {
    /// <summary>
    /// Updates plant field usages with the passed-in PhotoID.
    /// </summary>
    /// <param name="photoId"></param>
    /// <returns>Changed row count.</returns>
    int UpdatePhotoFieldValues(string photoId);

    /// <summary>
    /// Configures the update query to search for custom field ids
    /// that are related to plant images (main and inset).
    /// </summary>
    /// <param name="photoFieldIds"></param>
    void PrepareUpdateQuery(string photoFieldIds);
  }
}