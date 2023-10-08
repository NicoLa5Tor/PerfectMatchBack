using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Notification
{
    public int IdNotifacation { get; set; }

    public int? IdUser { get; set; }

    public string Description { get; set; }

    public DateTime? Date { get; set; }

    public virtual User IdUserNavigation { get; set; }
}
