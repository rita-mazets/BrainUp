using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class SubMenu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int MenuId { get; set; }

    public virtual ICollection<Content> Contents { get; } = new List<Content>();

    public virtual Menu Menu { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; } = new List<Task>();
}
