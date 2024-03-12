using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Commons;

namespace WaveChat.Context.Entities.Messages;

public partial class Userschannel : EntityBase
{
    public int? Channelid { get; set; }

    public virtual Channel? Channel { get; set; }
}
