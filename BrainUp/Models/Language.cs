using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Language
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Symbol { get; set; }

    public virtual ICollection<Cource> Cources { get; } = new List<Cource>();
}
