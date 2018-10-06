using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using eUI.DAL.DBUtility;
using eUI.Model;
using eUI.Model.ResponseModel;

namespace eUI.DAL
{
    public class N_NewsInfoDAL : BaseRepository<N_NewsInfo>
    {
        private static N_NewsInfoDAL instance;
        private static readonly object locker = new object();
        public N_NewsInfoDAL() { }
        public static N_NewsInfoDAL Instance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new N_NewsInfoDAL();
                    }
                }
            }
            return instance;
        }

        public N_NewsInfo GetNewsInfo(long nid)
        {
            return this.GetModel(nid);
        }

        public bool AddNewsInfo(N_NewsInfo newsInfo)
        {
            return this.Add(newsInfo) > 0;
        }

        public bool UpdateNewInfo(N_NewsInfo newsInfo)
        {
            return this.Update(newsInfo);
        }

        /// <summary>
        /// 采用存储过程分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageList<N_NewsInfo> GetPageByProcList(int page = 1, int pageSize = 10)
        {
            PageList<N_NewsInfo> model = new PageList<N_NewsInfo>();
            var list = new List<N_NewsInfo>();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("viewName", "N_NewsInfo");
            parm.Add("fieldName", "*");
            parm.Add("keyName", "NId");
            parm.Add("pageSize", pageSize);
            parm.Add("pageNo", page);
            parm.Add("orderString", "NId");
            parm.Add("recordTotal", 0, DbType.Int32, ParameterDirection.Output);
            list = this.GetAllList<N_NewsInfo>("ProcViewPager", parm);
            int totalCount = parm.Get<int>("@recordTotal");//返回总页数
            model.rows = list;
            model.total = totalCount;
            return model;
        }

        public bool DeleteNewInfo(int id)
        {
            string sql = string.Format("update [dbo].[N_NewsInfo] set [NIsValid]=0 where [NId]={0}"
                , id);
            return this.ExecuteSql(sql);
        }

    }
}
