using System;
using System.Collections.Generic;

namespace BrainUp.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CourceId { get; set; }

    public virtual Cource Cource { get; set; } = null!;

    public virtual ICollection<SubMenu> SubMenus { get; } = new List<SubMenu>();
}
