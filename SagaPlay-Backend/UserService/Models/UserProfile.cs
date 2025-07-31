namespace UserService.Models
{
    public class UserProfile
    {
        public Guid Id { get; set; }
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
