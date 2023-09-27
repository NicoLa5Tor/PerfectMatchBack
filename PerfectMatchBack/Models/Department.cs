using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Department
{
    public int IdDepartment { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
