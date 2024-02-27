using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Users;

namespace WaveChat.Context.Entities.Boards;

public partial class Dependency
{
    public int Iddependency { get; set; }

    public int Idboard { get; set; }

    public int Iduser { get; set; }

    public virtual Board IdboardNavigation { get; set; } = null!;

    public virtual User IduserNavigation { get; set; } = null!;
}
