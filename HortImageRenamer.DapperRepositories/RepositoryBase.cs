namespace HortImageRenamer.DapperRepositories
{
  using System.Data;
  using System.Data.SqlClient;
  using System.Text;
  using HortImageRenamer.Core;

  public abstract class RepositoryBase
  {
    private readonly IDbConnection _dbConnection;
    protected static StringBuilder QueryBuilder = new StringBuilder();

    protected RepositoryBase(ISettingsService settings)
    {
      _dbConnection = new SqlConnection(settings.ConnectionString);
      _dbConnection.Open();
    }

    protected IDbConnection OpenConnection()
    {
      return _dbConnection;
    } 
  }
}