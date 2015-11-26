namespace HortImageRenamer.Core
{
  public interface ISettingsService
  {
    /// <summary>
    /// A concatenated string of file extensions that are not subject to renaming.
    /// </summary>
    string LegalExtensions { get; }

    /// <summary>
    /// The extension to add when renaming.
    /// </summary>
    string TargetExtension { get; }

    /// <summary>
    /// The root path of the directory that contains high resolution images.
    /// </summary>
    string ImageRoot { get; }

    /// <summary>
    /// The root path of the directory that contains thumbnail images.
    /// </summary>
    string ThumbnailRoot { get; }
  }
}