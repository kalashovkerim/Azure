using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NimbApp.DbContexts;
using NimbApp.Models;

namespace NimbApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminPanel()
        {
            return View();
        }
        public IActionResult UserAdd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserAdd([Bind("Name,Surname,Login,Password,Number,Address,Password,Position")] User user)
        {
            var context = new AdminDbContext();

            if (ModelState.IsValid)
            {
                context.Add(user);
                context.SaveChanges();
            }

            return View();
        }
    }
}
