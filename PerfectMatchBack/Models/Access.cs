using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Access
{
    public int IdAccess { get; set; }
    public string Password { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
