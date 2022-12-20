using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Log
{
    public int Id { get; set; }

    public string? Table { get; set; }

    public string? Action { get; set; }

    public string? Message { get; set; }

    public DateTime? CreatedDate { get; set; }
}
