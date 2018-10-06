using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUI.DAL;
using eUI.Model;
using eUI.Model.ResponseModel;

namespace eUI.BLL
{
    public class N_NewsInfoBLL
    {
        public static PageList<N_NewsInfo> SearchInfo(PageParam bannerParams)
        {
            return N_NewsInfoDAL.Instance().GetPageByProcList(bannerParams.page, bannerParams.rows);
        }

        public static N_NewsInfo GetNewsInfo(long nid)
        {
            return N_NewsInfoDAL.Instance().GetNewsInfo(nid);
        }
        public static bool AddEdit(N_NewsInfo newsInfo)
        {
            if (newsInfo.NId > 0)
            {
                return N_NewsInfoDAL.Instance().UpdateNewInfo(newsInfo);
            }
            else
            {
                return N_NewsInfoDAL.Instance().AddNewsInfo(newsInfo);
            }
        }

        public static bool Delete(int id)
        {
            return N_NewsInfoDAL.Instance().DeleteNewInfo(id);
        }
    }
}
