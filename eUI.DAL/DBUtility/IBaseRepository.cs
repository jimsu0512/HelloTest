using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL.DBUtility
{
    public interface IBaseRepository<T>
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        long Add(T model);
        /// <summary>
        /// 根据ID删除一条数据
        /// </summary>
        bool Delete(int Id);
        /// <summary>
        /// 根据条件删除数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        bool DeleteList(string strWhere, object parameters);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(T model);
        /// <summary>
        /// 根据ID获取实体对象
        /// </summary>
        T GetModel(int Id);
        ///<summary>
        /// 获取实体列表
        /// </summary>
        IEnumerable<T> GetModelList();       
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageNum">页码</param>
        /// <param name="rowsNum">每页行数</param>
        /// <param name="strWhere">where条件</param>
        /// <param name="orderBy">Orde by排序</param>
        /// <param name="parameters">parameters参数</param>
        /// <returns></returns>
        IEnumerable<T> GetListPage(int pageNum, int rowsNum, string strWhere, string orderBy, object parameters);
        #endregion
    }
}
