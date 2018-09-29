using eUI.BLL;
using eUI.Model;
using eUI.Model.ResponseModel;
using Newtonsoft.Json;
using System;

namespace eUI.Test
{
    public class BannerType
    {
        public static void SearchInfo()
        {
            var result = Config_BannerTypeBLL.SearchInfo(new PageParam { page = 1, rows = 2 });
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }
        public static bool AddEdit (Config_BannerType bannerType)
        {
           return Config_BannerTypeBLL.AddEdit(bannerType);
        }
    }
}
