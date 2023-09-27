using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectMatchBack.Models;

public partial class Access
{
    [Key]
    public int IdAccess { get; set; }

    public string Password { get; set; } = null!;
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
