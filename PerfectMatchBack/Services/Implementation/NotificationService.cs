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
            await _context.AddAsync(onotification);
            await _context.SaveChangesAsync();
            var onotification2 = new Notification()
                {
            IdUserFK = notification.IdUser,
            IdUser = notification.IdUserFK,
            IdPublication = notification.IdPublication,
            IdMovement = onotification.IdMovement,
            AccessLink = notification.AccessLink,
            TypeNotification = notification.TypeNotification+1,
                };

            await _context.AddAsync(onotification2);
            await _context.SaveChangesAsync();
            
            await _hubContext.Clients.All.SendAsync("ReceiveMessage",
            notification, notification.Description);
        }

        public async Task<NotificationDTO> GetNotification(int id)
        {

            return _mapper.Map<NotificationDTO>(await _context.Notifications
               .Where(x => x.IdNotification == id)
               .Include(x => x.IdPublicationNavigation.Images)
               .Include(x => x.IdMovementNavigation.IdPublicationNavigation.Images)
               .Include(x => x.IdMovementNavigation).FirstAsync());
        }
        public async Task<List<NotificationDTO>> GetNotifications(int id)
        {
            var list = await _context.Notifications
               .Where(x => x.IdUser == id)
               .Include(x => x.IdMovementNavigation.IdPublicationNavigation.Images)
               .Include(x => x.IdUserFKNavigation)
               .Include(x => x.IdUserNavigation).ToListAsync();
            list.Reverse();
            return _mapper.Map<List<NotificationDTO>>(list);

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
