﻿using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class User
{
    public int IdUser { get; set; }

    public int IdRole { get; set; }

    public string? Name { get; set; }

    public DateTime? BirthDate { get; set; }

    public int IdAccess { get; set; }

    public int? IdCity { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Access? IdAccessNavigation { get; set; }

    public virtual City? IdCityNavigation { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Movement> MovementIdBuyerNavigations { get; set; } = new List<Movement>();

    public virtual ICollection<Movement> MovementIdSellerNavigations { get; set; } = new List<Movement>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
