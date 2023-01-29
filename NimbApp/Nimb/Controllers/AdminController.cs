using Business.Services.Classes;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using NimbRepository.DbContexts;
using NimbRepository.Model;
using NimbRepository.Model.Admin;

namespace NimbApp.Controllers
{
    public class AdminController : Controller
    {
        private IDataProviderService _dataprovider;
        public AdminController()
        {
            
        }

        public IActionResult AdminPanel()
        {
            _dataprovider = new NimbDataProvider<User>();

            return View(_dataprovider.GetAllData());
        }
        public IActionResult UserAdd()
        {
            return View();
        }
        public IActionResult UserEditConfirm(User user)
        {
            // id ne zapisivatsa v usera

            if (ModelState.IsValid)
            {    
                _dataprovider = new NimbDataProvider<User>();

                _dataprovider.Update(user);

                _dataprovider.Save();

                return View("AdminPanel", _dataprovider.GetAllData());
            }
            return View("UserEdit");
        }

        public IActionResult Delete(int id)
        {
            _dataprovider = new NimbDataProvider<User>();

            _dataprovider.Delete(id);

            _dataprovider.Save();

            return View("AdminPanel", _dataprovider.GetAllData());
        }

        [HttpPost]
        public IActionResult UserAdd([Bind("FirstName, LastName, PatronymicName, Login, Password, Number, Address, EmailAddress, Position")] User user)
        {
            if (ModelState.IsValid)
            {
                _dataprovider = new NimbDataProvider<User>();

                _dataprovider.SetData(user);

                _dataprovider.Save();
            }

            return RedirectToAction("AdminPanel", _dataprovider.GetAllData());
        }
        public IActionResult UserEdit(int id)
        {
            _dataprovider = new NimbDataProvider<User>();

            var user = _dataprovider.FindById(id);

            return View(user);
        }
    }
}
