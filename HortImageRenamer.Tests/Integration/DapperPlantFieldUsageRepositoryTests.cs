namespace HortImageRenamer.Tests.Integration
{
  using System;
  using FakeItEasy;
  using FluentAssertions;
  using HortImageRenamer.DapperRepositories;
  using HortImageRenamer.ServiceImplementations;
  using NUnit.Framework;

  [TestFixture]
  public class DapperPlantFieldUsageRepositoryTests
  {
    [Test]
    public void EmptyPhotoFieldsList_Throws()
    {
      var conn = new TestConnectionService();
      var repo = new DapperPlantFieldUsageRepository(conn);

      Action act = () => repo.UpdatePhotoFieldValues(A<string>._);

      act.ShouldThrow<InvalidOperationException>();
    }

    [Test]
    public void CanUpdateUsages()
    {
      var conn = new TestConnectionService();
      var repo = new DapperPlantFieldUsageRepository(conn);
      const string fieldList = "14, 1522";

      repo.PrepareUpdateQuery(fieldList);

      var result = repo.UpdatePhotoFieldValues("1000001");

      result.Should().Be(2);
    }
  }
}