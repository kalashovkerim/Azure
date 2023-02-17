using Microsoft.AspNetCore.Mvc;

namespace Nimb.Controllers
{
    public class ErrorPagesController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
