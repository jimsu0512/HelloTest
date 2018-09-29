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
    public class Config_ServiceTypeDAL : BaseRepository<Config_ServiceType>
    {
        private static Config_ServiceTypeDAL instance;
        private static readonly object locker = new object();
        public Config_ServiceTypeDAL() { }
        public static Config_ServiceTypeDAL Instance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new Config_ServiceTypeDAL();
                    }
                }
            }
            return instance;
        }

        public List<Config_ServiceType> GetConfig_ServiceType()
        {
            return this.GetModelList().ToList();
        }

        public bool AddServiceType(Config_ServiceType config_ServiceType)
        {
            return this.Add(config_ServiceType) > 0;
        }

        public bool UpdateServiceType(Config_ServiceType serviceType)
        {
            //string sql = string.Format("update [dbo].[Config_BannerType] set [CBTypeName]='{0}',[CBIsValid]={1},[CBIsDel]={2} where [CBTypeId]={3}"
            //    , bannerType.CBTypeName, bannerType.CBIsValid, bannerType.CBIsDel, bannerType.CBTypeId);
            //return this.ExecuteSql(sql);
            return true;
        }

        /// <summary>
        /// 采用存储过程分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageList<Config_ServiceTypeSearch> GetPageByProcList(int page = 1, int pageSize = 10)
        {
            PageList<Config_ServiceTypeSearch> model = new PageList<Config_ServiceTypeSearch>();
            var list = new List<Config_ServiceTypeSearch>();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("viewName", "Config_ServiceType");
            parm.Add("fieldName", "*");
            parm.Add("keyName", "CSTTypeId");
            parm.Add("pageSize", pageSize);
            parm.Add("pageNo", page);
            parm.Add("orderString", "CSTTypeId");
            parm.Add("recordTotal", 0, DbType.Int32, ParameterDirection.Output);
            list = this.GetAllList<Config_ServiceTypeSearch>("ProcViewPager", parm);
            int totalCount = parm.Get<int>("@recordTotal");//返回总页数
            model.rows = list;
            model.total = totalCount;
            return model;
        }

        public bool DeleteServiceType(int id)
        {
            string sql = string.Format("update [dbo].[Config_ServiceType] set [CBIsValid]=0,[CBIsDel]=1 where [CBTypeId]={0}"
                , id);
            return this.ExecuteSql(sql);
        }
    }
}