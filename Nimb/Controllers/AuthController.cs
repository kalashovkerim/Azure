using Microsoft.AspNetCore.Mvc;
using Nimb.Models;
using NimbRepository.DbContexts;
using NimbRepository.Model.Admin;

namespace Nimb.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AuthViewModel authModel)
        {
            if (ModelState.IsValid)
            {
                if (authModel.UserName == "admin" && authModel.Password == "admin")
                {
                    return RedirectToAction("AdminPanel", "Admin");
                }
                else if (authModel.UserName == "seller" && authModel.Password == "seller")
                {
                    return RedirectToAction("SellerMain", "Seller");
                }
                else if(authModel.UserName == "keeper" && authModel.Password == "keeper")
                {
                    TempData["Position"] = "keeper";
                    return RedirectToAction("KeeperPanel", "StoreKeeper");
                }
            }
            return View();
        }
    }
}
