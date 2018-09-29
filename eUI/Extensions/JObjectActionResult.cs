using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace easyUITest.Extensions
{
    /// <summary>
    /// 自定义JObject返回结果
    /// </summary>
    public class JObjectActionResult : JsonResult
    {
        /// <summary>
        /// 结果集
        /// </summary>
        public BaseResponse JObject
        {
            get;
            set;
        }

        public Encoding ContentEncoding
        {
            get;
            set;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;            
            if (JObject != null)
            {
                response.Write(JsonConvert.SerializeObject(JObject));
            }
        }
    }
}