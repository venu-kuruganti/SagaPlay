using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserService.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }

        [EmailAddress]
        public string UserEmail { get; set; }

        [PasswordPropertyText(true)]
        public string Password { get; set; }
    }
}
