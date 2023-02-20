using Microsoft.AspNetCore.Mvc;
using NimbRepository.Model.Seller;
using NimbRepository.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
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

        public async Task<IActionResult> SellerMain()
        {
            TempData["Check"] = "Seller";
            return View();
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
            return View();
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
                return View();
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
        public async Task<IActionResult> RegisterUser(Client client)
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
                return View();
            }
            
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

            return Json(new { data = goods },options);
        }
    }
}
