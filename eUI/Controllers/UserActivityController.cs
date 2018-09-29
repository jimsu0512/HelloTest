using easyUITest.Extensions;
using eUI.BLL;
using eUI.BLL.ViewBLL;
using eUI.Model;
using eUI.Model.ResponseModel;
using eUI.Model.ViewModel;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class UserActivityController : Controller
    {
        [HttpPost]
        public ActionResult SearchInfo(PageParam bannerParams)
        {
            PageList<UserActivityVM> bannerList = UserActivityVMBLL.SearchInfo(bannerParams);
            return Json(bannerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddEdit(UserActivity param)
        {
            var result = UserActivityBLL.AddEdit(param);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
