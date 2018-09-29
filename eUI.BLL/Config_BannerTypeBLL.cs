using eUI.DAL;
using eUI.Model;
using eUI.Model.ResponseModel;
using System.Collections.Generic;

namespace eUI.BLL
{
    public class Config_BannerTypeBLL
    {
        public static PageList<Config_BannerType> SearchInfo(PageParam bannerParams)
        {
           return Config_BannerTypeDAL.Instance().GetPageByProcList(bannerParams.page, bannerParams.rows);
        }

        public static List<Config_BannerType> GetBannerType()
        {
            return Config_BannerTypeDAL.Instance().GetBannerType();
        }
        public static bool AddEdit(Config_BannerType bannerType)
        {
            if (bannerType.CBTypeId > 0)
            {
                return Config_BannerTypeDAL.Instance().UpdateBannerType(bannerType);
            }
            else
            {
                return Config_BannerTypeDAL.Instance().AddBannerType(bannerType);
            }
        }

        public static bool Delete(int id)
        {
            return Config_BannerTypeDAL.Instance().DeleteBannerType(id);
        }
    }
}
