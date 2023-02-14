using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using NimbRepository.DbContexts;
using NimbRepository.Model;
using NimbRepository.Model.Admin;
using NimbRepository.Repository.Interfaces;
using NimbRepository.Repository.Classes;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.AspNetCore;
using Business.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using NimbRepository.Model.Seller;

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
            TempData["Check"] = "Yes";
            return  View();
        }
        public IActionResult UserAdd()
        {
            return View();
        }
        public async Task<IActionResult> UserEdit(int id)
        {
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

            if (ModelState.IsValid)
            {
                _unitOfwork.User.Update(user);

                _unitOfwork.Save();

            }
            return View("AdminPanel");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _unitOfwork.User.FindById(id);
            if (user != null) // check
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
            var result = await _validator.ValidateAsync(user);

            if (result.IsValid)
            {
                
                var login = GenerateUserLogin.FromName(user.FirstName);

                var password = GenerateUserPassword.Generate();
                var hashedPassword = new HashData(password).DoHash();

                var users = await _unitOfwork.User.GetAll();

                while (users!.Contains(users!.Where(us => us.Login == login).FirstOrDefault()))
                {
                    login = GenerateUserLogin.FromName(user.FirstName);
                }
                user.Login = login;
                user.Password = hashedPassword;

                _unitOfwork.User.Add(user);

                _unitOfwork.Save();


                GmailSender gs = new GmailSender(user.EmailAddress, "test1nimb@gmail.com", "kysmphuibjqpvrfz", "Login/Password", $"Login:{login}\nPassword:{password}\nDon't say your login and password to others!");
                gs.SendAsyncEmail();

                return RedirectToAction("AdminPanel", _unitOfwork!.User!.GetAll());
            }
            else
            {
                result.AddToModelState(ModelState, null);
                var context = new NimbDbContext();

                foreach (var failure in result.Errors)
                {
                    Console.WriteLine($"Property: {failure.PropertyName} Error Code: {failure.ErrorCode}");
                }
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
