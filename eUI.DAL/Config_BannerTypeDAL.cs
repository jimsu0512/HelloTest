using Dapper;
using eUI.DAL.DBUtility;
using eUI.Model;
using eUI.Model.ResponseModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace eUI.DAL
{
    public class Config_BannerTypeDAL : BaseRepository<Config_BannerType>
    {
        private static Config_BannerTypeDAL instance;
        private static readonly object locker = new object();
        public Config_BannerTypeDAL() { }
        public static Config_BannerTypeDAL Instance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new Config_BannerTypeDAL();
                    }
                }
            }
            return instance;
        }

        public List<Config_BannerType> GetConfig_BannerTypes()
        {
            return this.GetModelList().ToList();
        }

        public bool AddBannerType(Config_BannerType config_BannerType)
        {
            return this.Add(config_BannerType) > 0;
        }

        public bool UpdateBannerType(Config_BannerType bannerType)
        {
            return this.Update(bannerType);
        }

        /// <summary>
        /// 采用存储过程分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageList<Config_BannerType> GetPageByProcList(int page = 1, int pageSize = 10)
        {
            PageList<Config_BannerType> model = new PageList<Config_BannerType>();
            var list = new List<Config_BannerType>();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("viewName", "Config_BannerType");
            parm.Add("fieldName", "*");
            parm.Add("keyName", "CBTypeId");
            parm.Add("pageSize", pageSize);
            parm.Add("pageNo", page);
            parm.Add("orderString", "CBTypeId");
            parm.Add("recordTotal", 0, DbType.Int32, ParameterDirection.Output);
            list = this.GetAllList<Config_BannerType>("ProcViewPager", parm);
            int totalCount = parm.Get<int>("@recordTotal");//返回总页数
            model.rows = list;
            model.total = totalCount;
            return model;
        }

        public bool DeleteBannerType(int  id)
        {
            string sql = string.Format("update [dbo].[Config_BannerType] set [CBIsValid]=0,[CBIsDel]=1 where [CBTypeId]={0}"
                ,id);
            return this.ExecuteSql(sql);
        }

        public List<Config_BannerType> GetBannerType()
        {
            string sql = string.Format("SELECT [CBTypeId],[CBTypeName] FROM [eUI].[dbo].[Config_BannerType] WITH(NOLOCK) WHERE CBIsValid=1 AND CBIsDel=0");
            return this.Query(sql);
        }
    }
}
