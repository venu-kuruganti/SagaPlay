
using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; } //Salted Password.

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }



    }
}
