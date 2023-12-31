﻿using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class City
{
    public int IdCity { get; set; }

    public int? IdDepartment { get; set; }

    public string CityName { get; set; }

    public virtual Department IdDepartmentNavigation { get; set; }

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
