using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Commons;
using WaveChat.Context.Entities.Users;

namespace WaveChat.Context.Entities.DashBoard;

public partial class Newscomment : EntityBase
{
    public string Content { get; set; } = null!;

    public DateOnly Commentdate { get; set; }

    public int Idnew { get; set; }

    public int Iduser { get; set; }

    public virtual News IdnewNavigation { get; set; } = null!;

    public virtual User IduserNavigation { get; set; } = null!;
}
