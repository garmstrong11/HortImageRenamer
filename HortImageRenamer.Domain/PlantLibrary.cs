namespace HortImageRenamer.Domain
{
  public class PlantLibrary
  {
    public int Id { get; set; }
    public int? PhotoFieldId { get; set; }
    public int? InsetFieldId { get; set; }
    public int? Inset2FieldId { get; set; }
    public int? Inset3FieldId { get; set; }
    public int? Inset4FieldId { get; set; }
    public string Name { get; set; }

    #region Overrides of Object

    public override string ToString()
    {
      return string.Format("Name: {0} Id: {1}", Name, Id);
    }

    #endregion
  }
}