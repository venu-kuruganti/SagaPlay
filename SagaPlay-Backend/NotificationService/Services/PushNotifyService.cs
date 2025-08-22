using Microsoft.AspNetCore.SignalR;
using NotificationService.Utilities;
using SagaPlay.Shared.Contracts;

namespace NotificationService.Services
{
    public class PushNotifyService : INotifyService
    {
        private readonly IHubContext<NotificationHub> _hub;

        public PushNotifyService(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }

        public async Task<NotificationDTO> SendNotificationAsync(NotificationDTO Message)
        {
            //Targeting a specific user based on UserID, if UserId is not null. This may not be implemented right away.
            if (Message.UserId != null)
            {                
                await _hub.Clients.User(Message.UserId.ToString()).SendAsync("ReceiveNotification", Message);
            }
            else //Broadcasting to all connected clients
            {
                await _hub.Clients.All.SendAsync("ReceiveNotification", Message);
            }

            return Message;
        }

        /*
         * Step 4: Client-side (example in JavaScript/TypeScript)
            import * as signalR from "@microsoft/signalr";

            const connection = new signalR.HubConnectionBuilder()
                .withUrl("/hubs/notification")
                .build();

            connection.on("ReceiveNotification", (message) => {
                console.log("Notification received:", message);
                // show toast, update UI, etc.
            });

            await connection.start();
         */
    }
}
