using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Price
{
    public int Id { get; set; }

    public bool IsPaid { get; set; }

    public decimal? Price1 { get; set; }

    public string CurrencySymbol { get; set; } = null!;

    public virtual ICollection<Cource> Cources { get; } = new List<Cource>();

    public virtual Currency CurrencySymbolNavigation { get; set; } = null!;
}
