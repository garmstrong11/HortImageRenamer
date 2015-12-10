namespace HortImageRenamer.ServiceImplementations
{
  using System.Configuration;
  using HortImageRenamer.Core;
  using HortImageRenamer.ServiceInterfaces;

  public class TestConnectionService : IConnectionService
  {
    private readonly ISettingsService _settings;

    public TestConnectionService(ISettingsService settings)
    {
      _settings = settings;
    }

    public string GetConnectionString()
    {
      return ConfigurationManager
        .ConnectionStrings[_settings.ConnectionStringName]
        .ConnectionString;
    }
  }
}