
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models
{
    
    
    public class User
    {        
        public Guid Id { get; set; }

        public string UserName { get; set; }
       

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        //Navigation Properties
        public UserProfile UserProfile { get; set; }
        public UserPreferences UserPreferences { get; set; }

    }
}
