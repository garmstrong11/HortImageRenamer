namespace HortImageRenamer.Tests.Unit
{
  using System.Collections.Generic;
  using FakeItEasy;
  using FluentAssertions;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceImplementations;
  using NUnit.Framework;

  [TestFixture]
  public class SimplePlantLibraryServiceTests
  {
    [Test]
    public void ReturnsCorrectString()
    {
      var repo = A.Fake<IPlantLibraryRepository>();

      var lib1 = new PlantLibrary()
      {
        Id = 1,
        PhotoFieldId = 13,
        InsetFieldId = 26,
        Inset2FieldId = 39,
        Inset3FieldId = null,
        Inset4FieldId = null
      };
      var lib2 = new PlantLibrary()
      {
        Id = 2,
        PhotoFieldId = 22,
        InsetFieldId = 27,
        Inset2FieldId = 40,
        Inset3FieldId = 69,
        Inset4FieldId = 39
      };

      A.CallTo(() => repo.GetImageFieldIds()).Returns(new List<PlantLibrary> {lib1, lib2});
      var svc = new SimplePlantLibraryService(repo);

      var actual = svc.GetImageFieldIds();

      actual.Should().Be("13, 22, 26, 27, 39, 40, 69");
    }
  }
}