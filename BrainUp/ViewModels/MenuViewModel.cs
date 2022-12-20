using BrainUp.Models;

namespace BrainUp.ViewModels
{
    public class MenuViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int CourceId { get; set; }

        public virtual Cource Cource { get; set; } = null!;

        public  List<SubMenuViewModel> SubMenus { get; } = new List<SubMenuViewModel>();
    }
}
