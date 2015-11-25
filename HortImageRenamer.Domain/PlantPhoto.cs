namespace HortImageRenamer.Domain
{
  using System;
  using System.IO;

  public class PlantPhoto
  {
    public string PhotoId { get; set; }
    public string PhotoPath { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string ExtensionLower
    {
      get { return Path.GetExtension(PhotoId.ToLower()); }
    }

    public override string ToString()
    {
      return PhotoId;
    }
  }
}