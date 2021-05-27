using System;
using MyApp.Domain.Core;

namespace MyApp.Domain.Models
{
    public class User : AuditableEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
