using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Commons;
using WaveChat.Context.Entities.Users;

namespace WaveChat.Context.Entities.Messages;

public partial class Message : EntityBase
{
    public DateTime Senddate { get; set; }

    public bool Isread { get; set; }

    public string Content { get; set; } = null!;

    public int Idchannel { get; set; }

    public int Iduser { get; set; }

    public virtual Channel IdchannelNavigation { get; set; } = null!;

    public virtual User IduserNavigation { get; set; } = null!;

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
