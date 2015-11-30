namespace HortImageRenamer.ServiceImplementations
{
  using System.Linq;
  using HortImageRenamer.Domain;
  using HortImageRenamer.ServiceInterfaces;

  public class PlantLibraryService : IPlantLibraryService
  {
    private readonly IPlantLibraryRepository _repository;

    public PlantLibraryService(IPlantLibraryRepository repository)
    {
      _repository = repository;
    }
    
    public string GetImageFieldIds()
    {
      var result = _repository.GetImageFieldIds().ToList();

      var ids = result.Select(p => p.PhotoFieldId)
        .Concat(result.Select(p => p.InsetFieldId))
        .Concat(result.Select(p => p.Inset2FieldId))
        .Concat(result.Select(p => p.Inset3FieldId))
        .Concat(result.Select(p => p.Inset4FieldId))
        .Where(k => k.HasValue)
        .Cast<int>()
        .Distinct()
        .OrderBy(k => k)
        .ToList();

      return string.Join(", ", ids);
    }
  }
}