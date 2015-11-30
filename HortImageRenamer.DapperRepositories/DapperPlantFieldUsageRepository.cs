namespace HortImageRenamer.DapperRepositories
{
  using System;
  using Dapper;
  using HortImageRenamer.Core;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class DapperPlantFieldUsageRepository : RepositoryBase, IPlantFieldUsageRepository
  {
    private string _updateQuery = string.Empty;
    
    public DapperPlantFieldUsageRepository(IConnectionService connectionService) 
      : base(connectionService)
    {}

    public int UpdatePhotoFieldValues(string photoId)
    {
      if (string.IsNullOrWhiteSpace(_updateQuery)) {
        throw new InvalidOperationException("Please prepare the Update query first.");
      }

      var parms = new
      {
        PhotoId = photoId,
        NewPhotoId = photoId.AddTif()
      };

      int result;
      using (var conn = OpenConnection()) {
        result = conn.Execute(_updateQuery, parms);
      }

      return result;
    }

    public void PrepareUpdateQuery(string photoFieldIds)
    {
      QueryBuilder.Clear();

      QueryBuilder.Append("UPDATE tblPlantFieldUsage ");
      QueryBuilder.Append("SET FieldValue = @NewPhotoId ");
      QueryBuilder.Append("WHERE FieldValue = @PhotoId ");
      QueryBuilder.AppendFormat("AND CustomFieldID IN ({0})", photoFieldIds);

      _updateQuery = QueryBuilder.ToString();
    }
  }
}