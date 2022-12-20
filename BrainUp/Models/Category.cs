using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Cource> Cources { get; } = new List<Cource>();
}
