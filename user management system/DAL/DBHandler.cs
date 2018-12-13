using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DBHandler
    {
        string conString = @"Data Source=desktop-0o3jt39\sqlexpress;Initial Catalog=Assignment5;Integrated Security=True";
        public int SaveUser(UserDTO user)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                int count = 0;
                string query = "";
                SqlCommand cmd;
                if (user.ID <= 0)
                {
                    query = string.Format("Select count(*) from dbo.Users where login='{0}' or email='{1}'", user.txtLogin, user.txtEmail);
                    cmd = new SqlCommand(query, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
                if (count>0)
                {
                    return -1;
                }
                else
                {
                    int recAff=0;
                    if (user.ID <= 0)
                    {
                        query = string.Format("INSERT into dbo.Users (Name,Login,Password,email,gender,address,age,nic,dob,iscricket,hockey,imagename,chess ) OUTPUT INSERTED.UserID values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}');", user.txtName, user.txtLogin, user.txtPassword, user.txtEmail, user.cmbGender, user.txtAddress, user.txtAge, user.txtNic, user.txtDob, user.isCricket, user.isHockey, user.imageName, user.isChess);
                        cmd = new SqlCommand(query, conn);
                        recAff = (Int32)cmd.ExecuteScalar();
                    }
                    else
                    {
                        query = string.Format("Update dbo.Users set Name='{0}',Login='{1}',Password='{2}',email='{3}',gender='{4}',address='{5}',age='{6}',nic='{7}',dob='{8}',iscricket='{9}',hockey='{10}',chess='{11}' where UserID='{12}'", user.txtName, user.txtLogin, user.txtPassword, user.txtEmail, user.cmbGender, user.txtAddress, user.txtAge, user.txtNic, user.txtDob, user.isCricket, user.isHockey, user.isChess,user.ID);
                        cmd = new SqlCommand(query, conn);
                        recAff = cmd.ExecuteNonQuery();
                    }
                    return recAff;

                } 
            }
        }
        public string getImageName(int id)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string query = string.Format("Select imageName from dbo.Users where UserID='{0}'", id);
                SqlCommand cmd = new SqlCommand(query, conn);
                string imgName = Convert.ToString(cmd.ExecuteScalar());
                return imgName;
            }
        }
        public int ResetPassword(string p,string e)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string query = string.Format("Update dbo.Users set password='{0}' where email='{1}'", p,e);
                SqlCommand cmd = new SqlCommand(query, conn);
                return cmd.ExecuteNonQuery();
            }
        }
        public int validateUser(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string query = string.Format("Select userid from dbo.Users where login='{0}' and password='{1}'", login,password);
                SqlCommand cmd = new SqlCommand(query, conn);
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                return id;
            }
        }
        public int verifyEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string query = string.Format("Select userid from dbo.Users where email='{0}'", email);
                SqlCommand cmd = new SqlCommand(query, conn);
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                return id;
            }
        }
        public string getLoginByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string query = string.Format("Select login from dbo.Users where email='{0}'", email);
                SqlCommand cmd = new SqlCommand(query, conn);
                string login = Convert.ToString(cmd.ExecuteScalar());
                return login;
            }
        }
        public int validateAdmin(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string query = string.Format("Select adminId from dbo.Admin where login='{0}' and password='{1}'", login, password);
                SqlCommand cmd = new SqlCommand(query, conn);
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                return id;
            }
        }
        public List<UserDTO> getAllUsers()
        {
            List<UserDTO> list = new List<UserDTO>();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();

                String query = "Select * from dbo.Users";
                SqlCommand command = new SqlCommand(query, conn);
                var dr = command.ExecuteReader();
                while (dr.Read())
                {
                    UserDTO user = new UserDTO();
                    user.ID = dr.GetInt32(dr.GetOrdinal("UserID"));
                    user.txtLogin = dr.GetString(dr.GetOrdinal("Login"));
                    user.txtName = dr.GetString(dr.GetOrdinal("Name"));
                    user.txtPassword = dr.GetString(dr.GetOrdinal("Password"));
                    user.txtEmail = dr.GetString(dr.GetOrdinal("Email"));
                    user.txtAddress = dr.GetString(dr.GetOrdinal("Address"));
                    user.txtNic = dr.GetString(dr.GetOrdinal("Nic"));
                    user.txtDob = dr.GetDateTime(dr.GetOrdinal("Dob"));
                    user.imageName = dr.GetString(dr.GetOrdinal("imagename"));
                    user.isCricket = dr.GetBoolean(dr.GetOrdinal("IsCricket"));
                    user.isHockey = dr.GetBoolean(dr.GetOrdinal("Hockey"));
                    user.isChess = dr.GetBoolean(dr.GetOrdinal("Chess"));
                    user.txtAge = dr.GetInt32(dr.GetOrdinal("age"));
                    user.cmbGender = dr.GetString(dr.GetOrdinal("gender"))[0];
                    list.Add(user);
                }
            }

            return list;
        }
        public UserDTO getUserByLogin(String login)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string query = string.Format("Select * from dbo.Users where login='{0}'", login);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                UserDTO user = new UserDTO();
                if (dr.Read() == true)
                {
                    user.ID = dr.GetInt32(dr.GetOrdinal("UserID"));
                    user.txtLogin = dr.GetString(dr.GetOrdinal("Login"));
                    user.txtName = dr.GetString(dr.GetOrdinal("Name"));
                    user.txtPassword = dr.GetString(dr.GetOrdinal("Password"));
                    user.txtEmail = dr.GetString(dr.GetOrdinal("Email"));
                    user.txtAddress = dr.GetString(dr.GetOrdinal("Address"));
                    user.txtNic = dr.GetString(dr.GetOrdinal("Nic"));
                    user.txtDob = dr.GetDateTime(dr.GetOrdinal("Dob"));
                    user.imageName = dr.GetString(dr.GetOrdinal("imagename"));
                    user.isCricket = dr.GetBoolean(dr.GetOrdinal("IsCricket"));
                    user.isHockey = dr.GetBoolean(dr.GetOrdinal("Hockey"));
                    user.isChess = dr.GetBoolean(dr.GetOrdinal("Chess"));
                    user.txtAge = dr.GetInt32(dr.GetOrdinal("age"));
                    user.cmbGender = dr.GetString(dr.GetOrdinal("gender"))[0];
                    
                }
                return user;
            }
        }

        public UserDTO getUserByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(conString))
            {
                conn.Open();
                string query = string.Format("Select * from dbo.Users where userid='{0}'", id);
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                UserDTO user = new UserDTO();
                if (dr.Read() == true)
                {
                    user.ID = dr.GetInt32(dr.GetOrdinal("UserID"));
                    user.txtLogin = dr.GetString(dr.GetOrdinal("Login"));
                    user.txtName = dr.GetString(dr.GetOrdinal("Name"));
                    user.txtPassword = dr.GetString(dr.GetOrdinal("Password"));
                    user.txtEmail = dr.GetString(dr.GetOrdinal("Email"));
                    user.txtAddress = dr.GetString(dr.GetOrdinal("Address"));
                    user.txtNic = dr.GetString(dr.GetOrdinal("Nic"));
                    user.txtDob = dr.GetDateTime(dr.GetOrdinal("Dob"));
                    user.imageName = dr.GetString(dr.GetOrdinal("imagename"));
                    user.isCricket = dr.GetBoolean(dr.GetOrdinal("IsCricket"));
                    user.isHockey = dr.GetBoolean(dr.GetOrdinal("Hockey"));
                    user.isChess = dr.GetBoolean(dr.GetOrdinal("Chess"));
                    user.txtAge = dr.GetInt32(dr.GetOrdinal("age"));
                    user.cmbGender = dr.GetString(dr.GetOrdinal("gender"))[0];

                }
                return user;
            }
        }
    }
}
