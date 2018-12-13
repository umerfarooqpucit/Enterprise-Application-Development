using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.BAL
{
    public class FolderBO
    {
        public static int CreateFolder(FolderDTO dto)
        {
            return Drive.DAL.FolderDAO.CreateFolder(dto);
        }
        public static List<FolderDTO> GetAllFolders(int parent,int user)
        {
            return Drive.DAL.FolderDAO.GetAllFolders(parent,user);
        }

        public static int DeleteFolder(int Id)
        {
            return Drive.DAL.FolderDAO.DeleteFolder(Id);
        }

        public static List<FolderDTO> GetAllFolders(int parent)
        {
            return Drive.DAL.FolderDAO.GetAllFolders(parent);
        }

        public static string GetParentName(int p)
        {
            return Drive.DAL.FolderDAO.GetParentName(p);
        }
    }
}
