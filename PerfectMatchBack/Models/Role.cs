using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string RoleName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
