namespace HortImageRenamer.Tests.Integration
{
  using System;
  using ChinhDo.Transactions;
  using HortImageRenamer.Console;
  using HortImageRenamer.DapperRepositories;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceImplementations;
  using NUnit.Framework;

  [TestFixture]
  public class ImageRenameServiceTests
  {
    [Test]
    public void CanRenameTransactionally()
    {
      var settings = new FakeSettingsService();
      var connSvc = new TestConnectionService(settings);
      var photoRepo = new DapperPlantPhotoRepository(connSvc);
      var usageRepo = new DapperPlantFieldUsageRepository(connSvc);
      var libraryRepo = new DapperPlantLibraryRepository(connSvc);
      var photoService = new PlantPhotoService(settings, photoRepo);
      var libService = new PlantLibraryService(libraryRepo);
      var usageService = new PlantFieldUsageService(usageRepo, libService);
      var fileManager = new TxFileManager();

      var plantPhoto = new PlantPhoto
      {
        PhotoId = "1000001",
        PhotoPath = @"\\Nasdee\HortImages\1000000_1000350"
      };

      var imageService = new ImageRenameService(photoService, usageService, settings);

      imageService.RenameImage(plantPhoto, DateTime.Now);
    }

    [Test]
    public void RenameOnePhoto()
    {
      var settings = new TestSettingsService();

      var connSvc = new TestConnectionService(settings);
      var photoRepo = new DapperPlantPhotoRepository(connSvc);
      var usageRepo = new DapperPlantFieldUsageRepository(connSvc);
      var libraryRepo = new DapperPlantLibraryRepository(connSvc);
      var photoService = new PlantPhotoService(settings, photoRepo);
      var libService = new PlantLibraryService(libraryRepo);
      var usageService = new PlantFieldUsageService(usageRepo, libService);

      const string photoId = "1718";
      var plantPhoto = new PlantPhoto {PhotoId = "1718", PhotoPath = @"\\nasdee\HortImages\1000_1999"};

      var imageService = new ImageRenameService(photoService, usageService, settings);

      imageService.RenameImage(plantPhoto, DateTime.Now);
    }
  }
}