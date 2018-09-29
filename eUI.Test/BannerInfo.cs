using eUI.BLL.ViewBLL;
using eUI.Model.ResponseModel;
using Newtonsoft.Json;
using System;

namespace eUI.Test
{
    public class BannerInfo
    {
        public static void SearchInfo()
        {
            var bannerInfoVM = BannerInfoVMBLL.SearchInfo(new PageParam { page = 1, rows = 2 });
            Console.WriteLine(JsonConvert.SerializeObject(bannerInfoVM));
        }
    }
}
