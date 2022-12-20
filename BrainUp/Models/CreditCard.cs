using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class CreditCard
{
    public int Id { get; set; }

    public string? Number { get; set; }

    public string? Cvvhash { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public double? Balance { get; set; }

    public string? CurrencySymbol { get; set; }

    public string OwnerName { get; set; } = null!;

    public int UserId { get; set; }

    public virtual Currency? CurrencySymbolNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
