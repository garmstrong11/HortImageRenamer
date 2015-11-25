namespace HortImageRenamer.DapperRepositories
{
  using System.Collections.Generic;
  using System.Runtime.Remoting.Messaging;
  using Dapper;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class DapperPlantPhotoRepository : RepositoryBase, IPlantPhotoRepository
  {
    public DapperPlantPhotoRepository(IConnectionService connectionService) 
      : base(connectionService)
    {}

    public IEnumerable<PlantPhoto> GetPlantPhotosForRename()
    {
      IEnumerable<PlantPhoto> result;
      using (var conn = OpenConnection()) {
        var query = BuildPhotoQuery();
        result = conn.Query<PlantPhoto>(query);
      }

      return result;
    }

    public int UpdatePlantPhotoId(string photoId)
    {
      throw new System.NotImplementedException();
    }

    private string BuildPhotoQuery()
    {
      QueryBuilder.Clear();

      QueryBuilder.Append("SELECT PhotoID AS PhotoId, Path AS PhotoPath, UpdatedAt ");
      QueryBuilder.Append("FROM tblPlantPhotos p ");
      QueryBuilder.Append("WHERE p.PATH IS NOT NULL AND p.Path NOT LIKE '%Photoshoots%'");

      return QueryBuilder.ToString();
    }
  }
}