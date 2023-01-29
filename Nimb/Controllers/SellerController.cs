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

namespace NimbProjectApp.Controllers
{
    public class SellerController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;

        public SellerController(IUnitOfWork unitofwork)
        {
            _unitOfwork = unitofwork;
        }

        public IActionResult SellerMain()
        {
            return View();
        }
        
        public IActionResult RegisterCompany()
        {
            return View();
        }
        public IActionResult RegisterUser()
        {
            return View();
        }

        public IActionResult Goods(string Name)
        {

            var suppliers = _unitOfwork!.Supplier!.GetAll();


            if (Name == null || Name == "All" || Name == "")
            { 
                var goods = _unitOfwork!.Good!.GetAll();

                ViewData["Goods"] = goods;
            }

            return View(suppliers);
        }

        public IActionResult GetGoods(string Name, string Category)
        {


            var currentsupp = _unitOfwork!.Supplier!.GetAll()!.Where(supp => supp.Name == Name).FirstOrDefault();

            //var goods = _context.Goods.Where(good => good.SupplierId == currentsupp.Id).Where(good => good.Category == Category).ToArray();
            var goods = _unitOfwork!.Good!.GetAll()!.Where(good => good.SupplierId == currentsupp!.Id).Where(good => good.Category == Category).ToArray();

            var goods2 = _unitOfwork!.Good!.GetAll()!.Where(good => good.SupplierId == currentsupp!.Id);

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

            return View("Goods", _unitOfwork!.Supplier!.GetAll()!);
        }

        public IActionResult GetCategories(string Name)
        {

            var currentsupp = _unitOfwork!.Supplier!.GetAll()!.Where(supl => supl.Name == Name).FirstOrDefault();


            if (Name == null || Name == "All" || Name == "")
            {
                ViewData["Goods"] = _unitOfwork!.Good!.GetAll();
            }
            else
            {
                ViewData["Goods"] = _unitOfwork!.Good!.GetAll()!.Where(good => good.SupplierId == currentsupp.Id);
                ViewData["Categories"] = _unitOfwork!.Good!.GetAll()!.Where(good => good.SupplierId == currentsupp.Id).Select(good => good.Category).ToArray().Distinct();
            }

            return View("Goods", _unitOfwork!.Supplier!.GetAll()!);
        }
        public IActionResult GoodsSoldFirst()
        {

            return View();
        }
        public IActionResult GetUsers()
        {
            var clients = _unitOfwork.Client.GetAll();

            ViewBag.Check = true;
            return View("SellerMain", clients);
        }


        [HttpPost]
        public IActionResult RegisterUser(Client client)
        {
            if (ModelState.IsValid)
            {
                _unitOfwork.Client.Add(client);
                _unitOfwork.Save();    
            }
            return View("SellerMain");
        }
        public IActionResult SearchGood(string request)
        {
            var goods = _unitOfwork!.Good!.GetAll();
            var suppliers = _unitOfwork!.Supplier!.GetAll();

            if (goods!.Contains(goods!.Where(good => good.Category.ToUpper() == request.ToUpper()).FirstOrDefault()))
            {
                ViewData["Goods"] = goods!.Where(good => good.Category.ToUpper() == request.ToUpper());
            }
            else if (suppliers!.Contains(suppliers!.Where(supp => supp.Name.ToUpper() == request.ToUpper()).FirstOrDefault()))
            {
                var currentsupp = _unitOfwork!.Supplier!.GetAll()!.Where(supp => supp.Name.ToUpper() == request.ToUpper()).FirstOrDefault();
                ViewData["Goods"] = goods!.Where(good => good.SupplierId == currentsupp!.Id);
            }
            else
            {
                // modal window error not found
            }
            return View("Goods",_unitOfwork!.Supplier!.GetAll()!);
        }
        [HttpPost]
        public IActionResult RegisterCompany(Company client)
        {
            if (ModelState.IsValid) 
            {
                _unitOfwork.Company.Add(client);
                _unitOfwork.Save();
            }
                
            return View("SellerMain");
        }
    }
}
