using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Commons;

namespace WaveChat.Context.Entities.Messages;

public partial class Channel : EntityBase
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Channelstype> Channelstypes { get; set; } = new List<Channelstype>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<Userschannel> Userschannels { get; set; } = new List<Userschannel>();
}
