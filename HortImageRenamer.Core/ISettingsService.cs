namespace HortImageRenamer.Core
{
  using System.Collections.Generic;

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
    IEnumerable<string> ImageRoots { get; }

    /// <summary>
    /// The root path of the directory that contains thumbnail images.
    /// </summary>
    IEnumerable<string> ThumbnailRoots { get; }

    /// <summary>
    /// The connection string name for the db connection
    /// </summary>
    string ConnectionStringName { get; }
  }
}