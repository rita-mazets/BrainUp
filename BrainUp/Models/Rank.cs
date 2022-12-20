using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Rank
{
    public int Id { get; set; }

    public int Value { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CourceId { get; set; }

    public virtual Cource Cource { get; set; } = null!;
}
