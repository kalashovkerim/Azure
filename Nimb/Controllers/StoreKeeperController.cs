using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nimb.Models;
using NimbRepository.Model;
using NimbRepository.Model.Admin;
using NimbRepository.Model.Storekeeper;
using NimbRepository.Repository.Interfaces;

namespace Nimb.Controllers
{
    [Authorize(Roles = "Storekeeper")]
    public class StoreKeeperController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        private IValidator<Good> _goodvalidator;
        private IValidator<Supplier> _suppvalidator;

        public StoreKeeperController(IValidator<Good> goodvalidator, IValidator<Supplier> suppvalidator, IUnitOfWork unitofwork)
        {
            _unitOfwork = unitofwork;
            _goodvalidator = goodvalidator;
            _suppvalidator = suppvalidator;
        }
        public IActionResult KeeperPanel()
        {
            return View();
        }
        public IActionResult AddProvider()
        {
            return View();
        }
        public async Task<IActionResult> AddProduct()
        {
            var suppliers = new List<Supplier>();
            var supps = await _unitOfwork.Supplier.GetAll();
            foreach (var supplier in supps)
            {
                suppliers.Add(supplier);
            }
            ViewData["Suppliers"] = suppliers.Distinct();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProductAsync(ProductViewModel product)
        {
            var result = await _goodvalidator.ValidateAsync(product.good); 

            if (result.IsValid)
            {
                if (product.SuppId != 0)
                {
                    product.good.SupplierId = product.SuppId;

                    _unitOfwork.Good.Add(product.good);
                    _unitOfwork.Save();
                }
            }
            return View("KeeperPanel");
        }
        [HttpPost]
        public async Task<IActionResult> AddProviderAsync(Supplier supp)
        {
            var result = await _suppvalidator.ValidateAsync(supp);

            if (result.IsValid)
            {
                var check = await _unitOfwork.Supplier.GetAll()!;

                if (check!.Contains(check!.Where(supl => supl.Name.ToUpper() == supp.Name.ToUpper()).FirstOrDefault()))
                {
                    Console.WriteLine("Supllier already taken");
                    // modal window error
                }
                else
                {
                    _unitOfwork.Supplier.Add(supp);
                    _unitOfwork.Save();
                }
            }
            return View();
        }
    }
}
