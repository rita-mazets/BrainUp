using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Discription { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? RoleId { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Cource> Cources { get; } = new List<Cource>();

    public virtual ICollection<CreditCard> CreditCards { get; } = new List<CreditCard>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserProgress> UserProgresses { get; } = new List<UserProgress>();

    public virtual ICollection<Cource> CourcesNavigation { get; } = new List<Cource>();
}
