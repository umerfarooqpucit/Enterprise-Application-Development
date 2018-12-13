using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.Entities
{
    public class FileDTO
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String UniqueName { get; set; }
        public int ParentFolderId { get; set; }
        public int CreatedBy { get; set; }
        public string ContentType { get; set; }
        public string FileExt { get; set; }
        public int FileSizeInKB { get; set; }
        public DateTime UploadedOn { get; set; }
        public Boolean IsActive { get; set; }
    }
}
