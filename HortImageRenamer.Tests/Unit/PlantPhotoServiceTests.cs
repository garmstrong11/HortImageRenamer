namespace HortImageRenamer.Tests.Unit
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using HortImageRenamer.DapperRepositories;
  using HortImageRenamer.ServiceImplementations;
  using NUnit.Framework;

  [TestFixture]
  public class PlantPhotoServiceTests
  {
    [Test]
    public void CanAcquireRenameCandidates()
    {
      var settings = new FakeSettingsService();
      var repo = new DapperPlantPhotoRepository(new TestConnectionService());
      var svc = new PlantPhotoService(settings, repo);

      var actual = svc.GetRenameCandidates();
      var blackList = new List<string> {".tif", ".eps", ".jpg", ".tiff", ".jpeg", ".pdf", ".esp", ".bmp"};

      actual.Any(r => blackList.Contains(r.ExtensionLower)).Should().BeFalse();
    }
  }
}