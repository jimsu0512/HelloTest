using eUI.DAL;
using eUI.Model;
using eUI.Model.ResponseModel;

namespace eUI.BLL
{
    public class UserActivityBLL
    {
        public static bool AddEdit(UserActivity userActivity)
        {
            if (userActivity.UActivityId > 0)
            {
                return UserActivityDAL.Instance().UpdateUserActivity(userActivity);
            }
            else
            {
                return UserActivityDAL.Instance().AddUserActivity(userActivity);
            }
        }

        public static bool Delete(int id)
        {
            return UserActivityDAL.Instance().Delete(id);
        }
    }
}