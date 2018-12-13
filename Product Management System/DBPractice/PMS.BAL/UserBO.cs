using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.BAL
{
    public class UserBO
    {
        public static int Save(UserDTO dto)
        {
            return PMS.DAL.UserDAO.Save(dto);
        }

        public static int UpdatePassword(UserDTO dto)
        {
            return PMS.DAL.UserDAO.UpdatePassword(dto);
        }
        public static int ConfirmOldPassword(String old, int id)
        {
            return PMS.DAL.UserDAO.ConfirmOldPassword(old,id);
        }
        public static int verifyEmail(string email)
        {
            return PMS.DAL.UserDAO.verifyEmail(email);
        }
        public static UserDTO ValidateUser(String pLogin, String pPassword)
        {
            return PMS.DAL.UserDAO.ValidateUser(pLogin, pPassword);
        }
        public static UserDTO CheckExisting(String pLogin, String pEmail)
        {
            return PMS.DAL.UserDAO.CheckExisting(pLogin, pEmail);
        }
        public static UserDTO GetUserById(int pid)
        {
            return PMS.DAL.UserDAO.GetUserById(pid);
        }
        public static List<UserDTO> GetAllUsers()
        {
            return PMS.DAL.UserDAO.GetAllUsers();
        }

        public static int DeleteUser(int pid)
        {
            return PMS.DAL.UserDAO.DeleteUser(pid);
        }


        public static UserDTO CheckExisting2(String pLogin, String pEmail,int id)
        {
            return PMS.DAL.UserDAO.CheckExisting2(pLogin, pEmail,id);
        }
    }
}
