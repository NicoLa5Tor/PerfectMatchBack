using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Movement
{
    public int IdMovement { get; set; }

    public int? IdSeller { get; set; }

    public int? IdBuyer { get; set; }

    public int? IdPublication { get; set; }

    public double? Amount { get; set; }

    public DateTime Date { get; set; }

    public virtual User? IdBuyerNavigation { get; set; }

    public virtual Publication? IdPublicationNavigation { get; set; }

    public virtual User? IdSellerNavigation { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
