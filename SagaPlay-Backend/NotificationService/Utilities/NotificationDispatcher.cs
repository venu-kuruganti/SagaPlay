using SagaPlay.Shared.Contracts;
using NotificationService.Services;

namespace NotificationService.Utilities
{
    /// <summary>
    /// This class helps send the notification in the correct manner based on its type.
    /// </summary>
    public class NotificationDispatcher : INotificationDispatcher
    {
        private readonly IEnumerable<INotifyService> _services;

        public NotificationDispatcher(IEnumerable<INotifyService> services)
        {
            _services = services;
        }

        public async Task SendAsync(NotificationDTO message)
        {
            var service = _services.FirstOrDefault(s =>
            (message.Type == NotificationType.Push && s is PushNotifyService) ||
            (message.Type == NotificationType.Email && s is EmailNotifyService));

            if (service == null)
            {
                throw new InvalidOperationException("No notification service found for type.");
            }

            await service.SendNotificationAsync(message);
        }
    }
}
