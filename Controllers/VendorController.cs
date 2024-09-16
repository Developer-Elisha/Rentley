using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rentley.Db_Context;
using Rentley.Models;

namespace Rentley.Controllers
{
    public class VendorController : Controller
    {
        SqlContext sc;
        IWebHostEnvironment env;

        public VendorController(SqlContext context, IWebHostEnvironment env)
        {
            this.sc = context;
            this.env = env;
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("Users");
            var id = HttpContext.Session.GetInt32("Userid");
            if (user != null || id != null)
            {
                ViewBag.User = id;
                ViewBag.login = user;

                var totalProds = sc.tblProducts.Where(i => i.User_id == id).Count();
                var RentRequest = sc.tblRents.Where(r => r.status == "Pending").Count();
                var RentCompleted = sc.tblRents.Where(i => i.status == "Completed").Count();

                ViewBag.TotalProds = totalProds;
                ViewBag.TotalRentRequest = RentRequest;
                ViewBag.TotalRentCompleted = RentCompleted;

                return View();
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }

        }

        public IActionResult my_products()
        {

            var user = HttpContext.Session.GetString("Users");
            var id = HttpContext.Session.GetInt32("Userid");
            if (user != null || id != null)
            {
                ViewBag.User = id; 
                ViewBag.login = user;

                var myprods = sc.tblProducts.Include(c => c.Category).Where(x => x.User_id ==  id).ToList();

                return View(myprods);
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }
        }

        public IActionResult add_prod()
        {
            var user = HttpContext.Session.GetString("Users");
            var id = HttpContext.Session.GetInt32("Userid");
            if (user != null || id != null)
            {
                ViewBag.User = id;
                ViewBag.login = user;
                var categories = sc.tblCategory.ToList();
                ViewBag.Category = categories;
                return View();
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }
            
        }



        [HttpPost]
        public IActionResult add_prod(Prod_Image prod)
        {
            String filename = "";
            String uploadFolder = Path.Combine(env.WebRootPath, "MyImages");
            filename = Guid.NewGuid().ToString() + "_" + prod.ProductPhoto.FileName;
            String mergevalue = Path.Combine(uploadFolder, filename);
            prod.ProductPhoto.CopyTo(new FileStream(mergevalue, FileMode.Create));

            products pm = new products()
            {
                Prod_name = prod.Prod_name,
                Description = prod.Description,
                User_id = prod.User_id,
                Category_Id = prod.Category_Id,
                PricePerDay = prod.PricePerDay,
                Availability = prod.Availability,
                Product_Image = filename
            };

            sc.tblProducts.Add(pm);
            sc.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("my_products");
        }

        public IActionResult renter_req()
        {
            var user = HttpContext.Session.GetString("Users");
            var id = HttpContext.Session.GetInt32("Userid");
            if (user != null || id != null)
            {
                ViewBag.User = id;
                ViewBag.login = user;
                var req = sc.tblRents.Include(p => p.User_Temp ).Where( i => i.OwnerId == id && i.status == "Pending").Include(p => p.Products).ToList();
                return View(req);
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }
        }

        public IActionResult Update_Prods(int id)
        {

            var user = HttpContext.Session.GetString("Users");
            var usid = HttpContext.Session.GetInt32("Userid");
            if (user != null || usid != null)
            {
                var prod = sc.tblProducts.Find(id);
                if (prod == null)
                {
                    return RedirectToAction("error404");
                }
                var categories = sc.tblCategory.ToList();
                ViewBag.Category = categories;
                ViewBag.User = usid;
                ViewBag.login = user;
                return View(prod);
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }
        }

        [HttpPost]
        public IActionResult Update_Prods(products prods)
        {
            if (prods == null || prods.Category_Id == 0 || string.IsNullOrEmpty(prods.Prod_name) || string.IsNullOrEmpty(prods.Description) || string.IsNullOrEmpty(prods.Availability) || prods.PricePerDay == 0)
            {
                ModelState.AddModelError(string.Empty, "Invalid data.");
                return View(prods);
            }

            var Prod = sc.tblProducts.Find(prods.Prod_id);
            if (Prod == null)
            {
                return RedirectToAction("error404");
            }

            Prod.Prod_name = prods.Prod_name;
            Prod.Description = prods.Description;
            Prod.Availability = prods.Availability;
            Prod.PricePerDay = prods.PricePerDay;
            Prod.Category_Id = prods.Category_Id;

            sc.SaveChanges();
            return RedirectToAction("my_products");
        }

        public IActionResult Delete_Prod(int id)
        {
            var prod = sc.tblProducts.Find(id);
            if (prod == null)
            {
                return RedirectToAction("error404");
            }

            sc.tblProducts.Remove(prod);
            sc.SaveChanges();
            return RedirectToAction("my_products");
        }


        [HttpPost]
        public IActionResult Approve(int id)
        {
            var request = sc.tblRents.Find(id);
            if (request != null)
            {
                request.status = "Approved";
                sc.SaveChanges();
            }
            return RedirectToAction("renter_req");
        }

    }
}
