using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment8.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {
            UserDTO user = (UserDTO)Session["user"];
            if (user == null)
                return Redirect("~/User/Login");
            //ViewBag.parent = Request["parentId"];
            //if (ViewBag.parent == null)
            //    ViewBag.parent = 0;
            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}