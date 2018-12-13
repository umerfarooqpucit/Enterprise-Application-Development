using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace user_management_system.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /MainScreen/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExistingUser()
        {
            return View("~/Views/User/ExistingUser.cshtml");
        }
        public ActionResult NewUser()
        {
            return View("~/Views/User/NewUser.cshtml");
        }
        public ActionResult Admin()
        {
            return View("~/Views/Admin/AdminLogin.cshtml");
        }
    }
}
