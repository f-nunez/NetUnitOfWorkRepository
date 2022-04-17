namespace Fnunez.UnitOfWork.Models.Abstractions;

public abstract class ModelBase
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
}