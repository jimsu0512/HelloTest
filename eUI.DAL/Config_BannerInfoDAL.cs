using Dapper;
using eUI.DAL.DBUtility;
using eUI.Model;
using eUI.Model.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class Config_BannerInfoDAL : BaseRepository<Config_BannerInfo>
    {
        private static Config_BannerInfoDAL instance;
        private static readonly object locker = new object();
        public Config_BannerInfoDAL() { }
        public static Config_BannerInfoDAL Instance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new Config_BannerInfoDAL();
                    }
                }
            }
            return instance;
        }

        public List<Config_BannerInfo> GetConfig_BannerInfo()
        {
            return this.GetModelList().ToList();
        }

        public bool AddBannerInfo(Config_BannerInfo config_BannerInfo)
        {
            return this.Add(config_BannerInfo) > 0;
        }
        public bool UpdateBannerInfo(Config_BannerInfo bannerInfo)
        {
            string sql = string.Format("UPDATE [Config_BannerInfo] SET [CBannerName]='{0}',[CBIsValid]={1},[CBIsDel]={2} WHERE CBannerId={3}",
                bannerInfo.CBannerName, bannerInfo.CBIsValid, bannerInfo.CBIsDel,bannerInfo.CBannerId);
            return this.ExecuteSql(sql);
        }
        /// <summary>
        /// 采用存储过程分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageList<Config_BannerInfo> GetPageByProcList(int page = 1, int pageSize = 10)
        {
            PageList<Config_BannerInfo> model = new PageList<Config_BannerInfo>();
            var list = new List<Config_BannerInfo>();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("viewName", "Config_BannerInfo");
            parm.Add("fieldName", "*");
            parm.Add("keyName", "CBTypeId");
            parm.Add("pageSize", pageSize);
            parm.Add("pageNo", page);
            parm.Add("orderString", "CBannerId");
            parm.Add("recordTotal", 0, DbType.Int32, ParameterDirection.Output);
            list = this.GetAllList<Config_BannerInfo>("ProcViewPager", parm);
            int totalCount = parm.Get<int>("@recordTotal");//返回总页数
            model.rows = list;
            model.total = totalCount;
            return model;
        }


        public bool DeleteBannerInfo(int id)
        {
            string sql = string.Format("update [dbo].[Config_BannerInfo] set [CBIsValid]=0,[CBIsDel]=1 where [CBTypeId]={0}"
                , id);
            return this.ExecuteSql(sql);
        }

        public bool UpdateBannerImg(Config_BannerInfo bannerInfo)
        {
            string sql = string.Format("UPDATE [Config_BannerInfo] SET [CBannerUrl]='{0}' WHERE [CBannerId]={1}",
                bannerInfo.CBannerUrl, bannerInfo.CBannerId);
            return this.ExecuteSql(sql);
        }
    }
}
