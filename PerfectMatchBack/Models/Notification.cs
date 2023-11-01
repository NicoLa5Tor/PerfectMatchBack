﻿using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Notification
{
    public int IdNotification { get; set; }
    public int? IdMovement { get; set; }
    public int? IdUser { get; set; }
    public string? AccessLink { get; set; }
    public int State { get; set; } = 0;
    public int TypeNotification { get; set; }
    public virtual Movement? IdMovementNavigation { get; set; }
    public virtual User? IdUserNavigation { get; set; }
}
