namespace HortImageRenamer.ServiceImplementations
{
  using System;
  using System.Collections.Generic;
  using System.Data.SqlClient;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Transactions;
  using ChinhDo.Transactions;
  using Dapper;
  using HortImageRenamer.Core;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class TransactionalImageRenamer : ITransactionalImageRenamer
  {
    private static readonly StringBuilder Qb = new StringBuilder();
    private readonly ISettingsService _settings;
    //private readonly string _photoSelectQuery;
    private readonly string _usageUpdateQuery;
    private readonly string _photoUpdateQuery;
    private readonly DateTime _now;

    public TransactionalImageRenamer(ISettingsService settings)
    {
      _settings = settings;
      //_photoSelectQuery = BuildPhotoSelectQuery();
      _usageUpdateQuery = BuildUsageUpdateQuery();
      _photoUpdateQuery = BuildPhotoUpdateQuery();
      _now = DateTime.Now;
    }

    public ILogger Logger { get; set; }
    
    public void RenameImageAndUsages(PlantPhoto plantPhoto)
    {
      var dateString = _now.ToString("yyyy-MM-dd HH:mm:ss.fff");
      var conn = new SqlConnection(_settings.ConnectionString);
      var fileManager = new TxFileManager();

      try {
        using (var scope = new TransactionScope()) {
          conn.Open();

          conn.Execute(_photoUpdateQuery,
            new { OldPhotoId = plantPhoto.PhotoId, NewPhotoId = plantPhoto.PhotoId.AddTif(), NewUpdatedAt = _now });

          conn.Execute(_usageUpdateQuery,
            new { plantPhoto.PhotoId, NewPhotoId = plantPhoto.PhotoId.AddTif() });

          foreach (var imageRoot in _settings.ImageRoots)
          {
            var imagePath = plantPhoto.GetActualImagePath(imageRoot);
            var newPath = plantPhoto.GetReplacementPath(imageRoot, _settings.TargetExtension);

            if (File.Exists(imagePath)) {
              fileManager.Move(imagePath, newPath);
            }
          }

          foreach (var thumbnailRoot in _settings.ThumbnailRoots)
          {
            var thumbPath = plantPhoto.GetThumbnailPath(thumbnailRoot);
            var newPath = plantPhoto.GetReplacementPath(thumbnailRoot, _settings.TargetExtension, true);

            if (File.Exists(thumbPath)) {
              fileManager.Move(thumbPath, newPath);
            }
          }

          scope.Complete();
          var message = string.Format("{0}\t{0}{1}\t{2}", plantPhoto.PhotoId, _settings.TargetExtension, dateString);
          Logger.Info(message);
        }
      }
      catch (TransactionAbortedException trex)
      {
        Logger.Error(string.Format("{0}\t{1}", plantPhoto.PhotoId, trex.Message.Replace(Environment.NewLine, " ")));
      }
      catch (Exception exc)
      {
        Logger.Error(string.Format("{0}\t{1}", plantPhoto.PhotoId, exc.Message.Replace(Environment.NewLine, " ")));
      }
    }

    private static string BuildFindByIdQuery()
    {
      Qb.Clear();

      Qb.Append("SELECT PhotoID AS PhotoId, Path AS PhotoPath, UpdatedAt ");
      Qb.Append("FROM tblPlantPhotos p ");
      Qb.Append("WHERE p.PhotoID = @PhotoId");

      return Qb.ToString();
    }

    private static string BuildPhotoSelectQuery()
    {
      Qb.Clear();

      Qb.Append("SELECT PhotoID AS PhotoId, Path AS PhotoPath, UpdatedAt ");
      Qb.Append("FROM tblPlantPhotos p ");
      Qb.Append("WHERE p.PATH IS NOT NULL AND p.Path NOT LIKE '%Photoshoots%'");

      return Qb.ToString();
    }

    private static string BuildPhotoUpdateQuery()
    {
      Qb.Clear();

      Qb.Append("UPDATE tblPlantPhotos ");
      Qb.Append("SET PhotoID = @NewPhotoId, ");
      Qb.Append("UpdatedAt = @NewUpdatedAt ");
      Qb.Append("WHERE PhotoID = @OldPhotoId");

      return Qb.ToString();
    }

    private string BuildUsageUpdateQuery()
    {
      Qb.Clear();

      Qb.Append("SELECT PlantLibraryID AS Id, ");
      Qb.Append("[Name], ");
      Qb.Append("PhotoFieldID AS PhotoFieldId, ");
      Qb.Append("InsetFieldID AS InsetFieldId, ");
      Qb.Append("Inset2FieldID AS Inset2FieldId, ");
      Qb.Append("Inset3FieldID AS Inset3FieldId, ");
      Qb.Append("Inset4FieldID AS Inset4FieldId ");
      Qb.Append("FROM tblPlantLibrary");

      IEnumerable<PlantLibrary> libraries;

      using (var conn = new SqlConnection(_settings.ConnectionString)) {
        libraries = conn.Query<PlantLibrary>(Qb.ToString()).ToList();
      }

      var ids = libraries.Select(p => p.PhotoFieldId)
        .Concat(libraries.Select(p => p.InsetFieldId))
        .Concat(libraries.Select(p => p.Inset2FieldId))
        .Concat(libraries.Select(p => p.Inset3FieldId))
        .Concat(libraries.Select(p => p.Inset4FieldId))
        .Where(k => k.HasValue)
        .Cast<int>()
        .Distinct()
        .OrderBy(k => k)
        .ToList();

      Qb.Clear();

      Qb.Append("UPDATE tblPlantFieldUsage ");
      Qb.Append("SET FieldValue = @NewPhotoId ");
      Qb.Append("WHERE FieldValue = @PhotoId ");
      Qb.AppendFormat("AND CustomFieldID IN ({0})", string.Join(", ", ids));

      return Qb.ToString();
    }
  }
}