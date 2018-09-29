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
    public class UserActivityVMBLL
    {
        public static PageList<UserActivityVM> SearchInfo(PageParam pageParam)
        {
            string[] desc = { "UActivityId " };
            var userActivityVM = UserActivityVMDAL.Instance().FindWithOffsetFetch(pageParam.page, pageParam.rows, desc);
            PageList<UserActivityVM> model = new PageList<UserActivityVM>
            {
                rows = userActivityVM.Item1.ToList(),
                total = userActivityVM.Item2
            };
            return model;
        }
    }
}
