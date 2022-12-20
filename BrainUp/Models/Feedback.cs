using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public string Message { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int CourceId { get; set; }

    public virtual Cource Cource { get; set; } = null!;
}
