namespace HortImageRenamer.Tests.Integration
{
  using ChinhDo.Transactions;
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
      var connSvc = new TestConnectionService();
      var photoRepo = new DapperPlantPhotoRepository(connSvc);
      var usageRepo = new DapperPlantFieldUsageRepository(connSvc);
      var libraryRepo = new DapperPlantLibraryRepository(connSvc);
      var settings = new FakeSettingsService();
      var photoService = new PlantPhotoService(settings, photoRepo);
      var libService = new PlantLibraryService(libraryRepo);
      var usageSerivce = new PlantFieldUsageService(usageRepo, libService);
      var fileManager = new TxFileManager();

      var plantPhoto = new PlantPhoto
      {
        PhotoId = "1000001",
        PhotoPath = @"\\Nasdee\HortImages\1000000_1000350"
      };

      var imageService = new ImageRenameService(photoService, usageSerivce, settings);

      imageService.RenameImage(plantPhoto);
    }
  }
}