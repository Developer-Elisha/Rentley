using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rentley.Db_Context;
using Rentley.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rentley.Controllers
{
    public class RenterController : Controller
    {
        SqlContext sc;
        IWebHostEnvironment env;

        public RenterController(SqlContext context , IWebHostEnvironment env)
        {
            this.sc = context;
            this.env = env;
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("Users");

            if (user != null)
            {
                ViewBag.Login = user;
            }

            return View();
        }


        public IActionResult Listing(string search)
		{
            var user = HttpContext.Session.GetString("Users");

            if (user != null)
            {
                ViewBag.Login = user;
            }
            var products = sc.tblProducts.Include(p => p.User_Temp).Include(c => c.Category).Where(x => x.Prod_name.ToLower().Contains(search.ToLower())).ToList();
            return View(products);
        }


		public IActionResult product(int E2N20R31D22A28A31)
        {
            var user = HttpContext.Session.GetString("Users");
            var id = HttpContext.Session.GetInt32("Userid");
            if (user != null || id != null)
            {
                ViewBag.User = id;
                ViewBag.Login = user;
            }
            var prodid = sc.tblProducts.Include(p => p.User_Temp).Where(i => i.Prod_id == E2N20R31D22A28A31).FirstOrDefault();
            if (prodid == null)
            {
                return RedirectToAction("Listing", "Renter");
            }
            else
            {
                return View(prodid);
            }
        }

        [HttpPost]
        public IActionResult Rent_Request(Rents req)
        {
            var request = new Rents
            {
                User_id = req.User_id,
                Prod_id = req.Prod_id,
                FromDate = req.FromDate,
                FromTime = req.FromTime,
                UntilDate = req.UntilDate,
                UntilTime = req.UntilTime,
                OwnerId =  req.OwnerId, 
            };
            sc.tblRents.Add(request);
            sc.SaveChanges();
            return RedirectToAction("My_Rents");
        }

        public IActionResult My_Rents()
        {
            var user = HttpContext.Session.GetString("Users");
            var id = HttpContext.Session.GetInt32("Userid");

            if (user != null && id != null)
            {
                ViewBag.User = id;
                ViewBag.login = user;

                var rentRequests = sc.tblRents
                    .Include(r => r.User_Temp)
                    .Include(r => r.Products)
                    .Include(r => r.Owner)
                    .Where(r => r.User_id == id && r.status != "Completed")
                    .ToList();

                return View(rentRequests);
            }
            else
            {
                return RedirectToAction("login", "Renter");
            }
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User_temp us)
        {
            var user = sc.tblUser_temp.Where(a => a.Email == us.Email && a.Password == us.Password).FirstOrDefault();
            if (user != null)
            {
                var id = user.User_id;
                var name = user.Name;
                HttpContext.Session.SetInt32("Userid", id);
                HttpContext.Session.SetString("Users", name);
                ViewBag.User = id;
                ViewBag.login = name;

                //if (user.status == "Not-Approved")
                //{
                //    return RedirectToAction("Verification");
                //}
                //else if (user.status == "Approved")
                //{
                    if (user.Role == 1)
                    {
                        return RedirectToAction("Index", "Renter");
                    }
                    else if (user.Role == 2)
                    {
                        return RedirectToAction("Index", "Vendor");
                    }
                    else if (user.Role == 3)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        // Default role case, if none of the above match
                        return RedirectToAction("Index");
                    }
                }
                //else
                //{
                //    // Handle unexpected status values
                //    return RedirectToAction("Login"); // or some other action to handle this
                //}
           // }
            else
            {
                ViewBag.Error = "Invalid email or password";
                return View();
            }
        }



        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult register(User_temp us)
        {
            if (us == null || string.IsNullOrEmpty(us.Name) || string.IsNullOrEmpty(us.LastName) || string.IsNullOrEmpty(us.Email) || string.IsNullOrEmpty(us.Password) || us.Age == 0 || us.PhoneNumber == 0 || string.IsNullOrEmpty(us.Country) || us.Postal_Code == 0 || string.IsNullOrEmpty(us.City) || string.IsNullOrEmpty(us.Address) || us.Role == 0)
            {
                ModelState.AddModelError(string.Empty, "");
                return View(us);
            }
            var email = sc.tblUser_temp.FirstOrDefault(e => e.Email == us.Email);
            if (email == null)
            {
                if (us.Age >= 18)
                {
                    sc.tblUser_temp.Add(us);
                    sc.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "You must be at least 18 years old.");
                    return View(us);
                }
            }
            else
            {
                ViewBag.Acc = "You already have an account";
                return View("Register", us);
            }
        }

        public IActionResult Payment(int id)
        {
            var user = HttpContext.Session.GetString("Users");
            var usid = HttpContext.Session.GetInt32("Userid");

            if (user != null && usid != null)
            {
                ViewBag.User = usid;
                ViewBag.login = user;

                var pay = sc.tblRents
                    .Include(r => r.User_Temp)
                    .Include(r => r.Products)
                    .Include(r => r.Owner)
                    .FirstOrDefault(i => i.User_id == usid && i.RentRequestId == id);

                if (pay != null)
                {
                    return View(pay);
                }
                else
                {
                    TempData["ErrorMessage"] = "Record not found.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login", "Renter");
            }
        }

        [HttpPost]
        public IActionResult SubmitPayment(Rents r, int id)
        {
            
            var pay = sc.tblRents.Find(id);
            if (pay == null || string.IsNullOrEmpty(r.AccountName) || string.IsNullOrEmpty(r.AccountNo) || string.IsNullOrEmpty(r.PaymentMethod))
            {
                ModelState.AddModelError(string.Empty, "Please fill all the Fields");
                return RedirectToAction("Payment", new { id });
            }

            pay.Payment_Status = r.Payment_Status;
            pay.status = r.status;
            pay.AccountName = r.AccountName;
            pay.AccountNo = r.AccountNo;
            pay.PaymentMethod = r.PaymentMethod;
            pay.Rent_Days = r.Rent_Days;
            pay.Fee = r.Fee;
            pay.Sub_Total = r.Sub_Total;
            pay.Total = r.Total;

            sc.SaveChanges();

            return RedirectToAction("My_Rents");
        }


        [HttpPost]
        public IActionResult Return(Rents r, int id)
        {
            var request = sc.tblRents.Find(id);
            if (request != null)
            {
                request.status = "Completed";
                sc.SaveChanges();
            }
            return RedirectToAction("My_Rents");
        }


        public IActionResult Verification()
        {
            //var user = HttpContext.Session.GetString("Users");
            //var id = HttpContext.Session.GetInt32("Userid");
            //if (user != null || id != null)
            //{
            //    ViewBag.User = id;
            //    ViewBag.login = user;

                return View();
            //}
            //else
            //{
            //    return RedirectToAction("login", "Renter");
            //}
        }

        [HttpPost]
        public IActionResult Verification(varify_image v)
        {
            if (ModelState.IsValid)
            {
                // Define file paths
                string uploadFolder = Path.Combine(env.WebRootPath, "VerifyImg");

                // Save Front image
                string frontFileName = Guid.NewGuid().ToString() + "_" + v.Front.FileName;
                string frontFilePath = Path.Combine(uploadFolder, frontFileName);
                using (var fileStream = new FileStream(frontFilePath, FileMode.Create))
                {
                    v.Front.CopyTo(fileStream);
                }

                // Save Back image
                string backFileName = Guid.NewGuid().ToString() + "_" + v.Back.FileName;
                string backFilePath = Path.Combine(uploadFolder, backFileName);
                using (var fileStream = new FileStream(backFilePath, FileMode.Create))
                {
                    v.Back.CopyTo(fileStream);
                }

                // Save Selfie
                string selfieFileName = Guid.NewGuid().ToString() + "_" + v.Self.FileName;
                string selfieFilePath = Path.Combine(uploadFolder, selfieFileName);
                using (var fileStream = new FileStream(selfieFilePath, FileMode.Create))
                {
                    v.Self.CopyTo(fileStream);
                }


                Verify_Img img = new Verify_Img()
                {
                    FrontCNIC = frontFileName,
                    BackCNIC = backFileName,
                    Selfie = selfieFileName,
                };

                sc.tblVerify.Add(img);
                sc.SaveChanges();
                ModelState.Clear();

                
                return RedirectToAction("Verification");
            }

            return View(v);
        }



        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login");
        }
    }
}
