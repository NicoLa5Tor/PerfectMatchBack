using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Gender
{
    public int IdGender { get; set; }

    public string GenderName { get; set; } = null!;

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
