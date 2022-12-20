using BrainUp.Data;
using BrainUp.Models;
using BrainUp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BrainUp.Components
{
    public class CreateRankViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            var model = new RankViewModel();
            model.CourceId = id;

            return View(model);
        }
    }
}
