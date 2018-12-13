using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL
{
    public static class UserDAO
    {
        public static int Save(UserDTO dto)
        {
            String sqlQuery = "";
            if (dto.UserID > 0)
            {
                sqlQuery = String.Format("Update dbo.Users Set Name='{0}', PictureName='{1}',Login='{2}',Email='{3}' Where UserID={4}",
                    dto.Name, dto.PictureName,dto.Login,dto.Email, dto.UserID);
            }
            else
            {
                sqlQuery = String.Format("INSERT INTO dbo.Users(Name, Login,Password, PictureName,email, IsAdmin,IsActive) VALUES('{0}','{1}','{2}','{3}','{4}',{5},{6}); Select SCOPE_IDENTITY()",
                    dto.Name, dto.Login, dto.Password, dto.PictureName,dto.Email, 0, 1);
            }

            using (DBHelper helper = new DBHelper())
            {
                if(dto.UserID>0)
                    return helper.ExecuteQuery(sqlQuery);
                return Convert.ToInt32( helper.ExecuteScalar(sqlQuery));
            }
        }
        public static int ConfirmOldPassword(String old,int id)
        {
            String sqlQuery = "";
            sqlQuery = String.Format("Select count(*) from dbo.Users where Password='{0}' and UserID={1}", old, id);


            using (DBHelper helper = new DBHelper())
            {
                return Convert.ToInt32(helper.ExecuteScalar(sqlQuery));
            }
        }
        public static int verifyEmail(string email)
        {

            string query = string.Format("Select userid from dbo.Users where email='{0}'", email);
            using (DBHelper helper = new DBHelper())
            {
                return Convert.ToInt32(helper.ExecuteScalar(query));
            }
        }
        public static int UpdatePassword(UserDTO dto)
        {
            String sqlQuery = "";
            sqlQuery = String.Format("Update dbo.Users Set Password='{0}' Where UserID={1}", dto.Password, dto.UserID);


            using (DBHelper helper = new DBHelper())
            {
                return helper.ExecuteQuery(sqlQuery);
            }
        }

        public static UserDTO ValidateUser(String pLogin, String pPassword)
        {
            var query = String.Format("Select * from dbo.Users Where Login='{0}' and Password='{1}'", pLogin, pPassword);

            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);

                UserDTO dto = null;

                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }

                return dto;
            }
        }

        public static UserDTO GetUserById(int pid)
        {

            var query = String.Format("Select * from dbo.Users Where UserId={0}", pid);

            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);

                UserDTO dto = null;

                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }

                return dto;
            }
        }

        public static List<UserDTO> GetAllUsers()
        {
            using (DBHelper helper = new DBHelper())
            {
                var query = "Select * from dbo.Users Where IsActive = 1;";
                var reader = helper.ExecuteReader(query);
                List<UserDTO> list = new List<UserDTO>();

                while (reader.Read())
                {
                    var dto = FillDTO(reader);
                    if (dto != null)
                    {
                        list.Add(dto);
                    }
                }

                return list;
            }
        }

        public static int DeleteUser(int pid)
        {
            String sqlQuery = String.Format("Update dbo.Users Set IsActive=0 Where UserID={0}", pid);

            using (DBHelper helper = new DBHelper())
            {
                return helper.ExecuteQuery(sqlQuery);
            }
        }

        private static UserDTO FillDTO(SqlDataReader reader)
        {
            var dto = new UserDTO();
            dto.UserID = reader.GetInt32(0);
            dto.Name = reader.GetString(1);
            dto.Login = reader.GetString(2);
            dto.Password = reader.GetString(3);
            dto.PictureName = reader.GetString(4);
            dto.IsAdmin = reader.GetBoolean(5);
            dto.IsActive = reader.GetBoolean(6);
            dto.Email = reader.GetString(reader.GetOrdinal("Email"));
            return dto;
        }

        public static UserDTO CheckExisting(string pLogin, string pEmail)
        {
            var query = String.Format("Select * from dbo.Users Where Login='{0}' or Email='{1}'", pLogin, pEmail);

            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);

                UserDTO dto = null;

                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }

                return dto;
            }
        }
        public static UserDTO CheckExisting2(string pLogin, string pEmail,int id)
        {
            var query = String.Format("Select * from dbo.Users Where (Login='{0}' or Email='{1}') and UserID <> {2}", pLogin, pEmail,id);

            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);

                UserDTO dto = null;

                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }

                return dto;
            }
        }
    }
}
