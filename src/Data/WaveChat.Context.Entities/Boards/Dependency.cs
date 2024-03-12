using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Commons;
using WaveChat.Context.Entities.Users;

namespace WaveChat.Context.Entities.Boards;

public partial class Dependency : EntityBase
{
    public int Idboard { get; set; }

    public Guid Iduser { get; set; }

    public virtual Board IdboardNavigation { get; set; } = null!;

    public virtual User IduserNavigation { get; set; } = null!;
}
