using Dapper;
using eUI.DAL.DBUtility;
using eUI.Model;
using eUI.Model.ResponseModel;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class UserActivityDAL : BaseRepository<UserActivity>
    {

        private static UserActivityDAL instance;
        private static readonly object locker = new object();
        public UserActivityDAL() { }
        public static UserActivityDAL Instance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new UserActivityDAL();
                    }
                }
            }
            return instance;
        }

        public List<UserActivity> GetUserActivity()
        {
            return this.GetModelList().ToList();
        }

        public bool AddUserActivity(UserActivity userActivity)
        {
            return this.Add(userActivity) > 0;
        }

        public bool UpdateUserActivity(UserActivity userActivity)
        {
            //string sql = string.Format("update [dbo].[Config_BannerType] set [CBTypeName]='{0}',[CBIsValid]={1},[CBIsDel]={2} where [CBTypeId]={3}"
            //    , bannerType.CBTypeName, bannerType.CBIsValid, bannerType.CBIsDel, bannerType.CBTypeId);
            //return this.ExecuteSql(sql);
            return true;
        }

        public bool DeleteUserActivity(int id)
        {
            string sql = string.Format("update [dbo].[UserActivity] set [UAIsValid]=0,[UAIsDel]=1 where [UActivityId]={0}"
                , id);
            return this.ExecuteSql(sql);
        }
    }
}
