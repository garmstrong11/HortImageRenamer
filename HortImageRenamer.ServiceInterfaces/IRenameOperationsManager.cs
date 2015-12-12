namespace HortImageRenamer.ServiceInterfaces
{
  using System.Collections.Generic;
  using HortImageRenamer.Domain;

  public interface IRenameOperationsManager
  {
    void Initialize();
    void RenameAll();
    void RenameList(IEnumerable<string> photoIds);
    void RenameSingle(string photoId);
    void RenameCount(int count);

    //ILogger FileLogger { get; set; }
    ILogger ConsoleLogger { get; set; }
  }
}