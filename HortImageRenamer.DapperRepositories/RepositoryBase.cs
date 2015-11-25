namespace HortImageRenamer.DapperRepositories
{
  using System.Data;
  using System.Data.SqlClient;
  using System.Text;
  using HortImageRenamer.ServiceInterfaces;

  public abstract class RepositoryBase
  {
    private readonly IConnectionService _connectionService;
    protected static StringBuilder QueryBuilder = new StringBuilder();

    protected RepositoryBase(IConnectionService connectionService)
    {
      _connectionService = connectionService;
    }
    
    protected IDbConnection OpenConnection()
    {
      IDbConnection connection = 
        new SqlConnection(_connectionService.GetConnectionString());

      connection.Open();
      return connection;
    } 
  }
}