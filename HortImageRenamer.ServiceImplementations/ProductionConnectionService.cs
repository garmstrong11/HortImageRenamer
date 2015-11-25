namespace HortImageRenamer.ServiceImplementations
{
  using System.Configuration;
  using HortImageRenamer.ServiceInterfaces;

  public class ProductionConnectionService : IConnectionService
  {
    #region Implementation of IConnectionService

    public string GetConnectionString()
    {
      return ConfigurationManager.ConnectionStrings["HortProd"].ConnectionString;
    }

    #endregion
  }
}