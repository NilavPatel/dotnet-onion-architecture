using System;

namespace MyApp.Domain.Core.Models
{
    public class AuditableEntity : BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}