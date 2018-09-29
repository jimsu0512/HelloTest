using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace easyUITest.Extensions
{
    public static class JObjectActionResultExtensions
    {
        public static JObjectActionResult JObjectResult(this Controller controller, BaseResponse obj)
        {
            return new JObjectActionResult { JObject = obj };
        }

        public static JObjectActionResult JObjectResult(this Controller controller, bool success, object data)
        {
            BaseResponse obj = new BaseResponse
            {
                success = success
            };
            if (success)
            {
                obj.code = 200;
                obj.msg = "Success";
            }
            else
            {
                obj.code = 500;
                obj.msg = "Error";
            }
            obj.data = data;
            return JObjectResult(controller, obj);
        }

        public static JObjectActionResult JObjectResult(this Controller controller, bool success, int code, string msg)
        {
            BaseResponse obj = new BaseResponse();
            obj.success = success;
            obj.code = code;
            obj.msg = msg;
            return JObjectResult(controller, obj);
        }
    }
}