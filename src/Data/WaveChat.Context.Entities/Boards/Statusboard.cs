using System;
using System.Collections.Generic;
using WaveChat.Context.Entities.Commons;

namespace WaveChat.Context.Entities.Boards;

public partial class Statusboard : EntityBase
{
    public string Status { get; set; } = null!;
}
