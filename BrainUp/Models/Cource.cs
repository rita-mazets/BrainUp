using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Cource
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? ShotDiscription { get; set; }

    public string? Discription { get; set; }

    public string? StorageLink { get; set; }

    public string? Image { get; set; }

    public int TeacherId { get; set; }

    public int LevelId { get; set; }

    public int LanguageId { get; set; }

    public int CategoryId { get; set; }

    public int PriceId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();

    public virtual Language Language { get; set; } = null!;

    public virtual Level Level { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; } = new List<Menu>();

    public virtual Price Price { get; set; } = null!;

    public virtual ICollection<Rank> Ranks { get; } = new List<Rank>();

    public virtual User Teacher { get; set; } = null!;

    public virtual ICollection<UserProgress> UserProgresses { get; } = new List<UserProgress>();

    public virtual ICollection<User> Students { get; } = new List<User>();
}
