﻿using System;
using System.Collections.Generic;

namespace WaveChat.Context.Entities.DashBoard;

public partial class News
{
    public int Idnew { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int Likes { get; set; }

    public virtual ICollection<Newscomment> Newscomments { get; set; } = new List<Newscomment>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
