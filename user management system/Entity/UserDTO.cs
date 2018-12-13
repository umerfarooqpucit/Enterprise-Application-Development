using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity
{
    public class UserDTO
    {
        public int ID { get; set; }
        public String txtName { get; set; }
        public String txtLogin { get; set; }
        public String txtPassword { get; set; }
        public String txtEmail { get; set; }
        public char cmbGender { get; set; }
        public String imageName { get; set; }
        public String txtAddress { get; set; }
        public int txtAge { get; set; }
        public String txtNic { get; set; }
        public DateTime txtDob { get; set; }
        public bool isCricket { get; set; }
        public bool isHockey { get; set; }
        public bool isChess { get; set; }
    }
}
