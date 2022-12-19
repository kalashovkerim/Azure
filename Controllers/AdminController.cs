using Microsoft.AspNetCore.Mvc;

namespace NimbApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
