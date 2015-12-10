namespace HortImageRenamer.ServiceImplementations
{
  using System;
  using System.IO;
  using System.Linq;
  using System.Transactions;
  using ChinhDo.Transactions;
  //using HortImageRenamer.ConnectUnc;
  using HortImageRenamer.Core;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class ImageRenameService : IImageRenameService
  {
    private readonly IPlantPhotoService _plantPhotoService;
    private readonly IPlantFieldUsageService _usageService;
    private readonly ISettingsService _settings;
    //private readonly UncAccessWithCredentials _unc;

    public ImageRenameService(
      IPlantPhotoService plantPhotoService, 
      IPlantFieldUsageService usageService,
      ISettingsService settings)
    {
      if (plantPhotoService == null) throw new ArgumentNullException("plantPhotoService");
      if (usageService == null) throw new ArgumentNullException("usageService");
      if (settings == null) throw new ArgumentNullException("settings");

      _plantPhotoService = plantPhotoService;
      _usageService = usageService;
      _settings = settings;

      _usageService.Initialize();

    //  _unc = new UncAccessWithCredentials();
    //  _unc.NetUseWithCredentials(
    //    @"\\dmz.integracolor.local\HortThumbnails",
    //    "Administrator",
    //    "dmz", "N_5yDe_C#4");
    }
    
    public void RenameImage(PlantPhoto plantPhoto, DateTime modifiedDate)
    {
      var dateString = modifiedDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
      try {
        var fileManager = new TxFileManager();
        using (var scope = new TransactionScope()) {
          _plantPhotoService.RenamePlantPhoto(plantPhoto, modifiedDate);
          _usageService.UpdateUsagesForPhotoId(plantPhoto.PhotoId);

          foreach (var imageRoot in _settings.ImageRoots) {
            var imagePath = plantPhoto.GetActualImagePath(imageRoot);
            var newPath = plantPhoto.GetReplacementPath(imageRoot, _settings.TargetExtension);

            if (File.Exists(imagePath)) {
              fileManager.Move(imagePath, newPath);
            }
          }

          foreach (var thumbnailRoot in _settings.ThumbnailRoots) {
            var thumbPath = plantPhoto.GetThumbnailPath(thumbnailRoot);
            var newPath = plantPhoto.GetReplacementPath(thumbnailRoot, _settings.TargetExtension, true);

            if (File.Exists(thumbPath)) {
              fileManager.Move(thumbPath, newPath);
            }
          }

          //scope.Dispose();
          scope.Complete();
          var message = string.Format("{0}\t{0}{1}\t{2}", plantPhoto.PhotoId, _settings.TargetExtension, dateString);
          Logger.Info(message);
        }
      }
      catch (TransactionAbortedException trex) {
        Logger.Error(string.Format("{0}\t{1}", plantPhoto.PhotoId, trex.Message.Replace(Environment.NewLine, " ")));
      }
      catch (Exception exc) {
        Logger.Error(string.Format("{0}\t{1}", plantPhoto.PhotoId, exc.Message.Replace(Environment.NewLine, " ")));
      }
    }

    public void RenameImage(string photoId, DateTime modifiedDate)
    {
      if (string.IsNullOrWhiteSpace(photoId)) throw new ArgumentNullException("photoId");

      var maybePlantPhoto = _plantPhotoService.FindById(photoId);

      if (!maybePlantPhoto.Any()) {
        throw new ArgumentException(@"PlantPhoto not found for the PlantID", photoId);
      }

      RenameImage(maybePlantPhoto.First(), modifiedDate);
    }

    public ILogger Logger { get; set; }

  }
}
