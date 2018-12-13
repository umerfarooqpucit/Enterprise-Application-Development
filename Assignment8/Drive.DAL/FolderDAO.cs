using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Drive.DAL
{
    public class FolderDAO
    {
        public static int CreateFolder(FolderDTO dto)
        {
            String sqlQuery = "";
            sqlQuery = String.Format("INSERT into dbo.Folder(Name,ParentFolderId,Createdby,CreatedOn) Values('{0}',{1},{2},'{3}');  Select SCOPE_IDENTITY();"
                , dto.Name,dto.ParentFolderId,dto.CreatedBy,dto.CreatedOn);


            using (DBHelper helper = new DBHelper())
            {
                return Convert.ToInt32(helper.ExecuteScalar(sqlQuery));
            }
        }

        public static List<FolderDTO> GetAllFolders(int parent,int user)
        {
            using (DBHelper helper = new DBHelper())
            {
                var query = String.Format("Select * from dbo.Folder where ParentFolderId={0} and Createdby={1} and IsActive=1", parent, user);
                var reader = helper.ExecuteReader(query);
                List<FolderDTO> list = new List<FolderDTO>();

                while (reader.Read())
                {
                    var dto = FillDTO(reader);
                    if (dto != null && dto.IsActive==true)
                    {
                        list.Add(dto);
                    }
                }

                return list;
            }
        }
        private static FolderDTO FillDTO(SqlDataReader reader)
        {
            var dto = new FolderDTO();
            dto.ID = reader.GetInt32(0);
            dto.Name = reader.GetString(1);
            dto.ParentFolderId = reader.GetInt32(2);
            dto.CreatedBy = reader.GetInt32(3);
            dto.CreatedOn = reader.GetDateTime(4);
            dto.IsActive = reader.GetBoolean(5);
            return dto;
        }

        public static int DeleteFolder(int Id)
        {
            String sqlQuery = String.Format("Update dbo.Folder Set IsActive=0 Where id={0}", Id);

            using (DBHelper helper = new DBHelper())
            {
                return helper.ExecuteQuery(sqlQuery);
            }
        }

        public static List<FolderDTO> GetAllFolders(int parent)
        {
            using (DBHelper helper = new DBHelper())
            {
                var query = String.Format("Select * from dbo.Folder where ParentFolderId={0} and IsActive=1", parent);
                var reader = helper.ExecuteReader(query);
                List<FolderDTO> list = new List<FolderDTO>();

                while (reader.Read())
                {
                    var dto = FillDTO(reader);
                    if (dto != null && dto.IsActive == true)
                    {
                        list.Add(dto);
                    }
                }

                return list;
            }
        }

        public static string GetParentName(int p)
        {
            string sqlQuery = String.Format("select name from dbo.folder where id={0}", p);
            using (DBHelper helper = new DBHelper())
            {
                return Convert.ToString(helper.ExecuteScalar(sqlQuery));
            }
        }
    }
}
