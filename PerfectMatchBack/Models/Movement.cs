using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectMatchBack.Models;

public partial class Movement
{
    public int IdMovement { get; set; }

    public int? IdSeller { get; set; }

    public int? IdBuyer { get; set; }

    public int? IdPublication { get; set; }

    public double? Amount { get; set; }
    public virtual User? IdBuyerNavigation { get; set; }
    public virtual Publication? IdPublicationNavigation { get; set; }
    public virtual User? IdSellerNavigation { get; set; }
}
