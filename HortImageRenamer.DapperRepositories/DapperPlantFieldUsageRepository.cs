namespace HortImageRenamer.DapperRepositories
{
  using System;
  using System.Data.SqlClient;
  using System.Text;
  using Dapper;
  using HortImageRenamer.Core;
  using HortImageRenamer.Domain;

  public class DapperPlantFieldUsageRepository : IPlantFieldUsageRepository
  {
    private readonly ISettingsService _settingsService;
    private string _updateQuery = string.Empty;
    
    public DapperPlantFieldUsageRepository(ISettingsService settingsService)
    {
      _settingsService = settingsService;
    }

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
      using (var conn = new SqlConnection(_settingsService.ConnectionString)) {
        result = conn.Execute(_updateQuery, parms);
      }

      return result;
    }

    public void PrepareUpdateQuery(string photoFieldIds)
    {
      var qb = new StringBuilder();

      qb.Append("UPDATE tblPlantFieldUsage ");
      qb.Append("SET FieldValue = @NewPhotoId ");
      qb.Append("WHERE FieldValue = @PhotoId ");
      qb.AppendFormat("AND CustomFieldID IN ({0})", photoFieldIds);

      _updateQuery = qb.ToString();
    }
  }
}