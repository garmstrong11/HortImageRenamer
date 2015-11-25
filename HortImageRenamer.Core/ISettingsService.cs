namespace HortImageRenamer.Core
{
  public interface ISettingsService
  {
    string LegalExtensions { get; }

    string TargetExtension { get; }
    string ThumbnailRoot { get; }
  }
}