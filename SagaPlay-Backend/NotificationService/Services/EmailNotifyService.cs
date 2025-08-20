using SagaPlay.Shared.Contracts;
using NETCore.MailKit.Core;
using System.Threading.Tasks;


namespace NotificationService.Services
{
    public class EmailNotifyService : INotifyService
    {
        private readonly IEmailService _emailService;

        public EmailNotifyService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task<NotificationDTO> SendNotificationAsync(NotificationDTO Message)
        {
            await _emailService.SendAsync(Message.To, Message.Subject, Message.Message, isHtml: Message.IsHTML);
            return Message;
        }
    }
}
