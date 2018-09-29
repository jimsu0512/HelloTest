using eUI.DAL.ViewDAL;
using eUI.Model.ResponseModel;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.BLL.ViewBLL
{
    public class BannerInfoVMBLL
    {
        public static PageList<BannerInfoVM> SearchInfo(PageParam pageParam)
        {
            string[] desc = { "CBannerId" };
            var bannerInfoVM = BannerInfoVMDAL.Instance().FindWithOffsetFetch(pageParam.page, pageParam.rows, desc);
            PageList<BannerInfoVM> model = new PageList<BannerInfoVM>
            {
                rows = bannerInfoVM.Item1.ToList(),
                total = bannerInfoVM.Item2
            };
            return model;
        }
    }
}
