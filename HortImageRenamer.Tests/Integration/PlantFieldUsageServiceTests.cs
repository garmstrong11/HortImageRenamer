namespace HortImageRenamer.Tests.Integration
{
  using FluentAssertions;
  using HortImageRenamer.Console;
  using HortImageRenamer.DapperRepositories;
  using HortImageRenamer.ServiceImplementations;
  using NUnit.Framework;

  [TestFixture]
  public class PlantFieldUsageServiceTests
  {
    [Test]
    public void CanUpdatePlantFieldUsages()
    {
      var settings = new TestSettingsService();
      var libRepo = new DapperPlantLibraryRepository(settings);
      var libSvc = new PlantLibraryService(libRepo);
      var usageRepository = new DapperPlantFieldUsageRepository(settings);
      var service = new PlantFieldUsageService(usageRepository, libSvc);

      service.Initialize();

      var result = service.UpdateUsagesForPhotoId("1000001");

      result.Should().Be(2);
    }
  }
}