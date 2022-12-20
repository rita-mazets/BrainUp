using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Currency
{
    public string Symbol { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double Usdequivalent { get; set; }

    public virtual ICollection<CreditCard> CreditCards { get; } = new List<CreditCard>();

    public virtual ICollection<Price> Prices { get; } = new List<Price>();
}
