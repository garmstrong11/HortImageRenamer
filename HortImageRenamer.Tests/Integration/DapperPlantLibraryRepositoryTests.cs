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
      var connectionService = new TestConnectionService();
      //var connectionService = new ProductionConnectionService();
      var repo = new DapperPlantLibraryRepository(connectionService);

      var result = repo.GetImageFieldIds();

      result.Count().Should().Be(211);
    }
  }
}