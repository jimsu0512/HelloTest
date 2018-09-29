using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace easyUITest.Extensions
{
    public class BaseResponse
    {
        public bool success { get; set; }
        public int code { get; set; }
        public string msg { get; set; }

        public object data { get; set; }
    }
}