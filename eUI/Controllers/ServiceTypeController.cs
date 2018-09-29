using easyUITest.Extensions;
using eUI.BLL;
using eUI.Model;
using eUI.Model.ResponseModel;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    public class ServiceTypeController : Controller
    {
        [HttpPost]
        public ActionResult SearchInfo(PageParam bannerParams)
        {
            PageList<Config_ServiceTypeSearch> bannerList = Config_ServiceTypeBLL.SearchInfo(bannerParams);
            return Json(bannerList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddEdit(Config_ServiceType param)
        {
            var result = Config_ServiceTypeBLL.AddEdit(param);
            return this.JObjectResult(result, null);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = Config_ServiceTypeBLL.Delete(id);
            return this.JObjectResult(result, null);
        }
    }
}
