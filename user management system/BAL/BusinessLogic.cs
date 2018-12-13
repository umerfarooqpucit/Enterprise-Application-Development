using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model1;
using DAL;

namespace BAL
{
    public class BusinessLogic
    {
        public int save(UserDTO dto)
        {
            DBHandler dal = new DBHandler();
            return dal.save(dto);
        }
    }
}
