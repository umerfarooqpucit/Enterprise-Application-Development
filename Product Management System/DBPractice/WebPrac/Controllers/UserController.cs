using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebPrac.Security;

namespace WebPrac.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String login, String password)
        {
            var obj = PMS.BAL.UserBO.ValidateUser(login, password);
            if (obj != null)
            {
                Session["user"] = obj;
                if (obj.IsAdmin == true)
                    return Redirect("~/Home/Admin");
                else
                    return Redirect("~/Home/NormalUser");
            }

            ViewBag.MSG = "Invalid Login/Password";
            ViewBag.Login = login;

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserDTO user)
        {
            if (user.Email == "" || user.Name == "" || user.Password == "" || user.Login == "")
            {
                ViewBag.MSG = "Please Fill required information";
                return View();
            }
            var obj = PMS.BAL.UserBO.CheckExisting(user.Login, user.Email);
            if (obj == null)
            {
                var uniqueName = "";
                if (Request.Files["image1"] != null)
                {
                    var file = Request.Files["image1"];
                    if (file.FileName != "")
                    {
                        var ext = System.IO.Path.GetExtension(file.FileName);
                        if (ext != ".jpg")
                        {
                            ViewBag.Login = user.Login;
                            ViewBag.Name = user.Name;
                            ViewBag.Email = user.Email;
                            ViewBag.ImgName = user.PictureName;
                            ViewBag.ID = user.UserID;
                            ViewBag.MSG = "Only JPG files are allowed.";
                            return View("SignUp");
                        }
                        uniqueName = Guid.NewGuid().ToString() + ext;
                        var rootpath = Server.MapPath("~/UploadedFiles");
                        var fileSavePath = System.IO.Path.Combine(rootpath, uniqueName);
                        file.SaveAs(fileSavePath);
                    }
                    else
                    {
                        ViewBag.MSG = "**Please select image first.";
                        ViewBag.Login = user.Login;
                        ViewBag.Name = user.Name;
                        ViewBag.Email = user.Email;
                        return View();
                    }
                }
                user.PictureName = uniqueName;
                user.UserID=PMS.BAL.UserBO.Save(user);
                user.IsActive = true;
                Session["user"] = user;
                return Redirect("~/Home/NormalUser");
            }

            ViewBag.MSG = "Login/Email already exists.";
            ViewBag.Login = user.Login;
            ViewBag.Name = user.Name;
            ViewBag.Email = user.Email;
            ViewBag.ImgName = user.PictureName;
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(UserDTO dto)
        {
            //User Save Logic
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            SessionManager.ClearSession();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Edit()
        {
            if (SessionManager.IsValidUser)
            {

                if (SessionManager.User.IsAdmin)
                {
                    return Redirect("~/Home/Admin");
                }
                else
                {
                    var user = (UserDTO)Session["user"];
                    //PMS.BAL.UserBO.GetUserById(obj.UserID);
                    ViewBag.ID = user.UserID;

                    ViewBag.Name = user.Name;
                    ViewBag.Login = user.Login;
                    ViewBag.Pass = user.Password;
                    ViewBag.Email = user.Email;
                    ViewBag.ImgName = user.PictureName;
                    ViewBag.edit = true;
                    return View("~/Views/User/SignUp.cshtml");
                }
            }
            else
            {
                return Redirect("~/User/Login");
            }
            
        }
        [HttpPost]
        public ActionResult Edit(UserDTO user)
        {
            ViewBag.edit = true;
            if (user.Email == "" || user.Name == "" || user.Password == "" || user.Login == "")
            {
                ViewBag.MSG = "Please Fill required information";
                return View("SignUp");
            }
            var obj = PMS.BAL.UserBO.CheckExisting2(user.Login, user.Email,user.UserID);
            if (obj == null)
            {
                var uniqueName = "";
                if (Request.Files["image1"] != null)
                {
                    var file = Request.Files["image1"];
                    if (file.FileName != "")
                    {
                        var ext = System.IO.Path.GetExtension(file.FileName);
                        if (ext != ".jpg")
                        {
                            ViewBag.Login = user.Login;
                            ViewBag.Name = user.Name;
                            ViewBag.Email = user.Email;
                            ViewBag.ImgName = user.PictureName;
                            ViewBag.ID = user.UserID;
                            ViewBag.MSG = "Only JPG files are allowed.";
                            return View("SignUp");
                        }
                        
                        uniqueName = Guid.NewGuid().ToString() + ext;
                        var rootpath = Server.MapPath("~/UploadedFiles");
                        var fileSavePath = System.IO.Path.Combine(rootpath, uniqueName);
                        file.SaveAs(fileSavePath);
                        user.PictureName = uniqueName;
                    }
                }
                int retVal=PMS.BAL.UserBO.Save(user);
                if (retVal > 0)
                {
                    Session["user"] = user;
                    return Redirect("~/Home/NormalUser");
                }
                ViewBag.MSG = "Data couldn't be updated for some reason";
                return View("SignUp");
            }
            ViewBag.MSG = "Login/Email already exists.";
            ViewBag.Login = user.Login;
            ViewBag.Name = user.Name;
            ViewBag.Email = user.Email;
            ViewBag.ImgName = user.PictureName;
            ViewBag.ID = user.UserID;
            return View("SignUp");
            
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (SessionManager.IsValidUser)
            {

                if (SessionManager.User.IsAdmin)
                {
                    return Redirect("~/Home/Admin");
                }
                else
                {
                    var user = (UserDTO)Session["user"];
                    //PMS.BAL.UserBO.GetUserById(obj.UserID);

                    return View();
                }
            }
            else
            {
                return Redirect("~/User/Login");
            }
            
        }
        [HttpPost]
        public ActionResult ChangePassword(String OldPass,String NewPass,String ConPass )
        {
            ViewBag.edit = true;
            if (OldPass == "" || NewPass == "" || ConPass == "" )
            {
                ViewBag.MSG = "Please Fill required information";
                return View();
            }
            var user = (UserDTO)Session["user"];
            int retVal = PMS.BAL.UserBO.ConfirmOldPassword(OldPass, user.UserID);
            if (retVal>0)
            {
                if (NewPass != ConPass)
                {
                    ViewBag.OldPass = OldPass;
                    ViewBag.NewPass = NewPass;
                    ViewBag.MSG = "'Confirm password' doesn't match";
                    return View();
                }
                if (NewPass == OldPass)
                {
                    ViewBag.OldPass = OldPass;
                    ViewBag.MSG = "Please choose a different Password.";
                    return View();
                }
                user.Password = NewPass;
                int ret = PMS.BAL.UserBO.UpdatePassword(user);
                if (ret > 0)
                {
                    Session["user"] = user;
                    return Redirect("~/Home/NormalUser");
                }
                ViewBag.MSG = "Password couldn't be updated for some reason";
                return View();
            }
            ViewBag.MSG = "Please Enter Correct 'Old Password'";
            return View();

        }
        [HttpPost]
        public ActionResult ForgotPassword(string forgEmail)
        {
            int userId=PMS.BAL.UserBO.verifyEmail(forgEmail);

            if (userId > 0)
            {
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                int code = _rdm.Next(_min, _max);
                string fromEmail = "ead.csf15@gmail.com";
                string fromPass = "EAD_csf15m";
                string fromName = "PMS Admin";
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
                    Body = "Your code to reset Password is "+code.ToString(),
                })
                {
                    smtp.Send(message);
                }
                Session["code"] = Convert.ToString(code);
                Session["ID"] = userId;
                return View("CheckCode");
            }
            else
            {
                ViewBag.MSG = "Email doesn't exist";
                return View("Login");
            }

        }
        [HttpPost]
        public ActionResult CheckCode(string txtCode)
        {
            string tempCode = Session["code"].ToString();
            if (tempCode == txtCode)
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
            if (SessionManager.IsValidUser)
            {

                if (SessionManager.User.IsAdmin)
                {
                    return Redirect("~/Home/Admin");
                }
                else
                {
                    return View("ResetPassword");
                }
            }
            else
            {
                return Redirect("~/User/Login");
            }
            
            
        }
        [HttpPost]
        public ActionResult ResetPassword(string txtPassNew)
        {
            int id=Convert.ToInt32(Session["ID"]);
            var user = PMS.BAL.UserBO.GetUserById(id);
            user.Password = txtPassNew;
            int ret = PMS.BAL.UserBO.UpdatePassword(user);
            Session["code"] = null;
            Session["ID"] = null;
            if (ret > 0)
            {
                Session["user"] = user;
                return Redirect("~/Home/NormalUser");
            }
            ViewBag.MSG = "Password couldn't be updated for some reason";
            return View();
        }
        [HttpGet]
        public ActionResult Login2()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UProfile(int id)
        {
            if (SessionManager.IsValidUser)
            {

                var user = PMS.BAL.UserBO.GetUserById(id);
                return View("Profile", user);
            }
            else
            {
                return Redirect("~/User/Login");
            }
           
        }
        [HttpPost]
        public JsonResult ValidateUser(String login, String password)
        {

            Object data = null;

            try
            {
                var url = "";
                var flag = false;

                var obj = PMS.BAL.UserBO.ValidateUser(login, password);
                if (obj != null)
                {
                    flag = true;
                    SessionManager.User = obj;

                    if (obj.IsAdmin == true)
                        url = Url.Content("~/Home/Admin");
                    else
                        url = Url.Content("~/Home/NormalUser");
                }

                data = new
                {
                    valid = flag,
                    urlToRedirect = url
                };
            }
            catch (Exception)
            {
                data = new
                {
                    valid = false,
                    urlToRedirect = ""
                };
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
	}
}