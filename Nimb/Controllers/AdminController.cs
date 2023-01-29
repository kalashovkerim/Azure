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

namespace NimbApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        private IValidator<User> _validator;
        public AdminController(IValidator<User> validator,IUnitOfWork unitofwork)
        {
            _unitOfwork = unitofwork;
            _validator = validator;
        }

        public IActionResult AdminPanel()
        {
           var users = _unitOfwork!.User!.GetAll();

            return  View(users);
        }
        public IActionResult UserAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserEditConfirm(User user)
        {

            if (ModelState.IsValid)
            {
                _unitOfwork!.User!.Update(user);

                _unitOfwork.Save();

                return RedirectToAction("AdminPanel",_unitOfwork!.User!.GetAll());
            }
            return View("UserEdit");
        }

        public IActionResult Delete(int id)
        {
            var user = _unitOfwork!.User!.FindById(id);
            if (user != null) // check
            {
                _unitOfwork.User.Remove(user);
                _unitOfwork.Save();
            }

            return View("AdminPanel", _unitOfwork.User.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> UserAddAsync(User user)
        {
            var result = await _validator.ValidateAsync(user);

            if (result.IsValid)
            {
                _unitOfwork!.User!.Add(user);

                _unitOfwork.Save();

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
        public IActionResult UserEdit(int id)
        {
            var user = _unitOfwork!.User!.FindById(id);

            return View(user);
        }
    }
}
