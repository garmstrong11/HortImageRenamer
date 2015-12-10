namespace HortImageRenamer.Tests.Unit
{
  using System.IO;
  using System.Linq;
  using FluentAssertions;
  using HortImageRenamer.Console;
  using NUnit.Framework;

  [TestFixture]
  public class ProductionSettingsServiceTests
  {
    [Test]
    public void ImageRootsAreRealDirectories()
    {
      var settings = new ProductionSettingsService();

      var dirs = settings.ImageRoots.Select(r => new DirectoryInfo(r));

      dirs.All(d => d.Exists).Should().BeTrue();
    }
  }
}