using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.BAL
{
    public class FileBO
    {
        public static List<FileDTO> GetAllFiles(int parentId,int user)
        {
            return Drive.DAL.FileDAO.GetAllFiles(parentId,user);
        }

        public static int SaveFile(FileDTO dto)
        {
           return Drive.DAL.FileDAO.SaveFile(dto);
        }

        public static object GetFileByUniqueName(string uniqueName)
        {
            return Drive.DAL.FileDAO.GetFileByUniqueName(uniqueName);
        }

        public static int DeleteFile(int Id)
        {
            return Drive.DAL.FileDAO.DeleteFile(Id);
        }

        public static List<FileDTO> GetAllFiles(int parent)
        {
            return Drive.DAL.FileDAO.GetAllFiles(parent);
        }

        public static string GetParentName(int p)
        {
            return Drive.DAL.FileDAO.GetParentName(p);
        }

        public static List<FileDTO> Search(string fname, int userId)
        {
            return Drive.DAL.FileDAO.Search(fname, userId);
        }
    }
}
