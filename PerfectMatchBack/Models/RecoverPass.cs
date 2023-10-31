using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class RecoverPass
{
    public int IdRecover { get; set; }

    public int IdUser { get; set; }

    public string Token { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual User IdUserNavigation { get; set; }
}
