using Microsoft.AspNetCore.Mvc;
using NimbApp.Models;
namespace NimbApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("UserName,Password")] AuthViewModel authModel)
        {
            if (ModelState.IsValid)
            {
                if(authModel.UserName == "admin" && authModel.Password == "admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            return View();
        }
    }
}
