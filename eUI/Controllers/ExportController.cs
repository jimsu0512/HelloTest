using eUI.BLL;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class ExportController : Controller
    {
        UserInfoBLL userInfoBLL = new UserInfoBLL();
        //
        // GET: /Export/

        public ActionResult Index()
        {
            return View();
        }

        public FileResult UserInfo(UserParams userInfo)
        {
            ExportExcelModel exportExcelModel = new ExportExcelModel();
            exportExcelModel=userInfoBLL.ExportExcel(userInfo);
            return File(Server.MapPath(exportExcelModel.filename), "application/ms-excel");
            }

    }
}
