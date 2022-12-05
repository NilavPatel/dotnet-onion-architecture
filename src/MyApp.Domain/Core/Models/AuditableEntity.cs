namespace MyApp.Domain.Core.Models
{
    public class AuditableEntity : BaseEntity
    {
        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTimeOffset? LastModifiedOn { get; set; }
    }
}