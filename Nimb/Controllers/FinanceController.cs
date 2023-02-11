using Microsoft.AspNetCore.Mvc;

namespace Nimb.Controllers
{
    public class FinanceController : Controller
    {
        public IActionResult Statistics()
        {
            return View();
        }
    }
}
 