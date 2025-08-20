namespace SagaPlay.Shared.Contracts
{
    public class NotificationDTO
    {
        public Guid UserId { get; set; } //Push and toast targeting
        public string To { get; set; } //For email notification
        public string Subject { get; set; }        //For email
        public string Message { get; set; }     //For push and email notification
        public DateTime CreatedOn { get; set; } //For both
        public NotificationType Type { get; set; }
        public bool IsHTML { get; set; } = false;

    }

    public enum NotificationType
    {
        Email,
        Push
    }
}
