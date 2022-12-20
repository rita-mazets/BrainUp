using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Option
{
    public int Id { get; set; }

    public string Option1 { get; set; } = null!;

    public bool IsTrue { get; set; }

    public int TaskId { get; set; }

    public virtual Task Task { get; set; } = null!;
}
