using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.BAL
{
    public class UserBO
    {
        public static UserDTO ValidateUser(String pLogin, String pPassword)
        {
            return Drive.DAL.UserDAO.ValidateUser(pLogin, pPassword);
        }
        public static UserDTO GetUserById(int pid)
        {
            return Drive.DAL.UserDAO.GetUserById(pid);
        }
        public static List<UserDTO> GetAllUsers()
        {
            return Drive.DAL.UserDAO.GetAllUsers();
        }
    }
}
