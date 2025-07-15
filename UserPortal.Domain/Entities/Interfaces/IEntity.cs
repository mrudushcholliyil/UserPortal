namespace UserPortal.Domain.Entities.Interfaces
{
    /// <summary>
    /// Interface for entities in the User Portal domain.
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
