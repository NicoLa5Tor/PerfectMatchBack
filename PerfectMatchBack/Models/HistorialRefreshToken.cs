using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class HistorialRefreshToken
{
    public int IdHistorialToken { get; set; }

    public int? IdUser { get; set; }

    public string Token { get; set; }

    public string RefreshToken { get; set; }

    public DateTime? DateCreate { get; set; }

    public DateTime? DateExpiration { get; set; }

    public bool? IsActive { get; set; }

    public virtual User IdUserNavigation { get; set; }
}
