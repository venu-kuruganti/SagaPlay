using Microsoft.EntityFrameworkCore;

namespace UserService.Models
{
    public class UserProfile
    {
        public Guid UserProfileId { get; set; }

        //Foreign Key to Users
        public Guid UserId { get; set; }

        //Navigation property
        public User User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }      

        public string EmailAddress { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Bio { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string Country { get; set; }

        public string PhoneNumber { get; set; }


    }
}
