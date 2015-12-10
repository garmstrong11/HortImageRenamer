namespace HortImageRenamer.Tests.Integration
{
  using System.Data.SqlClient;
  using System.Linq;
  using System.Text;
  using Dapper;
  using FluentAssertions;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceImplementations;
  using NUnit.Framework;

  [TestFixture]
  public class DbAccessTests
  {
    [Test]
    public void CanConnect()
    {
      var settings = new FakeSettingsService();
      var conServ = new TestConnectionService(settings);;

      using (var conn = new SqlConnection(conServ.GetConnectionString())) {
        var sb = new StringBuilder();
        sb.Append("SELECT PlantLibraryID AS Id, ");
        sb.Append("[Name], ");
        sb.Append("PhotoFieldID AS PhotoFieldId, ");
        sb.Append("InsetFieldID AS InsetFieldId, ");
        sb.Append("Inset2FieldID AS Inset2FieldId, ");
        sb.Append("Inset3FieldID AS Inset3FieldId, ");
        sb.Append("Inset4FieldID AS Inset4FieldId ");
        sb.Append("FROM tblPlantLibrary");

        var query = sb.ToString();
        var result = conn.Query<PlantLibrary>(query).ToList();

        var ids = result.Select(p => p.PhotoFieldId)
          .Concat(result.Select(p => p.InsetFieldId))
          .Concat(result.Select(p => p.Inset2FieldId))
          .Concat(result.Select(p => p.Inset3FieldId))
          .Concat(result.Select(p => p.Inset4FieldId))
          .Where(k => k.HasValue)
          .Cast<int>()
          .Distinct()
          .OrderBy(k => k)
          .ToList();

        conn.Close();
        ids.Any().Should().BeTrue();
      }
    }
  }
}