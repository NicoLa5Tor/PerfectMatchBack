﻿namespace PerfectMatchBack.DTOs
{
    public class NotificationDTO
    {
        public int IdNotification { get; set; }
        public int? IdUser { get; set; }
        public int? IdMovement { get; set; }
        public string NameUser { get; set; }
        public string NamePublication { get; set; }
        public string ImagePublication { get; set; }
        public string? Description { get; set; }
        public string Date { get; set; }
        public string? AccessLink { get; set; }
        public int State { get; set; } = 0;
        public int TypeNotification { get; set; }
    }
}
