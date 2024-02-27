using System;
using System.Collections.Generic;

namespace WaveChat.Context.Entities.Messages;

public partial class Channelstype
{
    public int Idchanneltype { get; set; }

    public string? Typename { get; set; }

    public int? Idchannel { get; set; }

    public virtual Channel? IdchannelNavigation { get; set; }
}
