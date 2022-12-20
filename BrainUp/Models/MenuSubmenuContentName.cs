using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class MenuSubmenuContentName
{
    public string MenuName { get; set; } = null!;

    public int CourceId { get; set; }

    public string SubmenuName { get; set; } = null!;

    public string ContentName { get; set; } = null!;

    public int ContentId { get; set; }
}
