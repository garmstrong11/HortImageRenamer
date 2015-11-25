namespace HortImageRenamer.Domain
{
  using System.Collections.Generic;

  public interface IPlantLibraryRepository
  {
    /// <summary>
    /// Retrieves a sequence of custom field ids
    /// for all image fields
    /// </summary>
    /// <returns></returns>
    IEnumerable<PlantLibrary> GetImageFieldIds();
  }
}