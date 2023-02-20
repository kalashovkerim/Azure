using Microsoft.AspNetCore.Mvc;
using NimbRepository.Model.Admin;
using NimbRepository.Repository.Interfaces;
using FluentValidation;
using Business.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace NimbApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        private IValidator<User> _validator;
        public AdminController(IValidator<User> validator,IUnitOfWork unitofwork)
        {
            _unitOfwork = unitofwork;
            _validator = validator;
        }

        public async Task<IActionResult> AdminPanel()
        {
            TempData["Check"] = "Admin";
            return  View();
        }
        public IActionResult UserAdd()
        {
            TempData["Check"] = "Admin";
            return View();
        }
        public async Task<IActionResult> UserEdit(int id)
        {
            TempData["Check"] = "Admin";
            if (id == null || id == 0)
            {
                return View("AdminPanel");
            }
            else
            {
                var user = _unitOfwork.User.GetFirstOrDefault(us => us.Id== id);
                return View(user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(User user)
        {
            TempData["Check"] = "Admin"; ;
            var result = await _validator.ValidateAsync(user);

            if (result.IsValid)
            {
                _unitOfwork.User.Update(user);

                _unitOfwork.Save();

                return View("AdminPanel");
            }
            else
            {
                return View(user);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _unitOfwork.User.FindById(id);
            if (user != null) 
            {
                _unitOfwork.User.Remove(user);
                _unitOfwork.Save();
            }
            else
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            return Json(new { success = true, message = "Delete Successful" });
        }

        [HttpPost]
        public async Task<IActionResult> UserAdd(User user)
        {
            TempData["Check"] = "Admin";
            var result = await _validator.ValidateAsync(user);

            if (result.IsValid)
            {
                
                var login = GenerateUserLogin.FromName(user.FirstName);
                var password = GenerateUserPassword.Generate();

                var hashedPassword = new HashData(password.ToString()).DoHash();
              
                var users = await _unitOfwork.User.GetAll();

                if (users != null)
                {
                    var existingUsers = users.Select(s => s.Login);

                    while (existingUsers.Contains(login.ToString()))
                    {
                        login = GenerateUserLogin.FromName(user.FirstName);
                    }
                }

                user.Login = login.ToString();
                user.Password = hashedPassword;

                _unitOfwork.User.Add(user);

                _unitOfwork.Save();


                GmailSender gs = new GmailSender(user.EmailAddress, "test1nimb@gmail.com", "kysmphuibjqpvrfz", "Login/Password", $"Login:{login}\nPassword:{password}\nDon't say your login and password to others!");
                gs.SendAsyncEmail();

                return RedirectToAction("AdminPanel", users);
            }
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var users = await _unitOfwork.User.GetAll();
            return Json(new { data = users });
        }
    }
}
