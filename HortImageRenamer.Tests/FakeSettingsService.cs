namespace HortImageRenamer.Tests
{
  using HortImageRenamer.Core;

  public class FakeSettingsService : ISettingsService
  {
    #region Implementation of ISettingsSevice

    public string LegalExtensions
    {
      get { return ".eps|.esp|.tif|.tiff|.jpg|.jpeg|.pdf|.bmp"; }
    }

    public string TargetExtension
    {
      get { return ".tif"; }
    }

    public string ThumbnailRoot
    {
      get { return @"\\DMZ\HortThumbnails"; }
    }

    #endregion
  }
}