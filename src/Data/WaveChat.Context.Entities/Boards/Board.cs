using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Commons;
using WaveChat.Context.Entities.Users;

namespace WaveChat.Context.Entities.Boards;

public partial class Board : EntityBase
{
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateOnly Issuedate { get; set; }

    public DateOnly Deadlinedate { get; set; }

    public int Userboard { get; set; }

    public virtual ICollection<Dependency> Dependencies { get; set; } = new List<Dependency>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual User UserboardNavigation { get; set; } = null!;
}
