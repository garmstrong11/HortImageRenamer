namespace HortImageRenamer.Ui.Config
{
  using System.Collections.Generic;
  using HortImageRenamer.Core;

  public class SettingsService : ISettingsService
  {
     public SettingsService()
    {
      LegalExtensions = ".eps|.esp|.tif|.tiff|.jp|.jpg|.jpeg|.pdf|.bmp|.hmp|.psd";
      TargetExtension = ".tif";
      ImageRoots = new List<string> { @"\\Storage1\Users\garmstrong\FakeHortImages" };
      ThumbnailRoots = new List<string> { @"\\Storage1\Users\garmstrong\FakeHortThumbnails" };
      ConnectionString = @"Data Source=(localdb)\ProjectsV12; Initial Catalog=HortProd; Integrated Security=true";
    }

    public string LegalExtensions { get; set; }

    public string TargetExtension { get; set; }

    public IEnumerable<string> ImageRoots { get; set; }

    public IEnumerable<string> ThumbnailRoots { get; set; }

    public string ConnectionString { get; set; }
  }
}