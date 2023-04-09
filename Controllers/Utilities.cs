using Microsoft.AspNetCore.Mvc;

namespace MKsEMS.Controllers
{
    public class Utilities : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
