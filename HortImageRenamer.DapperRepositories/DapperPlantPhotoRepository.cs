namespace HortImageRenamer.DapperRepositories
{
  using System;
  using System.Collections.Generic;
  using System.Data.SqlClient;
  using System.Linq;
  using System.Text;
  using Dapper;
  using HortImageRenamer.Core;
  using HortImageRenamer.Domain;

  public class DapperPlantPhotoRepository : IPlantPhotoRepository
  {
    private readonly ISettingsService _settingsService;
    private readonly string _selectPhotoQuery;
    private readonly string _renamePhotoQuery;
    private readonly string _findByIdQuery;

    public DapperPlantPhotoRepository(ISettingsService settingsService)
    {
      _settingsService = settingsService;
      _selectPhotoQuery = BuildPhotoQuery();
      _renamePhotoQuery = BuildRenameQuery();
      _findByIdQuery = BuildFindByIdQuery();
    }

    public IEnumerable<PlantPhoto> GetPlantPhotosForRename()
    {
      IEnumerable<PlantPhoto> result;
      using (var conn = new SqlConnection(_settingsService.ConnectionString)) {
        result = conn.Query<PlantPhoto>(_selectPhotoQuery);
      }

      return result;
    }

    public int UpdatePlantPhotoId(string photoId, DateTime modifiedDate)
    {
      int result;
      using (var conn = new SqlConnection(_settingsService.ConnectionString)) {
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
      using (var conn = new SqlConnection(_settingsService.ConnectionString))
      {
        result = conn.Query<PlantPhoto>(_findByIdQuery, new {PhotoId = photoId}).ToList();
      }

      if (result.Any()) return new Maybe<PlantPhoto>(result.First());
      return new Maybe<PlantPhoto>();
    }

    private static string BuildFindByIdQuery()
    {
      var qb = new StringBuilder();

      qb.Append("SELECT PhotoID AS PhotoId, Path AS PhotoPath, UpdatedAt ");
      qb.Append("FROM tblPlantPhotos p ");
      qb.Append("WHERE p.PhotoID = @PhotoId");

      return qb.ToString();
    }

    private static string BuildPhotoQuery()
    {
      var qb = new StringBuilder();

      qb.Append("SELECT PhotoID AS PhotoId, Path AS PhotoPath, UpdatedAt ");
      qb.Append("FROM tblPlantPhotos p ");
      qb.Append("WHERE p.PATH IS NOT NULL AND p.Path NOT LIKE '%Photoshoots%'");

      return qb.ToString();
    }

    private static string BuildRenameQuery()
    {
      var qb = new StringBuilder();

      qb.Append("UPDATE tblPlantPhotos ");
      qb.Append("SET PhotoID = @NewPhotoId, ");
      qb.Append("UpdatedAt = @NewUpdatedAt ");
      qb.Append("WHERE PhotoID = @OldPhotoId");

      return qb.ToString();
    }
  }
}