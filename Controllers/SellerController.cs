using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NimbProjectApp.DbContexts;
using NimbProjectApp.Models;
using NimbProjectApp.Models.Seller;
using NimbProjectApp.Models.Storekeeper;
using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace NimbProjectApp.Controllers
{
    public class SellerController : Controller
    {
        private SellerDbFirstContext _context;
        public SellerController(SellerDbFirstContext context)
        {
            _context= context;
        }
        
        public IActionResult SellerMain()
        {;
            
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

        public IActionResult Goods()
        {
            var suppliers = _context.Suppliers;

            var urlname = Request.Query["Name"].ToString();
            var urlCategory = Request.Query["Category"].ToString();

            if (urlname == null || urlname == "All" || urlname == "")
            {
                var goods = _context.Goods;

                ViewData["Goods"] = goods;
            }
           
            return View(suppliers);
        }
        public IActionResult GetGoods()
        {
          
            var urlname = Request.Query["Name"].ToString();
            var urlCategory = Request.Query["Category"].ToString();

            var currentsupp = _context.Suppliers.Where(supp => supp.Name == urlname).FirstOrDefault();

            var categories = _context.Goods.Where(good => good.SupplierId == currentsupp.Id).Select(good => good.Category).ToArray();
            

            if (urlCategory == "" || urlCategory == "All")
            {
                ViewData["Goods"] = _context.Goods.Where(good => good.SupplierId == currentsupp.Id);
            }
            else
            {
                ViewData["Goods"] = _context.Goods.Where(good => good.SupplierId == currentsupp.Id).Where(good => good.Category == urlCategory).ToArray();
            }
            

            ViewData["Categories"] = categories.Distinct();

            return View("Goods", _context.Suppliers);
        }
        public IActionResult GetCategories() 
        {
       
            var urlname = Request.Query["Name"].ToString();
           
            var currentsupp = _context.Suppliers.Where(supp => supp.Name == urlname).FirstOrDefault();


            if (urlname == null || urlname == "All" || urlname == "")
            {
                ViewData["Goods"] = _context.Goods;
            }
            else
            {
                ViewData["Goods"] = _context.Goods.Where(good => good.SupplierId == currentsupp.Id);
                ViewData["Categories"] = _context.Goods.Where(good => good.SupplierId == currentsupp.Id).Select(good => good.Category).ToArray().Distinct(); 
            }
    
            return View("Goods",_context.Suppliers);
        }
        public IActionResult GoodsSoldFirst()
        {
            
            return View();
        }
        public IActionResult GetUsers()
        {
            var users = _context.Clients;

            ViewBag.Check = true;

            return View("SellerMain", users);
        }

        [HttpPost]
        public IActionResult RegisterUser([Bind("FirstName,LastName,PatronymicName,PhoneNumber,EmailAddress")] Client client)
        {

            client.Date = DateTime.Now;

            _context.Add(client);
            _context.SaveChanges();
            return View("SellerMain");
        }
        [HttpPost]
        public IActionResult RegisterCompany([Bind("Name, PhoneNumber, Address,EmailAddress")] Company client)
        {
            _context.Add(client);
            _context.SaveChanges();
            return View("SellerMain");
        }
    }
}
