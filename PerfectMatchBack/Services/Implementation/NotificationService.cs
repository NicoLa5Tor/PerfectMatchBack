using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using PerfectMatchBack.DTOs;
using PerfectMatchBack.Models;
using PerfectMatchBack.Services.Contract;

public enum NotificationType{
    infoNotification =0,
    moveNotification= 1,
    sellNotification = 2
}

namespace PerfectMatchBack.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly PetFectMatchContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<EmailHub> _hubContext;

        public NotificationService(PetFectMatchContext context, IMapper mapper, IHubContext<EmailHub> hubContext)
        {
            _hubContext = hubContext;
            _context = context;
            _mapper = mapper;
        }

        public async Task AddNotification(NotificationDTO notification)
        {
            var onotification = _mapper.Map<Notification>(notification);
            if (notification.TypeNotification == (int)NotificationType.infoNotification)
            {
                await _context.AddAsync(onotification);
            }
            else
            {
                await _context.AddAsync(onotification);
                var seller = _context.Users.FirstOrDefault(x => x.Publications.FirstOrDefault(x => x.IdPublication == onotification.IdUser) != null);
                if (seller == null)
                {
                    return;
                }
                onotification.IdMovement = seller.IdUser;
                onotification.TypeNotification =(int) NotificationType.sellNotification;
                await _context.AddAsync(onotification);
            }
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveMessage",
            notification, notification.Description);
        }

        public async Task<NotificationDTO> GetNotification(int id)
        {

            return _mapper.Map<NotificationDTO>(await _context.Notifications
               .Where(x => x.IdNotification == id)
               //.Include(x => x.IdMovementNavigation.IdPublicationNavigation.Images)
               .Include(x => x.IdMovementNavigation).FirstAsync());
        }
        public async Task<List<NotificationDTO>> GetNotifications(int id)
        {
            return _mapper.Map<List<NotificationDTO>>(await _context.Notifications
               .Where(x => x.IdUser == id)
               .Include(x => x.IdMovementNavigation.IdPublicationNavigation.Images)
               .Include(x => x.IdUserNavigation).ToListAsync());

        }

        public async Task<List<NotificationDTO>> GetNotifications()
        {
            return _mapper.Map<List<NotificationDTO>>(await _context.Notifications.ToListAsync());
        }
        public async Task UpdateNotification(int id)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(x => x.IdNotification == id);
            if (notification == null)
            {
                return;
            }
            notification.State = 1;
            _context.Update(notification);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveNotification(int id)
        {
            var notification = await _context.Notifications.FirstOrDefaultAsync(x => x.IdNotification == id);
            if (notification == null)
            {
                return;
            }
            _context.Remove(notification);
            await _context.SaveChangesAsync();
        }
    }
}
