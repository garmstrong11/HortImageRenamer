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
      var settings = new TestSettingsService();
      var photoRepo = new DapperPlantPhotoRepository(settings);
      var usageRepo = new DapperPlantFieldUsageRepository(settings);
      var libraryRepo = new DapperPlantLibraryRepository(settings);
      var photoService = new PlantPhotoService(settings, photoRepo);
      var libService = new PlantLibraryService(libraryRepo);
      var usageService = new PlantFieldUsageService(usageRepo, libService);

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
      var photoRepo = new DapperPlantPhotoRepository(settings);
      var usageRepo = new DapperPlantFieldUsageRepository(settings);
      var libraryRepo = new DapperPlantLibraryRepository(settings);
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