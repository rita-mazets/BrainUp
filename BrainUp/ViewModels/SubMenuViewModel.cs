using BrainUp.Models;

namespace BrainUp.ViewModels
{
    public class SubMenuViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int MenuId { get; set; }

        public virtual Menu Menu { get; set; } = null!;

        public  List<Models.Task> Tasks { get; } = new List<Models.Task>();

        public Tuple<int, int> MenuSubMenuIds { get; set; }
    }
}
