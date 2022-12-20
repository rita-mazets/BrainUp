using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class MenuSubmenuTaskName
{
    public string MenuName { get; set; } = null!;

    public int CourceId { get; set; }

    public string SubmenuName { get; set; } = null!;

    public string TaskName { get; set; } = null!;

    public int TaskId { get; set; }
}
