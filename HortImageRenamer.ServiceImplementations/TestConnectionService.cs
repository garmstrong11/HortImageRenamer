namespace HortImageRenamer.ServiceImplementations
{
  using System.Configuration;
  using HortImageRenamer.ServiceInterfaces;

  public class TestConnectionService : IConnectionService
  {
    #region Implementation of IConnectionService

    public string GetConnectionString()
    {
      return ConfigurationManager.ConnectionStrings["HortProdTest"].ConnectionString;
    }

    #endregion
  }
}