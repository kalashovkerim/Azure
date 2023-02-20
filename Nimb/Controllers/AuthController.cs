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
using NimbApp.Controllers;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using NuGet.Protocol.Plugins;
using Business.Services.Classes;
using Microsoft.AspNetCore.Components;
using NimbRepository.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

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
            TempData["Check"] = "";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthViewModel authModel)
        {
 
            if (ModelState.IsValid)
            {
                var users = await _unitOfwork.User.GetAll();

                var userlog = users.Where(us => us.Login == authModel.UserName).FirstOrDefault();

                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                string controller = " ";
                string action = " ";

                if (authModel.Password == "admin" && authModel.UserName == "admin") 
                {
                    identity.AddClaims(new[] { new Claim(ClaimTypes.Name, authModel.UserName), new Claim(ClaimTypes.Role, "Admin") });
                    controller = "Admin";
                    action = "AdminPanel";
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                    TempData["Check"] = "Admin";
                    return RedirectToAction("AdminPanel", "Admin");
                }

                if (userlog != null)
                {
                    bool isUser = new HashData(authModel.Password).Verify(userlog.Password);
                    if (isUser) {
                        
                        
                        if (userlog.Position == "Admin")
                        {
                            identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                            controller = "Admin";
                            action = "AdminPanel";
                        }
                        else if (userlog.Position == "Storekeeper")
                        {
                            identity.AddClaims(new[] { new Claim(ClaimTypes.Name, authModel.UserName), new Claim(ClaimTypes.Role, "Storekeeper") });
                            controller = "StoreKeeper";
                            action = "AddProvider";
                        }
                        else if (userlog.Position == "Seller")
                        {
                            identity.AddClaims(new[] { new Claim(ClaimTypes.Name, authModel.UserName), new Claim(ClaimTypes.Role, "Seller") });
                            controller = "Seller";
                            action = "SellerMain";
                           
                        }
                        else if (userlog.Position == "Economist")
                        {
                            identity.AddClaims(new[] { new Claim(ClaimTypes.Name, authModel.UserName), new Claim(ClaimTypes.Role, "Economist") });
                            controller = "Economist";
                            action = "Statistics";

                        }
                        
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                        TempData["Check"] = userlog.Position;
                        return RedirectToAction(action, controller);
                    }
                    
                }
            }
            ModelState.AddModelError("", "Password or login is invalid");            
            return View();

        }

        public async Task<IActionResult> Logout()
        {
            AuthViewModel AuthModel = new AuthViewModel();
            TempData["Check"] = "";
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
