namespace HortImageRenamer.ServiceInterfaces
{
  public interface IPlantLibraryService
  {
    /// <summary>
    /// Gets a joined string list of custom field ids
    /// for search photo queries.
    /// </summary>
    /// <returns></returns>
    string GetImageFieldIds();
  }
}