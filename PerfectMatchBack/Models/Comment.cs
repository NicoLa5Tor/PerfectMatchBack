using System;
using System.Collections.Generic;

namespace PerfectMatchBack.Models;

public partial class Comment
{
    public int IdComment { get; set; }

    public int? IdPublication { get; set; }

    public int? IdUser { get; set; }

    public int? IdCommentFk { get; set; }

    public string? Comment1 { get; set; }

    public virtual Comment? IdCommentFkNavigation { get; set; }

    public virtual Publication? IdPublicationNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<Comment> InverseIdCommentFkNavigation { get; set; } = new List<Comment>();
}
