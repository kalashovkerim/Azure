using Business.Services.Classes;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Nimb.Models;
using NimbRepository.DbContexts;
using NimbRepository.Model.Admin;

namespace Nimb.Controllers
{
    public class AuthController : Controller
    {
        private IDataProviderService _dataprovider;
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
                //_dataprovider = new NimbDataProvider<User>();
                if(authModel.UserName == "admin" && authModel.Password == "admin")
                {
                    return RedirectToAction("AdminPanel","Admin");
                }
        
            }
            return View();
        }
    }
}
