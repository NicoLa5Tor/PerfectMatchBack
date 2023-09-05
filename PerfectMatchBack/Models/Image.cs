using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Image
{
    public int IdImage { get; set; }

    public int IdPublication { get; set; }

    public string DataImage { get; set; } = null!;

    public virtual Publication IdPublicationNavigation { get; set; } = null!;
}
