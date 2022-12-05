using MyApp.Domain.Entities;

namespace MyApp.Application.Models.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }

        public UserDTO(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailId = user.EmailId;
            Status = (int)user.Status;
            StatusText = user.Status.ToString();
        }
    }
}