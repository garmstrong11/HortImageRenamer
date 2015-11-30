namespace HortImageRenamer.DapperRepositories
{
  using System;
  using System.Collections.Generic;
  using Dapper;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class DapperPlantPhotoRepository : RepositoryBase, IPlantPhotoRepository
  {
    private readonly string _selectPhotoQuery;
    private readonly string _renamePhotoQuery;

    public DapperPlantPhotoRepository(IConnectionService connectionService)
      : base(connectionService)
    {
      _selectPhotoQuery = BuildPhotoQuery();
      _renamePhotoQuery = BuildRenameQuery();
    }

    public IEnumerable<PlantPhoto> GetPlantPhotosForRename()
    {
      IEnumerable<PlantPhoto> result;
      using (var conn = OpenConnection()) {
        result = conn.Query<PlantPhoto>(_selectPhotoQuery);
      }

      return result;
    }

    public int UpdatePlantPhotoId(string photoId)
    {
      int result;
      using (var conn = OpenConnection()) {
        result = conn.Execute(_renamePhotoQuery,
          new {
            OldPhotoId = photoId, 
            NewPhotoId = string.Format("{0}.tif", photoId), 
            NewUpdatedAt = DateTime.Now
          });
      }
      return result;
    }

    private static string BuildPhotoQuery()
    {
      QueryBuilder.Clear();

      QueryBuilder.Append("SELECT PhotoID AS PhotoId, Path AS PhotoPath, UpdatedAt ");
      QueryBuilder.Append("FROM tblPlantPhotos p ");
      QueryBuilder.Append("WHERE p.PATH IS NOT NULL AND p.Path NOT LIKE '%Photoshoots%'");

      return QueryBuilder.ToString();
    }

    private static string BuildRenameQuery()
    {
      QueryBuilder.Clear();

      QueryBuilder.Append("UPDATE tblPlantPhotos ");
      QueryBuilder.Append("SET PhotoID = @NewPhotoId, ");
      QueryBuilder.Append("UpdatedAt = @NewUpdatedAt ");
      QueryBuilder.Append("WHERE PhotoID = @OldPhotoId");

      return QueryBuilder.ToString();
    }
  }
}