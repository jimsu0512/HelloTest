using easyUITest.Extensions;
using eUI.BLL;
using eUI.BLL.ViewBLL;
using eUI.Model;
using eUI.Model.ResponseModel;
using eUI.Model.ViewModel;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class BannerInfoController : Controller
    {
        [HttpPost]
        public ActionResult SearchInfo(PageParam bannerParams)
        {
            PageList<BannerInfoVM> bannerList = BannerInfoVMBLL.SearchInfo(bannerParams);
            return Json(bannerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddEdit(Config_BannerInfo param)
        {
            var result = Config_BannerInfoBLL.AddEdit(param);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
