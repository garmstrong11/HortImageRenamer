namespace HortImageRenamer.Tests.Integration
{
  using System.Data.SqlClient;
  using System.Linq;
  using Dapper;
  using FluentAssertions;
  using HortImageRenamer.Console;
  using NUnit.Framework;

  [TestFixture]
  public class DbAccessTests
  {
    [Test]
    public void CanConnectToTestDb()
    {
      var settings = new TestSettingsService();

      using (var conn = new SqlConnection(settings.ConnectionString))
      {
        const string query = "SELECT db_name()";
        var result = conn.Query<string>(query).ToList();

        result.First().Should().Be("HortProd");
      }
    }

    [Test]
    public void CanConnectToProductionDb()
    {
      var settings = new ProductionSettingsService();

      using (var conn = new SqlConnection(settings.ConnectionString))
      {
        const string query = "SELECT db_name()";
        var result = conn.Query<string>(query).ToList();

        result.First().Should().Be("HortProduction");
      }
    }
  }
}