using eUI.DAL;
using eUI.Model;
using eUI.Model.ResponseModel;

namespace eUI.BLL
{
    public class Config_ServiceTypeBLL
    {
        public static PageList<Config_ServiceTypeSearch> SearchInfo(PageParam bannerParams)
        {
            return Config_ServiceTypeDAL.Instance().GetPageByProcList(bannerParams.page, bannerParams.rows);
        }

        public static bool AddEdit(Config_ServiceType serviceType)
        {
            if (serviceType.CSTTypeId > 0)
            {
                return Config_ServiceTypeDAL.Instance().UpdateServiceType(serviceType);
            }
            else
            {
                return Config_ServiceTypeDAL.Instance().AddServiceType(serviceType);
            }
        }

        public static bool Delete(int id)
        {
            return Config_ServiceTypeDAL.Instance().Delete(id);
        }
    }
}