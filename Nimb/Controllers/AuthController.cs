using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nimb.Models;
using NimbRepository.DbContexts;
using NimbRepository.Model.Admin;
using NimbRepository.Repository.Interfaces;

namespace Nimb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        public AuthController(IUnitOfWork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }
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
                var users = _unitOfwork.User.GetAll();

                var userlog = users.Where(us => us.Login == authModel.UserName && us.Password == authModel.Password).FirstOrDefault();

                if(userlog != null)
                {
                    if (userlog.Position == "Admin")
                    { 
                        return RedirectToAction("AdminPanel", "Admin");
                    }
                    else if (userlog.Position == "Storekeeper")
                    {
                        TempData["Position"] = "keeper";
                        return RedirectToAction("KeeperPanel", "StoreKeeper");
                    }
                    else if(userlog.Position == "Seller")
                    {
                        return RedirectToAction("SellerMain", "Seller");
                    }
                }
            }
            return View();
        }
    }
}
