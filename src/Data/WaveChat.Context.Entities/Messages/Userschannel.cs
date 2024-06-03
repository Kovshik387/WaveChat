using WaveChat.Context.Entities.Commons;
using WaveChat.Context.Entities.Users;

namespace WaveChat.Context.Entities.Messages;

public partial class Userschannel : EntityBase
{
    public int? Channelid { get; set; }
    public int? Userid { get; set; }
    public virtual Channel? Channel { get; set; }
    public virtual User? User { get; set; }
}
