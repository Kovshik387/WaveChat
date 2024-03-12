using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Boards;
using WaveChat.Context.Entities.Messages;
using WaveChat.Context.Entities.Users;
using WaveChat.Context.Entities.DashBoard;
using WaveChat.Context.Entities.Commons;

namespace WaveChat.Context.Entities;

public partial class Photo : EntityBase
{
    public string Image { get; set; } = null!;

    public string Bucket { get; set; } = null!;

    public Guid? Iduser { get; set; }

    public int? Idboard { get; set; }

    public int? Idnew { get; set; }

    public int? Idmessage { get; set; }

    public int? Idchannel { get; set; }

    public virtual Board? IdboardNavigation { get; set; }

    public virtual Channel? IdchannelNavigation { get; set; }

    public virtual Message? IdmessageNavigation { get; set; }

    public virtual News? IdnewNavigation { get; set; }

    public virtual User? IduserNavigation { get; set; }
}
