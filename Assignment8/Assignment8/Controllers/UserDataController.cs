using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment8.Controllers
{
    public class UserDataController : ApiController
    {
        [HttpGet]
        public int GetValue()
        {
            return 5;
        }
        [HttpGet]
        public int AddNumbers(int a,int b)
        {
            return a + b;
        }
    }
}