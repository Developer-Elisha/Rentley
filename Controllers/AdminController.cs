using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentley.Db_Context;
using Rentley.Models;

namespace Rentley.Controllers
{
    public class AdminController : Controller
    {
        SqlContext sc;
        IWebHostEnvironment env;

        public AdminController(SqlContext context, IWebHostEnvironment env)
        {
            this.sc = context;
            this.env = env;
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("Users");
            if (user != null)
            {
                ViewBag.login = user;

                var totalProds = sc.tblProducts.Count();
                var totalCategories = sc.tblCategory.Count();
                var totalVendors = sc.tblUser_temp.Where(i => i.Role == 2).Count();
                var totalUsers = sc.tblUser_temp.Where(i => i.Role == 1).Count();

                ViewBag.TotalProds = totalProds;
                ViewBag.TotalCategories = totalCategories;
                ViewBag.TotalVendors = totalVendors;
                ViewBag.TotalUsers = totalUsers;

                return View();
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }
        }

        public IActionResult Rented_prod()
        {
            var user = HttpContext.Session.GetString("Users");
            if (user != null)
            {
                ViewBag.login = user;

                var rented = sc.tblRents.Include(r => r.User_Temp).Include(r => r.Products).Include(r => r.Owner).Where(s => s.status == "Rented");
                return View(rented);
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }
        }

        public IActionResult user()
        {
            var user = HttpContext.Session.GetString("Users");
            if (user != null)
            {
                ViewBag.login = user;

                var renter = sc.tblUser_temp.Where(s => s.Role == 1);
                return View(renter);
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }
        }

        public IActionResult vendor()
        {
            var user = HttpContext.Session.GetString("Users");
            if (user != null)
            {
                ViewBag.login = user;

                var vendor = sc.tblUser_temp.Where(s => s.Role == 2);
                return View(vendor);
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }
        }

        public IActionResult approve_user()
        {
            return View();
        }

        public IActionResult approve_vendor()
        {
            return View();
        }

        public IActionResult add_category()
        {
            var user = HttpContext.Session.GetString("Users");
            if (user != null)
            {
                ViewBag.login = user;
                return View();
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }
        }

        [HttpPost]
        public IActionResult add_category(category cate)
        {
            if (cate == null || string.IsNullOrEmpty(cate.Category_Name))
            {
                ModelState.AddModelError(string.Empty, "Please Fill Category Name");
                return View(cate);
            }
            sc.Add(cate);
            sc.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("add_category");
        }
    }
}
