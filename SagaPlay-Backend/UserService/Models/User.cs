
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    
    
    public class User
    {        
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; } //Salted Password.

        public byte[] PasswordSalt { get; set; } //To unsalt the password and compare it.

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        //Navigation Properties
        public UserProfile UserProfile { get; set; }
        public UserPreferences UserPreferences { get; set; }

    }
}
