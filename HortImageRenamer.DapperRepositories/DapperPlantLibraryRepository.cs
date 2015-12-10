namespace HortImageRenamer.DapperRepositories
{
  using System.Collections.Generic;
  using System.Data.SqlClient;
  using System.Text;
  using Dapper;
  using HortImageRenamer.Core;
  using HortImageRenamer.Domain;

  public class DapperPlantLibraryRepository : IPlantLibraryRepository
  {
    private readonly ISettingsService _settingsService;

    public DapperPlantLibraryRepository(ISettingsService settingsService)
    {
      _settingsService = settingsService;
    }

    public IEnumerable<PlantLibrary> GetImageFieldIds()
    {
      IEnumerable<PlantLibrary> result;

      using (var conn = new SqlConnection(_settingsService.ConnectionString)) {
        var query = GetPlantLibraryImageIdQuery();
        result = conn.Query<PlantLibrary>(query);
      }

      return result;
    }

    private static string GetPlantLibraryImageIdQuery()
    {
      var qb = new StringBuilder();

      qb.Append("SELECT PlantLibraryID AS Id, ");
      qb.Append("[Name], ");
      qb.Append("PhotoFieldID AS PhotoFieldId, ");
      qb.Append("InsetFieldID AS InsetFieldId, ");
      qb.Append("Inset2FieldID AS Inset2FieldId, ");
      qb.Append("Inset3FieldID AS Inset3FieldId, ");
      qb.Append("Inset4FieldID AS Inset4FieldId ");
      qb.Append("FROM tblPlantLibrary");

      return qb.ToString();
    }
  }
}