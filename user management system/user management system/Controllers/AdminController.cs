using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace user_management_system.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult AdminHome()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/home/index");
            }
            DBHandler handle = new DBHandler();
            var users=handle.getAllUsers();
            return View(users);
        }
        public ActionResult Edit(int id)
        {
            DBHandler handle = new DBHandler();
            var user = handle.getUserByID(id);
            ViewBag.ID = user.ID;
            ViewBag.Cric = user.isCricket;
            ViewBag.Hock = user.isHockey;
            ViewBag.Chess = user.isChess;
            ViewBag.Name = user.txtName;
            ViewBag.Login = user.txtLogin;
            ViewBag.Pass = user.txtPassword;
            ViewBag.Email = user.txtEmail;
            ViewBag.Gender = user.cmbGender;
            ViewBag.Age = user.txtAge;
            ViewBag.Address = user.txtAddress;
            ViewBag.Nic = user.txtNic;
            ViewBag.Dob = user.txtDob.Date.ToString("MM/dd/yyyy");
            //ViewBag.dd = user.txtDob.Date.ToString("MM/DD/YYYY");
            ViewBag.ImgName = user.imageName;
            ViewBag.edit = true;
            return View("~/Views/User/NewUser.cshtml");
        }
        public ActionResult Logout()
        {
            Session["login"] = null;
            Session["admin"] = null;
            Session.Abandon();
            return Redirect("~/home/index");
        }
        [HttpPost]
        public ActionResult Login()
        {
            string login = Request["txtLogin"];
            ViewBag.login = login;
            string password = Request["txtPassword"];
            DBHandler handle = new DBHandler();
            int retVal = handle.validateAdmin(login, password);
            if (retVal > 0)
            {
                Session["login"] = login;
                Session["admin"] = "admin";
                return RedirectToAction("AdminHome");
            }
            else
            {
                ViewBag.Message = "Invalid Login/Password";
                return View("~/Views/Admin/AdminLogin.cshtml");
            }
        }

    }
}
