using System;
using System.Collections.Generic;

namespace WaveChat.Context.Entities.Users;

public partial class Rolestype
{
    public int Idroletype { get; set; }

    public string? Rolename { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
