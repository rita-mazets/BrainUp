using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class FullCource
{
    public string Name { get; set; } = null!;

    public string? Discription { get; set; }

    public string? ShortDiscription { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? StorageLink { get; set; }

    public string? TeacherFirstName { get; set; }

    public string? TeacherLastName { get; set; }

    public string? TeacherDiscription { get; set; }

    public string? TeacherImage { get; set; }

    public string CategoryName { get; set; } = null!;

    public string LevelName { get; set; } = null!;

    public string LanguageName { get; set; } = null!;

    public bool IsPaid { get; set; }

    public decimal? Price { get; set; }

    public string Symbol { get; set; } = null!;

    public double Usdequivalent { get; set; }
}
