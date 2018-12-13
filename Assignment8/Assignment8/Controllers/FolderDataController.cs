using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Font;
using iText.Layout.Element;
using System.IO;
using iText.IO.Font;
using System.Collections;


namespace Assignment8.Controllers
{
    public class FolderDataController : ApiController
    {
        [HttpPost]
        public int CreateFolder()
        {
            
            FolderDTO dto = new FolderDTO();
            dto.Name = HttpContext.Current.Request["foName"];
            dto.ParentFolderId =Convert.ToInt32( HttpContext.Current.Request["parentId"]); 
            dto.CreatedBy = Convert.ToInt32( HttpContext.Current.Request["createdBy"]); 
            dto.CreatedOn = DateTime.Now;
            dto.IsActive = true;    
            return Drive.BAL.FolderBO.CreateFolder(dto);
        }
        [HttpGet]
        public List<FolderDTO> ShowAllFolders()
        {
            int parentId = Convert.ToInt32(HttpContext.Current.Request["parentId"]);
            int userId = Convert.ToInt32(HttpContext.Current.Request["userId"]);
            return Drive.BAL.FolderBO.GetAllFolders(parentId,userId);
        }
        [HttpPost]
        public int DeleteFolder()
        {
            int Id = Convert.ToInt32(HttpContext.Current.Request["id"]);
            //int userId = Convert.ToInt32(HttpContext.Current.Request["userId"]);
            return Drive.BAL.FolderBO.DeleteFolder(Id);
        }
        [HttpGet]
        public Object DownloadMeta(int parent,int user)
        {
            var rootPath = System.Web.HttpContext.Current.Server.MapPath("~/UploadedFiles");
            
            PdfWriter writer = new PdfWriter (rootPath+"/Meta.pdf");
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);

            List<FolderDTO> folders = Drive.BAL.FolderBO.GetAllFolders(parent,user);
            List<FileDTO> files = Drive.BAL.FileBO.GetAllFiles(parent,user);

            Queue content = new Queue();
            content.Enqueue(folders);
            content.Enqueue(files);
            while (content.Count>0)
            {
                List<FileDTO> dto = new List<FileDTO>();
                var element = content.Dequeue();
                if (element.GetType() ==dto.GetType() )
                {
                    files = (List<FileDTO>)element;
                    foreach (var file in files)
                    {
                        document.Add(new Paragraph("Name: " + file.Name).SetFont(font));
                        document.Add(new Paragraph("Type: File").SetFont(font));
                        document.Add(new Paragraph("Size: " + file.FileSizeInKB +" KB").SetFont(font));
                        var parentName=Drive.BAL.FileBO.GetParentName(file.ParentFolderId);
                        
                        document.Add(new Paragraph("Parent: " +(parentName==""?"ROOT":parentName) ).SetFont(font));
                        document.Add(new Paragraph().SetFont(font));
                    }
                }
                else
                {
                    folders = (List<FolderDTO>)element;
                    foreach (var folder in folders)
                    {
                        document.Add(new Paragraph("Name: " + folder.Name).SetFont(font));
                        document.Add(new Paragraph("Type: Folder").SetFont(font));
                        document.Add(new Paragraph("Size: None").SetFont(font));
                        var parentName=Drive.BAL.FolderBO.GetParentName(folder.ParentFolderId);

                        document.Add(new Paragraph("Parent: " + (parentName==""?"ROOT":parentName) ).SetFont(font));
                        document.Add(new Paragraph().SetFont(font));
                        List<FolderDTO> tempFo = Drive.BAL.FolderBO.GetAllFolders(folder.ID, user);
                        List<FileDTO> tempFi = Drive.BAL.FileBO.GetAllFiles(folder.ID, user);
                        if (tempFo.Count > 0)
                        {
                            content.Enqueue(tempFo);
                        }
                        if (tempFi.Count > 0)
                        {
                            content.Enqueue(tempFi);
                        }
                    }
                }
                document.Add(new Paragraph().SetFont(font));
            }

            document.Close();
            
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            var fileFullPath = System.IO.Path.Combine(rootPath, "Meta.pdf");
            byte[] file1 = System.IO.File.ReadAllBytes(fileFullPath);
            System.IO.MemoryStream md = new System.IO.MemoryStream(file1);
            response.Content = new ByteArrayContent(file1);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            response.Content.Headers.ContentDisposition.FileName = "Meta.pdf";
            return response;
        }
    }
}