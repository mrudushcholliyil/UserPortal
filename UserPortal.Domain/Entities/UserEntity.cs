namespace UserPortal.Domain.Entities
{
    /// <summary>
    /// User Entity class, user id will automatically generated.
    /// </summary>
    public class UserEntity : Entity
    {        
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Phone { get; set; } = null;        
    }
}
