using System;
using System.Collections.Generic;

namespace WaveChat.Context.Entities.Boards;

public partial class Statusboard
{
    public int Idstatusboard { get; set; }

    public string Status { get; set; } = null!;
}
