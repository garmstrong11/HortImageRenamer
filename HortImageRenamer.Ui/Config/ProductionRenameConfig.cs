namespace HortImageRenamer.Ui.Config
{
  using System.Collections.Generic;

  public static class ProductionRenameConfig
  {
    public static string LegalExtensions
    {
      get { return ".eps|.esp|.tif|.tiff|.jp|.jpg|.jpeg|.pdf|.bmp|.hmp|.psd"; }
    }

    public static string TargetExtension
    {
      get { return ".tif"; }
    }

    public static IEnumerable<string> ImageRoots
    {
      get { return new List<string> { @"\\Storage1\HortImages", @"\\nasdee\HortImages" }; }
    }

    public static IEnumerable<string> ThumbnailRoots
    {
      get { return new List<string> { @"\\DMZ\HortThumbnails" }; }
    }

    public static string ConnectionString
    {
      get { return @"Data Source=Armadillo; Initial Catalog=HortProduction; Integrated Security=true"; }
    } 
  }
}