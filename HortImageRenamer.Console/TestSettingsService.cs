namespace HortImageRenamer.Console
{
  using System.Collections.Generic;
  using HortImageRenamer.Core;

  public class TestSettingsService : ISettingsService
  {
    public string LegalExtensions
    {
      get { return ".eps|.esp|.tif|.tiff|.jp|.jpg|.jpeg|.pdf|.bmp"; }
    }

    public string TargetExtension
    {
      get { return ".tif"; }
    }

    public IEnumerable<string> ImageRoots
    {
      get { return new List<string> {@"\\Storage1\Users\garmstrong\FakeHortImages"}; }
    }

    public IEnumerable<string> ThumbnailRoots
    {
      get { return new List<string> {@"\\Storage1\Users\garmstrong\FakeHortThumbnails"}; }
    }

    public string ConnectionStringName
    {
      get { return "HortProdTest"; }
    }
}