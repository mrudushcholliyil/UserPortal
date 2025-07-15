using UserPortal.Domain.Entities.Interfaces;

namespace UserPortal.Domain.Entities
{
    /// <summary>
    /// Class representing a base entity with a unique identifier.
    /// It gives unique name to all derived entities in the domain, also helps
    /// generic repositories to work with any entity type.
    /// </summary>
    public abstract class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}
