using SagaPlay.Shared.Contracts;

namespace NotificationService.Services
{
    public interface INotifyService
    {
        Task<NotificationDTO> SendNotificationAsync(NotificationDTO Message);
    }
}
