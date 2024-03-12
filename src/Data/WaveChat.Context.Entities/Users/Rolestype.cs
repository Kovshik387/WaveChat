using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Commons;

namespace WaveChat.Context.Entities.Users;

public partial class Rolestype : EntityBase
{
    public string? Rolename { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
