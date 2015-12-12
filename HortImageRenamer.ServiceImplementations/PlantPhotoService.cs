namespace HortImageRenamer.ServiceImplementations
{
  using System;
  using System.Collections.Generic;
  using System.Data.SqlClient;
  using System.Linq;
  using System.Text;
  using Dapper;
  using HortImageRenamer.Core;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class PlantPhotoService : IPlantPhotoService
  {
    private readonly ISettingsService _settings;
    private readonly List<PlantPhoto> _plantPhotos; 

    public PlantPhotoService(ISettingsService settings)
    {
      _settings = settings;
      _plantPhotos = new List<PlantPhoto>();
    }

    #region Implementation of IPlantPhotoService

    public void Initialize()
    {
      var whiteList = _settings.LegalExtensions.Split(
        new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

      IEnumerable<PlantPhoto> result;

      using (var conn = new SqlConnection(_settings.ConnectionString))
      {
        conn.Open();
        result = conn.Query<PlantPhoto>(BuildPhotoQuery());
      }

      _plantPhotos.AddRange(result.Where(r => !whiteList.Contains(r.ExtensionLower)));
    }

    public IEnumerable<PlantPhoto> PlantPhotos
    {
      get { return _plantPhotos.AsEnumerable(); }
    }

    public bool TryFindPlantPhoto(string photoId, out PlantPhoto plantPhoto)
    {
      var found = _plantPhotos.FirstOrDefault(p => p.PhotoId == photoId);

      if (found == null) {
        plantPhoto = new PlantPhoto {PhotoId = "Not found"};
        return false;
      }

      plantPhoto = found;
      return true;
    }

    #endregion

    private static string BuildPhotoQuery()
    {
      var qb = new StringBuilder();

      qb.Append("SELECT PhotoID AS PhotoId, Path AS PhotoPath, UpdatedAt ");
      qb.Append("FROM tblPlantPhotos p ");
      qb.Append("WHERE p.PATH IS NOT NULL AND p.Path NOT LIKE '%Photoshoots%'");

      return qb.ToString();
    }
  }
}