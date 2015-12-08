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

    public string FullPath
    {
      get { return Path.Combine(PhotoPath, PhotoId); }
    }

    public string GetThumbnailPath(string thumbnailRoot)
    {
      var thumbPath = PhotoPath.Replace(@"\\nasdee\HortImages", thumbnailRoot);
      var thumbFilename = string.Format("{0}.jpg", PhotoId);
      return Path.Combine(thumbPath, thumbFilename);
    }

    public string GetActualImagePath(string replacementRoot)
    {
      return FullPath.Replace(@"\\nasdee\HortImages", replacementRoot);
    }

    public string GetReplacementPath(string replacementRoot, string extension, bool isThumb = false)
    {
      // replacementRoot in the form \\Storage1\HortImages, \\DMZ\HortThumbnails
      var pattern = isThumb ? "{0}{1}.jpg" : "{0}{1}";
      var newPath = PhotoPath.Replace(@"\\nasdee\HortImages", replacementRoot);
      var newFileName = string.Format(pattern, PhotoId, extension);
      return Path.Combine(newPath, newFileName);
    }

    public override string ToString()
    {
      return PhotoId;
    }
  }
}