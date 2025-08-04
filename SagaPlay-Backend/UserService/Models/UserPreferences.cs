using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace UserService.Models
{
   
    public class UserPreferences
    {
        public Guid UserPreferencesId { get; set; }

        //Foreign Key
        public Guid UserId { get; set; }

        //Navigation Property
        public User User { get; set; }

        public string Theme { get; set; }

        public string Language { get; set; }

        public string NotificationSettings { get; set; }

        public string PlaybackQualitySettings { get; set; }

        public bool ReceiveNewsLetter { get; set; }

    }
}
