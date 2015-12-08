namespace HortImageRenamer.Tests.Integration
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using HortImageRenamer.DapperRepositories;
  using HortImageRenamer.ServiceImplementations;
  using NUnit.Framework;

  [TestFixture]
  public class DapperPlantPhotoRepositoryTests
  {
    [Test]
    public void CanGetPhotosToRename()
    {
      var repo = new DapperPlantPhotoRepository(new TestConnectionService());
      var result = repo.GetPlantPhotosForRename().ToList();

      result.Should().NotBeEmpty();
    }

    [Test]
    public void CanRenamePhoto()
    {
      var repo = new DapperPlantPhotoRepository(new TestConnectionService());
      var result = repo.UpdatePlantPhotoId("1000001", DateTime.Now);

      result.Should().Be(1);
    }
  }
}