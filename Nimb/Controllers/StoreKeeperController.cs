﻿using FluentValidation;
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
        public IActionResult AddProvider()
        {
            TempData["Check"] = "keeper";

            return View();
        }
        
        public async Task<IActionResult> AddProduct()
        {
            TempData["Check"] = "keeper";
            var suppliers = new List<Supplier>(await _unitOfwork.Supplier.GetAll());

            ViewData["Suppliers"] = suppliers.Distinct();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel product)
        {
            TempData["Check"] = "keeper";
            var result = await _goodvalidator.ValidateAsync(product.Good);

            var suppliers = new List<Supplier>(await _unitOfwork.Supplier.GetAll());

            ViewData["Suppliers"] = suppliers.Distinct();

            if (result.IsValid)
            {
                if (product.SuppId != 0)
                {
                    product.Good.SupplierId = product.SuppId;

                    _unitOfwork.Good.Add(product.Good);
                    _unitOfwork.Save();

                    return View("AddProduct",product);
                }
            }
            return View("AddProduct", product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProviderAsync(Supplier supp)
        {
            TempData["Check"] = "keeper";
            var result = await _suppvalidator.ValidateAsync(supp);

            if (result.IsValid)
            {
                var check = await _unitOfwork.Supplier.GetAll()!;

                var existingSuppliers = check.Select(s => s.Name.ToUpperInvariant());

                var supplierName = supp.Name.ToUpperInvariant();

                if (existingSuppliers.Contains(supplierName))
                {
                    Console.WriteLine("Supplier already taken");
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
