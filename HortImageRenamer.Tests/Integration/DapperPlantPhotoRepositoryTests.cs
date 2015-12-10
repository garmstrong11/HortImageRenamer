namespace HortImageRenamer.Tests.Integration
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using HortImageRenamer.Console;
  using HortImageRenamer.DapperRepositories;
  using HortImageRenamer.ServiceImplementations;
  using NUnit.Framework;

  [TestFixture]
  public class DapperPlantPhotoRepositoryTests
  {
    [Test]
    public void CanGetPhotosToRename()
    {
      var settings = new FakeSettingsService();
      var repo = new DapperPlantPhotoRepository(new TestConnectionService(settings));
      var result = repo.GetPlantPhotosForRename().ToList();

      result.Should().NotBeEmpty();
    }

    [Test]
    public void CanRenamePhoto()
    {
      var settings = new FakeSettingsService();
      var repo = new DapperPlantPhotoRepository(new TestConnectionService(settings));
      var result = repo.UpdatePlantPhotoId("1000001", DateTime.Now);

      result.Should().Be(1);
    }

    [Test]
    public void CanFindById()
    {
      var settings = new TestSettingsService();
      var repo = new DapperPlantPhotoRepository(new TestConnectionService(settings));
      var result = repo.FindById("1000001.tif");

      result.First().PhotoId.Should().Be("1000001.tif");
    }
  }
}