using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Boards;
using WaveChat.Context.Entities.Messages;
using WaveChat.Context.Entities.DashBoard;
using Microsoft.AspNetCore.Identity;
using WaveChat.Context.Entities.Commons;

namespace WaveChat.Context.Entities.Users;

public partial class User : EntityBase
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Lastname { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string Passwordhash { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public DateOnly Registrationdate { get; set; }

    public DateOnly Lastvisitdate { get; set; }

    public int? Roletype { get; set; }

    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();

    public virtual ICollection<Dependency> Dependencies { get; set; } = new List<Dependency>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Newscomment> Newscomments { get; set; } = new List<Newscomment>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual Rolestype? RoletypeNavigation { get; set; }
}
