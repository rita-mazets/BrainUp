using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Content
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public int SubMenuId { get; set; }

    public string Name { get; set; } = null!;

    public virtual SubMenu SubMenu { get; set; } = null!;
}
