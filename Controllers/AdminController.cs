using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NimbApp.DbContexts;
using NimbApp.Models.Admin;

namespace NimbApp.Controllers
{
    public class AdminController : Controller
    {
        private AdminDbContext _context;
        public AdminController(AdminDbContext context)
        {
            _context = context;
        }

        public IActionResult AdminPanel()
        {
            var Users = _context.Users;
            return View(Users);
        }
        public IActionResult UserEdit()
        {
            return View();
        }
        public IActionResult UserAdd()
        {
            return View();
        }
        public IActionResult UserEditConfirm([Bind("Name,Surname,Login,Password,Number,Address,Password,Position")] User user)
        {
            if (ModelState.IsValid)
            {
                string curid = Request.Query["Id"].ToString();
                int id = int.Parse(curid);

                _context.Remove(_context.Users.Find(id));

                _context.Add(user);
                _context.SaveChanges();

                return View("AdminPanel", _context.Users);
            }
            return View("UserEdit");
        }
        public IActionResult Delete()
        {
            string curid = Request.Query["Id"].ToString();
            int id = int.Parse(curid);

            //User? reuser = _context.Users.Where(x => x.Id == id).SingleOrDefault();

            _context.Remove(_context.Users.Find(id));

            _context.SaveChanges();

            var Users = _context.Users;

            return View("AdminPanel",Users);
        }

        [HttpPost]
        public IActionResult UserAdd([Bind("Name,Surname,Login,Password,Number,Address,Password,Position")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                _context.SaveChanges();
            }

            return View();
        }
    }
}
