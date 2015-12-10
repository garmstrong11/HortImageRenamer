namespace HortImageRenamer.DapperRepositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Dapper;
  using HortImageRenamer.Core;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class DapperPlantPhotoRepository : RepositoryBase, IPlantPhotoRepository
  {
    private readonly string _selectPhotoQuery;
    private readonly string _renamePhotoQuery;
    private readonly string _findByIdQuery;

    public DapperPlantPhotoRepository(IConnectionService connectionService)
      : base(connectionService)
    {
      _selectPhotoQuery = BuildPhotoQuery();
      _renamePhotoQuery = BuildRenameQuery();
      _findByIdQuery = BuildFindByIdQuery();
    }

    public IEnumerable<PlantPhoto> GetPlantPhotosForRename()
    {
      IEnumerable<PlantPhoto> result;
      using (var conn = OpenConnection()) {
        result = conn.Query<PlantPhoto>(_selectPhotoQuery);
      }

      return result;
    }

    public int UpdatePlantPhotoId(string photoId, DateTime modifiedDate)
    {
      int result;
      using (var conn = OpenConnection()) {
        result = conn.Execute(_renamePhotoQuery,
          new {
            OldPhotoId = photoId, 
            NewPhotoId = photoId.AddTif(), 
            NewUpdatedAt = modifiedDate
          });
      }
      return result;
    }

    public Maybe<PlantPhoto> FindById(string photoId)
    {
      IEnumerable<PlantPhoto> result;
      using (var conn = OpenConnection()) {
        result = conn.Query<PlantPhoto>(_findByIdQuery, new {PhotoId = photoId}).ToList();
      }

      if (result.Any()) return new Maybe<PlantPhoto>(result.First());
      return new Maybe<PlantPhoto>();
    }

    private static string BuildFindByIdQuery()
    {
      QueryBuilder.Clear();

      QueryBuilder.Append("SELECT PhotoID AS PhotoId, Path AS PhotoPath, UpdatedAt ");
      QueryBuilder.Append("FROM tblPlantPhotos p ");
      QueryBuilder.Append("WHERE p.PhotoID = @PhotoId");

      return QueryBuilder.ToString();
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