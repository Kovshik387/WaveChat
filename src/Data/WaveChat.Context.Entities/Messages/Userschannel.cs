using System;
using System.Collections.Generic;

namespace WaveChat.Context.Entities.Messages;

public partial class Userschannel
{
    public int Iduserchannel { get; set; }

    public int? Channelid { get; set; }

    public virtual Channel? Channel { get; set; }
}
