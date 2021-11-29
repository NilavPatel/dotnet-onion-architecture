using System.ComponentModel.DataAnnotations;
using MyApp.Domain.Enums;

namespace MyApp.Application.Models.Requests
{
    public class CreateUserReq
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmailId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public UserStatus Status { get; set; }
    }
}
