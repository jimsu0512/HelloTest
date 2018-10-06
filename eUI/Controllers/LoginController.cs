
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using eUI.Common;
using eUI.Model.ViewModel;

namespace easyUITest.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    
                        RedirectToAction("Index", "Home");
                    
                  
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View("Error");
        }

        /// <summary>
        /// 生产验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidateCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);//生成验证码
            Session["validateCode"] = code;//存储于Session中
            byte[] buffer = validateCode.CreateValidateGraphic(code);//创建验证码的图片
            return File(buffer, "image/jpeg");
        }

    }
}
