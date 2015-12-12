namespace HortImageRenamer.Core
{
  using System.Collections.Generic;

  public interface IRenameConfig
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
    /// The root paths of the directories that contains high resolution images.
    /// </summary>
    IEnumerable<string> ImageRoots { get; }

    /// <summary>
    /// The root paths of the directories that contains thumbnail images.
    /// </summary>
    IEnumerable<string> ThumbnailRoots { get; }

    /// <summary>
    /// The connection string for hort db, test or production
    /// </summary>
    string ConnectionString { get; } 
  }
}