namespace HortImageRenamer.Tests.Integration
{
  using System.Linq;
  using FluentAssertions;
  using HortImageRenamer.DapperRepositories;
  using HortImageRenamer.ServiceImplementations;
  using NUnit.Framework;

  [TestFixture]
  public class DapperPlantLibraryRepositoryTests
  {
    [Test]
    public void CanGetLibraryIds()
    {
      var settings = new FakeSettingsService();
      var connectionService = new TestConnectionService(settings);
      //var connectionService = new ProductionConnectionService();
      var repo = new DapperPlantLibraryRepository(connectionService);

      var result = repo.GetImageFieldIds();

      result.Count().Should().Be(211);
    }
  }
}