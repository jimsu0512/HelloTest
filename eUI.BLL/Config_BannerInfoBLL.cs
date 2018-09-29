using eUI.DAL;
using eUI.Model;
using eUI.Model.ResponseModel;

namespace eUI.BLL
{
    public class Config_BannerInfoBLL
    {
        public static PageList<Config_BannerInfo> SearchInfo(PageParam bannerParams)
        {
            return Config_BannerInfoDAL.Instance().GetPageByProcList(bannerParams.page, bannerParams.rows);
        }

        public static bool AddEdit(Config_BannerInfo bannerInfo)
        {
            if (bannerInfo.CBannerId > 0)
            {
                return Config_BannerInfoDAL.Instance().UpdateBannerInfo(new Config_BannerInfo { CBannerId = bannerInfo.CBannerId, CBannerName = bannerInfo.CBannerName, CBIsValid = bannerInfo.CBIsValid, CBIsDel = bannerInfo.CBIsDel });
            }
            else
            {
                return Config_BannerInfoDAL.Instance().AddBannerInfo(bannerInfo);
            }
        }

        public static bool Delete(int id)
        {
            return Config_BannerInfoDAL.Instance().Delete(id);
        }

        public static bool UpdateBannerImg(Config_BannerInfo bannerInfo)
        {
            return Config_BannerInfoDAL.Instance().UpdateBannerImg(bannerInfo);
        }
    }
}
