
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using eUI.Common;

namespace easyUITest.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //return View(WebFiles.eUIIndex);
            return View();
        }

        public ActionResult UserInfo()
        {
            return View(WebFiles.UserInfoIndex);
        }

        public ActionResult ProductInfo()
        {
            return View(WebFiles.ProductIndex);
        }

        public ActionResult UserReport()
        {
            return View(WebFiles.UserReport);
        }

        public ActionResult UserColumnReport()
        {
            return View(WebFiles.UserColumnReport);
        }

        public ActionResult UserLineReport()
        {
            return View(WebFiles.UserLineReport);
        }

        public ActionResult UserBoard()
        {
            return View(WebFiles.UserBoard);
        }

        public ActionResult WxPage()
        {
            return View(WebFiles.WxPage);
        }

        public ActionResult BannerType()
        {
            return View(WebFiles.BannerTypeIndex);
        }

        public ActionResult BannerInfo()
        {
            return View(WebFiles.BannerInfo);
        }
        public ActionResult ServiceType()
        {
            return View(WebFiles.ServiceType);
        }

        public ActionResult UserActivity()
        {
            return View(WebFiles.UserActivity);
        }

    }
}
