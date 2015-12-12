﻿namespace HortImageRenamer.Console
{
  using System.Collections.Generic;
  using HortImageRenamer.Core;

  public class ProductionSettingsService : ISettingsService
  {
    public ProductionSettingsService()
    {
      LegalExtensions = ".eps|.esp|.tif|.tiff|.jp|.jpg|.jpeg|.pdf|.bmp|.hmp|.psd";
      TargetExtension = ".tif";
      ImageRoots = new List<string> { @"\\Storage1\HortImages", @"\\nasdee\HortImages" };
      ThumbnailRoots = new List<string> { @"\\DMZ\HortThumbnails" };
      ConnectionString = @"Data Source=Armadillo; Initial Catalog=HortProduction; Integrated Security=true"; 
    }

    public string LegalExtensions { get; set; }
    public string TargetExtension { get; set; }
    public IEnumerable<string> ImageRoots { get; set; }
    public IEnumerable<string> ThumbnailRoots { get; set; }
    public string ConnectionString { get; set; }
  }
}