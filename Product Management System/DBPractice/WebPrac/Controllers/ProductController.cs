using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPrac.Security;

namespace WebPrac.Controllers
{
    public class ProductController : Controller
    {
        private ActionResult GetUrlToRedirect()
        {
            if (SessionManager.IsValidUser)
            {
                //if (SessionManager.User.IsAdmin == false)
                //{
                //    TempData["Message"] = "Unauthorized Access";
                //    return Redirect("~/Home/NormalUser");
                //}
                return null;
            }
            else
            {
                TempData["Message"] = "Unauthorized Access";
                return Redirect("~/User/Login");
            }

            
        }
        public ActionResult ShowAll()
        {
            if (SessionManager.IsValidUser == false)
            {
                return Redirect("~/User/Login");
            }

            var products = PMS.BAL.ProductBO.GetAllProducts(true);

            return View(products);
        }

        public ActionResult New()
        {
            var redVal = GetUrlToRedirect();
            if (redVal == null)
            {
                var dto = new ProductDTO();
                redVal =  View(dto);
            }

            return redVal;
        }

        public ActionResult Edit(int id)
        {

            var redVal = GetUrlToRedirect();
            if (redVal == null)
            {
                var prod = PMS.BAL.ProductBO.GetProductById(id);
                var user = (UserDTO)Session["user"];
                if (user.UserID == prod.CreatedBy || user.IsAdmin)
                {
                    redVal = View("New", prod);
                }
                else
                {
                    TempData["Msg"] = "You are not authorized to edit this Product.";
                    redVal = RedirectToAction("ShowAll");
                }
                
            }

            return redVal;
            
        }
        public ActionResult Edit2(int ProductID)
        {
            var redVal = GetUrlToRedirect();
            if (redVal == null)
            {
                var prod = PMS.BAL.ProductBO.GetProductById(ProductID);
                //return View("New", prod);
                var user = (UserDTO)Session["user"];
                if (user.UserID == prod.CreatedBy || user.IsAdmin)
                {
                    redVal = View("New", prod);
                }
                else
                {
                    TempData["Msg"] = "You are not authorized to edit this Product.";
                    redVal = RedirectToAction("ShowAll");
                }

            }

            return redVal;
            
        }
        public ActionResult Delete(int id)
        {

            if (SessionManager.IsValidUser)
            {

                //if (SessionManager.User.IsAdmin == false)
                //{
                //    TempData["Message"] = "Unauthorized Access";
                //    return Redirect("~/Home/NormalUser");
                //}
                var prod = PMS.BAL.ProductBO.GetProductById(id);
                var user = (UserDTO)Session["user"];
                if (user.UserID == prod.CreatedBy || user.IsAdmin)
                {
                    PMS.BAL.ProductBO.DeleteProduct(id);
                    TempData["Msg"] = "Record is deleted!";
                    return RedirectToAction("ShowAll");
                }
                else
                {
                    TempData["Msg"] = "You are not authorized to delete this Product.";
                    return RedirectToAction("ShowAll");
                }
                
            }
            else
            {
                return Redirect("~/User/Login");
            }

           
        }
        [HttpPost]
        public ActionResult Save(ProductDTO dto)
        {

            if (SessionManager.IsValidUser==false)
            {

                //if (SessionManager.User.IsAdmin == false)
                //{
                //    TempData["Message"] = "Unauthorized Access";
                //    return Redirect("~/Home/NormalUser");
                //}
                return Redirect("~/User/Login");
            }
            


            var uniqueName = "";

            if (Request.Files["Image"] != null)
            {
                var file = Request.Files["Image"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);

                    //Generate a unique name using Guid
                    uniqueName = Guid.NewGuid().ToString() + ext;

                    //Get physical path of our folder where we want to save images
                    var rootPath = Server.MapPath("~/UploadedFiles");

                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);

                    // Save the uploaded file to "UploadedFiles" folder
                    file.SaveAs(fileSavePath);

                    dto.PictureName = uniqueName;
                }
            }

            var user=(UserDTO)Session["user"];

            if (dto.ProductID > 0)
            {
                dto.ModifiedOn = DateTime.Now;
                dto.ModifiedBy = user.UserID;
            }
            else
            {
                dto.CreatedOn = DateTime.Now;
                dto.CreatedBy = user.UserID;
            }

            PMS.BAL.ProductBO.Save(dto);

            TempData["Msg"] = "Record is saved!";

            return RedirectToAction("ShowAll");
        }

    }
}