namespace HortImageRenamer.Console
{
  using System.Collections.Generic;
  using HortImageRenamer.Core;

  public class ProductionSettingsService : ISettingsService
  {
    public string LegalExtensions
    {
      get { return ".eps|.esp|.tif|.tiff|.jp|.jpg|.jpeg|.pdf|.bmp|.hmp|.psd"; }
    }

    public string TargetExtension
    {
      get { return ".tif"; }
    }

    public IEnumerable<string> ImageRoots
    {
      get { return new List<string> { @"\\Storage1\HortImages", @"\\nasdee\HortImages" }; }
    }

    public IEnumerable<string> ThumbnailRoots
    {
      get { return new List<string> { @"\\DMZ\HortThumbnails" }; }
    }

    public string ConnectionString
    {
      get { return @"Data Source=Armadillo; Initial Catalog=HortProduction; Integrated Security=true"; }
    }
  }
}