using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class UserProgress
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CourceId { get; set; }

    public int TaskId { get; set; }

    public double? Point { get; set; }

    public virtual Cource Cource { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
