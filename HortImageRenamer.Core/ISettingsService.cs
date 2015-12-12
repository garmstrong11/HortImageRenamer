﻿namespace HortImageRenamer.Core
{
  using System.Collections.Generic;

  public interface ISettingsService
  {
    /// <summary>
    /// A concatenated string of file extensions that are not subject to renaming.
    /// </summary>
    string LegalExtensions { get; set; }

    /// <summary>
    /// The extension to add when renaming.
    /// </summary>
    string TargetExtension { get; set; }

    /// <summary>
    /// The root paths of the directories that contains high resolution images.
    /// </summary>
    IEnumerable<string> ImageRoots { get; set; }

    /// <summary>
    /// The root paths of the directories that contains thumbnail images.
    /// </summary>
    IEnumerable<string> ThumbnailRoots { get; set; }

    /// <summary>
    /// The connection string for hort db, test or production
    /// </summary>
    string ConnectionString { get; set; }
  }
}