using SagaPlay.Shared.Contracts;

namespace NotificationService.Services
{
    public class PushNotifyService : INotifyService
    {
        public Task<NotificationDTO> SendNotificationAsync(NotificationDTO Message)
        {
            throw new NotImplementedException("Yet to implement this function!");
        }
    }
}
