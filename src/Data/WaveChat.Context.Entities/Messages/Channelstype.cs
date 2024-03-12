using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Commons;

namespace WaveChat.Context.Entities.Messages;

public partial class Channelstype : EntityBase
{
    public string? Typename { get; set; }

    public int? Idchannel { get; set; }

    public virtual Channel? IdchannelNavigation { get; set; }
}
