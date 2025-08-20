using SagaPlay.Shared.Contracts;

namespace NotificationService.Utilities
{
    public interface INotificationDispatcher
    {
        public Task SendAsync(NotificationDTO message);
    }
}
