using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace user_management_system.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        [HttpPost]
        public ActionResult NewUser(UserDTO user)
        {
           
            ViewBag.Cric = user.isCricket;
            ViewBag.Hock = user.isHockey;
            ViewBag.Chess = user.isChess;
            if ((ViewBag.Name = user.txtName) == "" || (ViewBag.Login = user.txtLogin) == "" || (ViewBag.Pass = user.txtPassword) == "" || (ViewBag.Email = user.txtEmail) == "" || (ViewBag.Gender = user.cmbGender) == '2' || (ViewBag.Age = user.txtAge) == null || (ViewBag.Address = user.txtAddress) == "" || (ViewBag.Nic = user.txtNic) == "" || (ViewBag.Dob = user.txtDob) == null)
            {
                ViewBag.Message = "**Please Enter Valid data.";
            }
            else
            {
                var uniqueName = "";
                if (Request.Files["image1"] != null)
                {
                    var file = Request.Files["image1"];
                    if (file.FileName != "")
                    {
                        var ext = System.IO.Path.GetExtension(file.FileName);
                        uniqueName = Guid.NewGuid().ToString() + ext;
                        var rootpath = Server.MapPath("~/Images");
                        var fileSavePath = System.IO.Path.Combine(rootpath, uniqueName);
                        file.SaveAs(fileSavePath);
                    }
                    else
                    {
                        ViewBag.Message = "**Please select image first.";
                        return View("~/Views/User/NewUser.cshtml");
                    }
                }
                DBHandler handle = new DBHandler();
                user.imageName = uniqueName;
                int retVal = handle.SaveUser(user);
                if (retVal == -1)
                {
                    ViewBag.Message = "**User with this login/Email Already Exists.";
                }
                else if (retVal > 0)
                {
                    Session["login"]=user.txtLogin;
                    Session["ID"] = retVal;
                    return RedirectToAction("UserHome");
                }   
                else
                    ViewBag.Message = "**Data couldn't be saved for some reason.";
            }
            
            
            return View("~/Views/User/NewUser.cshtml");
        }
        public ActionResult UserHome()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/home/index");
            }
            DBHandler handle = new DBHandler();
            int id = Convert.ToInt32(Session["ID"]);
            ViewBag.ImgName=handle.getImageName(id);
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string forgEmail)
        {
            DBHandler handle = new DBHandler();
            int retVal=handle.verifyEmail(forgEmail);
            if (retVal > 0)
            {
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                int code = _rdm.Next(_min, _max);
                string fromEmail = "ead.csf15@gmail.com";
                string fromPass = "EAD_csf15m";
                string fromName = "Admin";
                MailAddress fromAddress = new MailAddress(fromEmail, fromName);
                MailAddress toAddress = new MailAddress(forgEmail);
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPass)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = "Reset Password",
                    Body = code.ToString(),
                })
                {
                    smtp.Send(message);
                }
                Session["code"] = Convert.ToString(code);
                Session["email"] = forgEmail;
                return View("CheckCode");
            }
            else
            {
                ViewBag.Message="Email doesn't exist";
                return View("ExistingUser");
            }
            
        }
        [HttpPost]
        public ActionResult CheckCode(string txtCode)
        {
            string tempCode = Session["code"].ToString();
            if (tempCode==txtCode)
            {
                return RedirectToAction("ResetPassword");
            }
            else
            {
                ViewBag.Message = "Wrong Code";
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View("ResetPassword");
        }
        [HttpPost]
        public ActionResult ResetPassword(string txtPassNew)
        {
            DBHandler handle = new DBHandler();
            int retval=handle.ResetPassword(txtPassNew, Convert.ToString(Session["email"]));
            if (retval > 0)
            {
                string login = handle.getLoginByEmail(Session["email"].ToString());
                Session["email"] = null;
                Session["code"] = null;
                Session["login"] = login;
                var user = handle.getUserByLogin(login);
                Session["ID"] = user.ID;
                return RedirectToAction("UserHome");
            }
            return Redirect("~/home/index");
        }
        public ActionResult Logout()
        {
            Session["login"] = null;
            Session["ID"] = null;
            Session.Abandon();
            return Redirect("~/home/index");
        }
        [HttpPost]
        public ActionResult Login()
        {     
            string login= Request["txtLogin"];
            ViewBag.login=login;
            string password = Request["txtPassword"];
            DBHandler handle = new DBHandler();
            int retVal=handle.validateUser(login, password);
            if (retVal>0)
            {
                Session["login"] = login;
                Session["ID"] = retVal;
                return RedirectToAction("UserHome");
            }
            else
            {
                ViewBag.Message = "Invalid Login/Password";
                return View("~/Views/User/ExistingUser.cshtml");
            }
            
        }
        public ActionResult EditUser()
        {
            DBHandler handle = new DBHandler();
            UserDTO user= handle.getUserByLogin(Session["login"].ToString());
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
        [HttpPost]
        public ActionResult EditUser(UserDTO user)
        {
           
            ViewBag.Cric = user.isCricket;
            ViewBag.Hock = user.isHockey;
            ViewBag.Chess = user.isChess;
            
            if ((ViewBag.Name = user.txtName) == "" || (ViewBag.Login = user.txtLogin) == "" || (ViewBag.Pass = user.txtPassword) == "" || (ViewBag.Email = user.txtEmail) == "" || (ViewBag.Gender = user.cmbGender) == '2' || (ViewBag.Age = user.txtAge) == null || (ViewBag.Address = user.txtAddress) == "" || (ViewBag.Nic = user.txtNic) == "" || (ViewBag.Dob = user.txtDob) == null)
            {
                ViewBag.Message = "**Please Enter Valid data.";
            }
            else
            {
                
                DBHandler handle = new DBHandler();
                
                int retVal = handle.SaveUser(user);
                if (retVal > 0)
                {
                    if (Session["admin"] != null)
                    {
                        return RedirectToAction("AdminHome","Admin");
                    }
                    Session["login"] = user.txtLogin;
                    Session["ID"] = user.ID;
                    return RedirectToAction("UserHome");
                }
                else
                    ViewBag.Message = "**Data couldn't be saved for some reason.";
            }
            ViewBag.ImgName = user.imageName;
            ViewBag.edit = true;
            return View("~/Views/User/NewUser.cshtml");
        }
    }
}
