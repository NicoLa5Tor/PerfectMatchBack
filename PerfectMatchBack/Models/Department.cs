using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Department
{
    public int IdDeparment { get; set; }

    public string DepartamentName { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
