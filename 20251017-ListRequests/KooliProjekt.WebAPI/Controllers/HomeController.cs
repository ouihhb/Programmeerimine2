using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Data;

namespace KooliProjekt.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
