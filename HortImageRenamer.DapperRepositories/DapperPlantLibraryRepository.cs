namespace HortImageRenamer.DapperRepositories
{
  using System.Collections.Generic;
  using Dapper;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class DapperPlantLibraryRepository : RepositoryBase, IPlantLibraryRepository
  {
    public DapperPlantLibraryRepository(IConnectionService connectionService) 
      : base(connectionService)
    {
    }

    public IEnumerable<PlantLibrary> GetImageFieldIds()
    {
      IEnumerable<PlantLibrary> result;

      using (var conn = OpenConnection()) {
        var query = GetPlantLibraryImageIdQuery();
        result = conn.Query<PlantLibrary>(query);
      }

      return result;
    }

    private static string GetPlantLibraryImageIdQuery()
    {
      QueryBuilder.Clear();

      QueryBuilder.Append("SELECT PlantLibraryID AS Id, ");
      QueryBuilder.Append("[Name], ");
      QueryBuilder.Append("PhotoFieldID AS PhotoFieldId, ");
      QueryBuilder.Append("InsetFieldID AS InsetFieldId, ");
      QueryBuilder.Append("Inset2FieldID AS Inset2FieldId, ");
      QueryBuilder.Append("Inset3FieldID AS Inset3FieldId, ");
      QueryBuilder.Append("Inset4FieldID AS Inset4FieldId ");
      QueryBuilder.Append("FROM tblPlantLibrary");

      return QueryBuilder.ToString();
    }
  }
}