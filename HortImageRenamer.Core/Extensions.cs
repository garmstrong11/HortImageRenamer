namespace HortImageRenamer.Core
{
  public static class Extensions
  {
    public static string AddTif(this string baseName)
    {
      return string.Format("{0}.tif", baseName);
    }
  }
}