using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using NimbRepository.Model.Seller;
using NimbRepository.Model.Storekeeper;
using NimbRepository.Model.Admin;
using NimbRepository.Repository.Interfaces;
using System.Globalization;
using NimbRepository.Repository.Classes;
using Microsoft.AspNetCore.Authorization;
using NuGet.Packaging.Signing;
using FluentValidation;
using Nimb.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace NimbProjectApp.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SellerController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        private IValidator<Client> _validator;

        public SellerController(IUnitOfWork unitofwork,IValidator<Client> validator)
        {
            _unitOfwork = unitofwork;
            _validator = validator;
        }

        public async Task<IActionResult> SellerMainAsync()
        {
            TempData["Check"] = "Seller";
            return View(await _unitOfwork.Client.GetAll());
        }
        
        public IActionResult RegisterCompany()
        {
            TempData["Check"] = "Seller";
            return View();
        }
        public IActionResult RegisterUser()
        {
            TempData["Check"] = "Seller";
            return View();
        }

        public IActionResult GoodsSoldFirst()
        {
            
            return View();
        }
        public async Task<IActionResult> Goods()
        {
            TempData["Check"] = "Seller";
            ViewData["Goods"] = await _unitOfwork.Good.GetAll();
            var suppliers = await  _unitOfwork.Supplier.GetAll();
            return View(suppliers);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var client = _unitOfwork.Client.GetFirstOrDefault(u => u.Id == id);
            if (client == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }


            _unitOfwork.Client.Remove(client);
            _unitOfwork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        public IActionResult EditUser(int id)
        {
            TempData["Check"] = "Seller";
            if (id == null || id == 0)
            {
                return View("SellerMain");
            }
            else
            {
                var client = _unitOfwork.Client.GetFirstOrDefault(cl => cl.Id == id);
                return View(client);
            }
        }
        [HttpPost]
        public IActionResult EditUser(Client client)
        {
            TempData["Check"] = "Seller";
            _unitOfwork.Client.Update(client);

            _unitOfwork.Save();

            return RedirectToAction("SellerMain", "Seller");
        }
        [HttpPost]
        public IActionResult RegisterUser(Client client)
        {
            TempData["Check"] = "Seller";
            var result = _validator.Validate(client);
            if (result.IsValid)
            {
                _unitOfwork.Client.Add(client);
                _unitOfwork.Save();
                return RedirectToAction("SellerMain", _unitOfwork.Client.GetAll());
            }
            else 
            {
                return View(client);
            }
            
        }
       
        [HttpPost]
        public async Task<IActionResult> RegisterCompany(Company client)
        {
            if (ModelState.IsValid) 
            {
                _unitOfwork.Company.Add(client);
                _unitOfwork.Save();
            }
                
            return RedirectToAction("SellerMain", "Seller", await _unitOfwork.Client.GetAll());
        }
        public IActionResult PlaceAnOrder()
        {
            TempData["Check"] = "Seller";
            return View();
        }
        public IActionResult SWin()
        {
            TempData["Check"] = "Seller";
            return View();
        }
        public async Task<IActionResult> Check(int id)
        {
            TempData["Check"] = "Seller";
            var goods = await _unitOfwork.Good.GetAll();
            ViewData["Goods"] = goods;
            var client = _unitOfwork.Client.GetFirstOrDefault(cl => cl.Id == id);
            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            TempData["Check"] = "Seller";
            var clients = await _unitOfwork.Client.GetAll();
            return Json(new {data = clients});
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGoods()
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };
            
            TempData["Check"] = "Seller";
            var goods = await _unitOfwork.Good.GetAll();
            var suppliers = await _unitOfwork.Supplier.GetAll();

            DataViewModel DW = new();


            


            return Json(new { data = goods },options);
        }
    }
}
