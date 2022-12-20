using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Task
{
    public int Id { get; set; }

    public string Condition { get; set; } = null!;

    public double? Point { get; set; }

    public string? Image { get; set; }

    public int? SubMenuId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Option> Options { get; } = new List<Option>();

    public virtual SubMenu? SubMenu { get; set; }

    public virtual ICollection<UserProgress> UserProgresses { get; } = new List<UserProgress>();
}
