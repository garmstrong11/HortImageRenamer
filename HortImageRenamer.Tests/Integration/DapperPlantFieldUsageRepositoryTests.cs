namespace HortImageRenamer.Tests.Integration
{
  using System;
  using FakeItEasy;
  using FluentAssertions;
  using HortImageRenamer.Console;
  using HortImageRenamer.DapperRepositories;
  using NUnit.Framework;

  [TestFixture]
  public class DapperPlantFieldUsageRepositoryTests
  {
    [Test]
    public void EmptyPhotoFieldsList_Throws()
    {
      var settings = new TestSettingsService();
      var repo = new DapperPlantFieldUsageRepository(settings);

      Action act = () => repo.UpdatePhotoFieldValues(A<string>._);

      act.ShouldThrow<InvalidOperationException>();
    }

    [Test]
    public void CanUpdateUsages()
    {
      var settings = new TestSettingsService();
      var repo = new DapperPlantFieldUsageRepository(settings);
      const string fieldList = "14, 1522";

      repo.PrepareUpdateQuery(fieldList);

      var result = repo.UpdatePhotoFieldValues("1000001");

      result.Should().Be(2);
    }
  }
}