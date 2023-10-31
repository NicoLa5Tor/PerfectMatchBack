using PerfectMatchBack.DTOs;

namespace PerfectMatchBack.Services.Contract
{
    public interface INotificationService
    {
        Task<List<NotificationDTO>> GetNotifications(int id);
        Task UpdateNotification(int id);
        Task<List<NotificationDTO>> GetNotifications();
        Task AddNotification(NotificationDTO notification);
        Task RemoveNotification(int id);
    }
}
