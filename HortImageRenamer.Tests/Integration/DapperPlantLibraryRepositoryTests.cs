namespace HortImageRenamer.Tests.Integration
{
  using System.Linq;
  using FluentAssertions;
  using HortImageRenamer.Console;
  using HortImageRenamer.DapperRepositories;
  using NUnit.Framework;

  [TestFixture]
  public class DapperPlantLibraryRepositoryTests
  {
    [Test]
    public void CanGetLibraryIds()
    {
      var settings = new TestSettingsService();
      var repo = new DapperPlantLibraryRepository(settings);

      var result = repo.GetImageFieldIds();

      result.Count().Should().Be(211);
    }
  }
}