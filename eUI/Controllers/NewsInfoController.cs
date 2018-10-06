using System.Web.Mvc;
using easyUITest.Extensions;
using eUI.BLL;
using eUI.Model;
using eUI.Model.ResponseModel;

namespace easyUITest.Controllers
{
    public class NewsInfoController : Controller
    {
        [HttpPost]
        public ActionResult SearchInfo(PageParam bannerParams)
        {
            PageList<N_NewsInfo> bannerList = N_NewsInfoBLL.SearchInfo(bannerParams);
            return Json(bannerList, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult AddEdit(N_NewsInfo param)
        {
            var result = N_NewsInfoBLL.AddEdit(param);
            return this.JObjectResult(result, null);
        }


        [HttpPost]
        public ActionResult GetModelById(long  nid)
        {
            var result = N_NewsInfoBLL.GetNewsInfo(nid);
            return Json(result, JsonRequestBehavior.DenyGet);
        }



        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = N_NewsInfoBLL.Delete(id);
            return this.JObjectResult(result, null);
        }
    }
}
