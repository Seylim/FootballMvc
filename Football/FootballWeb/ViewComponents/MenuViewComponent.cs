using Microsoft.AspNetCore.Mvc;

namespace FootballWeb.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
