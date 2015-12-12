namespace HortImageRenamer.Ui.Config
{
  using System.Collections.Generic;

  public static class TestRenameConfig
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
      get { return new List<string> { @"\\Storage1\Users\garmstrong\FakeHortImages" }; }
    }

    public static IEnumerable<string> ThumbnailRoots
    {
      get { return new List<string> { @"\\Storage1\Users\garmstrong\FakeHortThumbnails" }; }
    }

    public static string ConnectionString
    {
      get { return @"Data Source=(localdb)\ProjectsV12; Initial Catalog=HortProd; Integrated Security=true"; }

    }
  }
}