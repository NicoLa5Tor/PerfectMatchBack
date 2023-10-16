using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Notification
{
    public int IdNotification { get; set; }
    public int? IdUser { get; set; }
    public int? IdPublication { get; set; }
    public string? AccessLink { get; set; }
    public DateTime? Date { get; set; }
    public int State { get; set; } = 0;
    public int TypeNotification { get; set; }
    public virtual User? IdUserNavigation { get; set; }
    public virtual Publication? IdPublicationNavigation { get; set; }
}
