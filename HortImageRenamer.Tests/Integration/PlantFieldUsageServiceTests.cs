namespace HortImageRenamer.Tests.Integration
{
  using FluentAssertions;
  using HortImageRenamer.DapperRepositories;
  using HortImageRenamer.ServiceImplementations;
  using NUnit.Framework;

  [TestFixture]
  public class PlantFieldUsageServiceTests
  {
    [Test]
    public void CanUpdatePlantFieldUsages()
    {
      var settings = new FakeSettingsService();
      var conn = new TestConnectionService(settings);
      var libRepo = new DapperPlantLibraryRepository(conn);
      var libSvc = new PlantLibraryService(libRepo);
      var usageRepository = new DapperPlantFieldUsageRepository(conn);
      var service = new PlantFieldUsageService(usageRepository, libSvc);

      service.Initialize();

      var result = service.UpdateUsagesForPhotoId("1000001");

      result.Should().Be(2);
    }
  }
}