using Microsoft.AspNetCore.Mvc;
using NimbApp.Models.Authorization;
using System;

namespace NimbApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
           return View();
        }
        
        public IActionResult Admin()
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
                    return RedirectToAction("AdminPanel", "Admin");
                }
                if (authModel.UserName == "seller" && authModel.Password == "seller")
                {
                    return RedirectToAction("SellerMain", "Seller");
                }
                return View();
            }
            return View();
        }
    }
}
