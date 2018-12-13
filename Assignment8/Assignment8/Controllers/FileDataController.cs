using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web.UI.WebControls;
using System.Net.Http.Headers;
using Microsoft.WindowsAPICodePack.Shell;
using System.Drawing;

namespace Assignment8.Controllers
{
    public class FileDataController : ApiController
    {
        [HttpGet]
        public List<FileDTO> ShowAllFiles()
        {
            int parentId = Convert.ToInt32(HttpContext.Current.Request["parentId"]);
            int userId = Convert.ToInt32(HttpContext.Current.Request["userId"]);
            return Drive.BAL.FileBO.GetAllFiles(parentId,userId);
        }
        [HttpPost]
        public int DeleteFile()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["id"]);
            //int userId = Convert.ToInt32(HttpContext.Current.Request["userId"]);
            return Drive.BAL.FileBO.DeleteFile(Id);
        }
        [HttpGet]
        public Object DownloadFile(String uniqueName)
        {
            var rootPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles");
            FileDTO dto = (FileDTO)Drive.BAL.FileBO.GetFileByUniqueName(uniqueName);
            if (dto != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var fileFullPath = System.IO.Path.Combine(rootPath, dto.UniqueName + dto.FileExt);
                byte[] file = System.IO.File.ReadAllBytes(fileFullPath);
                System.IO.MemoryStream md = new System.IO.MemoryStream(file);
                response.Content = new ByteArrayContent(file);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(dto.ContentType);
                response.Content.Headers.ContentDisposition.FileName = dto.Name;
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
        [HttpGet]
        public List<FileDTO> Search()
        {
            string fname = Convert.ToString(HttpContext.Current.Request["fName"]);
            int userId = Convert.ToInt32(HttpContext.Current.Request["userId"]);
            return Drive.BAL.FileBO.Search(fname, userId);
        }
        [HttpGet]
        public Object GenerateThumbnail(string uniqueName)
        {
            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            FileDTO dto = (FileDTO)Drive.BAL.FileBO.GetFileByUniqueName(uniqueName);
            if (dto != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var fileFullPath = Path.Combine(rootPath, dto.UniqueName + dto.FileExt);
                ShellFile shellFile = ShellFile.FromFilePath(fileFullPath);
                Bitmap shellThumb = shellFile.Thumbnail.LargeBitmap;
                byte[] file = ImageToBytes(shellThumb);
                MemoryStream ms = new MemoryStream(file);

                response.Content = new ByteArrayContent(file);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");

                response.Content.Headers.ContentType = new MediaTypeHeaderValue(dto.ContentType);
                response.Content.Headers.ContentDisposition.FileName = dto.Name;
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }

        }
        private byte[] ImageToBytes(System.Drawing.Image img)
        {
            using (var stream = new MemoryStream())
            {
                ImageConverter _imageConverter = new ImageConverter();
                byte[] xByte = (byte[])_imageConverter.ConvertTo(img, typeof(byte[]));
                return xByte;
            }
        }
        [HttpPost]
        public Object UploadFile()
        {
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                try
                {
                    foreach (var fileName in HttpContext.Current.Request.Files.AllKeys)
                    {
                        HttpPostedFile file = HttpContext.Current.Request.Files[fileName];
                        if (file != null)
                        {
                            FileDTO dto = new FileDTO();
                            dto.Name = file.FileName;
                            dto.FileExt = Path.GetExtension(file.FileName);
                            //dto.ContentType = file.ContentType;
                            dto.UniqueName = Guid.NewGuid().ToString();
                            dto.FileSizeInKB = (file.ContentLength)/1024;
                            dto.UploadedOn = DateTime.Now;
                            dto.ParentFolderId =Convert.ToInt32( HttpContext.Current.Request["parentId"]);
                            dto.ContentType = file.ContentType;
                            dto.CreatedBy = Convert.ToInt32(HttpContext.Current.Request["userId"]);
                            dto.IsActive = true;
                            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
                            var fileSavePath = System.IO.Path.Combine(rootPath, dto.UniqueName + dto.FileExt);
                            file.SaveAs(fileSavePath);
                            var id=Drive.BAL.FileBO.SaveFile(dto);
                            Object data = new
                            {
                                ID=id,
                                Name=dto.Name,
                                UniqueName=dto.UniqueName,
                                Ext=dto.FileExt
                            };
                            return data;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            return response;
        }
    }
}