using easyUITest.Extensions;
using eUI.BLL;
using eUI.Model;
using eUI.Model.ResponseModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class BannerTypeController : Controller
    {
        [HttpPost]
        public ActionResult SearchInfo(PageParam bannerParams)
        {
            PageList<Config_BannerType> bannerList = Config_BannerTypeBLL.SearchInfo(bannerParams);
            return Json(bannerList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddEdit(Config_BannerType param)
        {
            var result = Config_BannerTypeBLL.AddEdit(param);
            return this.JObjectResult(result, null);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = Config_BannerTypeBLL.Delete(id);
            return this.JObjectResult(result, null);
        }

        [HttpPost]
        public ActionResult GetBannerList()
        {
            List<Config_BannerType> bannerList = Config_BannerTypeBLL.GetBannerType();
            return Json(bannerList, JsonRequestBehavior.AllowGet);
        }
    }
}
