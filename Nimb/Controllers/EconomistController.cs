using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NimbRepository.Repository.Classes;
using NimbRepository.Repository.Interfaces;

namespace Nimb.Controllers
{
    [Authorize(Roles = "Economist")]
    public class EconomistController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;

        public EconomistController(IUnitOfWork unitOfWork)
        {
            _unitOfwork= unitOfWork;
        }

        public IActionResult Statistics()
        {
            return View();
        }

        /*public IActionResult Oborot()
        {
            2022 - 12 - 06 to  2022 - 12 - 10
            return View();
        }*/

        /*public IActionResult ChistayaPribl()
        {
            return View();
        }*/

        /*public IActionResult Zatrati()
        {
            return View();
        }*/
    }
}
