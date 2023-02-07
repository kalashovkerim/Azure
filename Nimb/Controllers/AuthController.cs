using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nimb.Models;
using NimbRepository.DbContexts;
using NimbRepository.Model.Admin;
using NimbRepository.Repository.Interfaces;
using System.Security.Claims;

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
        public async Task<IActionResult> Login(AuthViewModel authModel)
        {
            var identity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name, authModel.UserName),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim(ClaimTypes.Role, "Seller"),
            new Claim(ClaimTypes.Role,"Storekeeper") },
            CookieAuthenticationDefaults.AuthenticationScheme);

            if (ModelState.IsValid)
            {
                var users = _unitOfwork.User.GetAll();

                var userlog = users.Where(us => us.Login == authModel.UserName && us.Password == authModel.Password).FirstOrDefault();

                if (userlog != null)
                {
                    if (userlog.Position == "Admin")
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                        return RedirectToAction("AdminPanel", "Admin");
                    }
                    else if (userlog.Position == "Storekeeper")
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                        return RedirectToAction("KeeperPanel", "StoreKeeper");
                    }
                    else if (userlog.Position == "Seller")
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                        return RedirectToAction("SellerPanel", "SellerMain");
                    }
                }
            }
            return View();
        }
    }
}
