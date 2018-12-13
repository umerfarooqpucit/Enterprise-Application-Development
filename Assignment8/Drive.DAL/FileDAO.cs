using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Drive.DAL
{
    public class FileDAO
    {
     
        public static List<FileDTO> GetAllFiles(int parentId,int user)
        {
            using (DBHelper helper = new DBHelper())
            {
                var query = String.Format("Select * from dbo.Files where ParentFolderId={0} and CreatedBy={1} and IsActive=1", parentId,user);
                var reader = helper.ExecuteReader(query);
                List<FileDTO> list = new List<FileDTO>();

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
        private static FileDTO FillDTO(SqlDataReader reader)
        {
            var dto = new FileDTO();
            dto.ID = reader.GetInt32(0);
            dto.Name = reader.GetString(1);
            dto.ParentFolderId = reader.GetInt32(2);
            dto.FileExt = reader.GetString(3);
            dto.FileSizeInKB = reader.GetInt32(4);
            dto.CreatedBy = reader.GetInt32(5);
            dto.UploadedOn = reader.GetDateTime(6);
            dto.IsActive = reader.GetBoolean(7);
            dto.UniqueName = reader.GetString(8);
            dto.ContentType = reader.GetString(9);
            return dto;
        }

        public static object GetFileByUniqueName(string uniqueName)
        {
            var query = String.Format("Select * from dbo.Files Where UniqueName='{0}'", uniqueName);

            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query);

                FileDTO dto = null;

                if (reader.Read())
                {
                    dto = FillDTO(reader);
                }

                return dto;
            }
        }

        public static int SaveFile(FileDTO dto)
        {
            string sqlQuery = String.Format("insert into dbo.files (name,parentfolderid,fileext,filesizeinkb,createdby,uploadedon,isactive,uniquename,contenttype) values('{0}',{1},'{2}',{3},{4},'{5}','{6}','{7}','{8}'); Select SCOPE_IDENTITY();", dto.Name, dto.ParentFolderId, dto.FileExt, dto.FileSizeInKB, dto.CreatedBy, dto.UploadedOn, dto.IsActive, dto.UniqueName, dto.ContentType);
            using (DBHelper helper = new DBHelper())
            {
                return Convert.ToInt32(helper.ExecuteScalar(sqlQuery));
            }
        }

        public static int DeleteFile(int Id)
        {
            String sqlQuery = String.Format("Update dbo.Files Set IsActive=0 Where id={0}", Id);

            using (DBHelper helper = new DBHelper())
            {
                return helper.ExecuteQuery(sqlQuery);
            }
        }

        public static List<FileDTO> GetAllFiles(int parent)
        {
            using (DBHelper helper = new DBHelper())
            {
                var query = String.Format("Select * from dbo.Files where ParentFolderId={0} and IsActive=1", parent);
                var reader = helper.ExecuteReader(query);
                List<FileDTO> list = new List<FileDTO>();

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
            string sqlQuery = String.Format("select name from dbo.folder where id={0}",p);
            using (DBHelper helper = new DBHelper())
            {
                return Convert.ToString(helper.ExecuteScalar(sqlQuery));
            }
        }

        public static List<FileDTO> Search(string fname, int userId)
        {
            using (DBHelper helper = new DBHelper())
            {
                var query = String.Format("Select * from dbo.Files where CreatedBy={1} and IsActive=1 and name like '{0}%'", fname, userId);
                var reader = helper.ExecuteReader(query);
                List<FileDTO> list = new List<FileDTO>();

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
    }
}
