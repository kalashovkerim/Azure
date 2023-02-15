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
            TempData["Check"] = "Seller main";
            return View(await _unitOfwork.Client.GetAll());
        }
        
        public IActionResult RegisterCompany()
        {
            TempData["Check"] = "Add company";
            return View();
        }
        public IActionResult RegisterUser()
        {
            TempData["Check"] = "Add client";
            return View();
        }

        public async Task<IActionResult> Goods(string Name)
        {

            var suppliers = await _unitOfwork.Supplier.GetAll();


            if (Name == null || Name == "All" || Name == "")
            { 
                var goods = await _unitOfwork!.Good!.GetAll();

                ViewData["Goods"] = goods;
            }

            return View(suppliers);
        }

        public async Task<IActionResult> GetGoodsAsync(string Name, string Category)
        {
            var allgoods = await _unitOfwork.Good.GetAll();
            var supps = await _unitOfwork.Supplier.GetAll();
            var currentsupp = supps.Where(supp => supp.Name == Name).FirstOrDefault();

            //var goods = _context.Goods.Where(good => good.SupplierId == currentsupp.Id).Where(good => good.Category == Category).ToArray();
            var goods = allgoods.Where(good => good.SupplierId == currentsupp!.Id).Where(good => good.Category == Category).ToArray();

            var goods2 = allgoods.Where(good => good.SupplierId == currentsupp!.Id);

            var categories = goods2.Select(good => good.Category).ToArray();


            if (Category == "" || Category == "All")
            {
                ViewData["Goods"] = goods2;
            }
            else
            {
                ViewData["Goods"] = goods;
            }


            ViewData["Categories"] = categories.Distinct();

            return View("Goods", supps);
        }

        public async Task<IActionResult> GetCategoriesAsync(string Name)
        {

            var supps = await _unitOfwork.Supplier.GetAll();
            var currentsupp = supps.Where(supl => supl.Name == Name).FirstOrDefault();

            var goods = await _unitOfwork.Good.GetAll();

            if (Name == null || Name == "All" || Name == "")
            {
                ViewData["Goods"] = _unitOfwork.Good.GetAll()!;
            }
            else
            {
                ViewData["Goods"] = goods.Where(good => good.SupplierId == currentsupp.Id);
                ViewData["Categories"] = goods.Where(good => good.SupplierId == currentsupp.Id).Select(good => good.Category).ToArray().Distinct();
            }

            return View("Goods", _unitOfwork.Supplier.GetAll()!);
        }
        public IActionResult GoodsSoldFirst()
        {

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

            _unitOfwork.Client.Update(client);

            _unitOfwork.Save();

            return View("SellerMain");
        }
        [HttpPost]
        public IActionResult RegisterUser(Client client)
        {
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
        public async Task<IActionResult> SearchGoodAsync(string request)
        {
            var goods = await _unitOfwork.Good.GetAll();
            var suppliers = await _unitOfwork.Supplier.GetAll();

            if (goods.Contains(goods.Where(good => good.Category.ToUpper() == request.ToUpper()).FirstOrDefault()))
            {
                ViewData["Goods"] = goods.Where(good => good.Category.ToUpper() == request.ToUpper());
            }
            else if (suppliers!.Contains(suppliers.Where(supp => supp.Name.ToUpper() == request.ToUpper()).FirstOrDefault()))
            {
                var currentsupp = suppliers.Where(supp => supp.Name.ToUpper() == request.ToUpper()).FirstOrDefault();
                ViewData["Goods"] = goods.Where(good => good.SupplierId == currentsupp!.Id);
            }
            else
            {
                // modal window error not found
            }
            return View("Goods", suppliers);
        }
        [HttpPost]
        public IActionResult RegisterCompany(Company client)
        {
            if (ModelState.IsValid) 
            {
                _unitOfwork.Company.Add(client);
                _unitOfwork.Save();
            }
                
            return View("SellerMain", _unitOfwork.Client.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _unitOfwork.Client.GetAll();
            return Json(new {data = clients});
        }
    }
}
