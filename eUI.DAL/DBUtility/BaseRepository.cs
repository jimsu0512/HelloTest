using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL.DBUtility
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        private readonly string sqlconnection =
                     "Data Source=.;Initial Catalog=eUI;User Id=sa;Password=123@abcd";
        private SqlConnection GetOpenConnection()
        {
            SqlConnection connection = new SqlConnection(sqlconnection);
            connection.Open();
            return connection;
        }
        private IDbConnection _connection;
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(T model)
        {
            long result;
            using (_connection = GetOpenConnection())
            {
                result = _connection.Insert<long>(model);
                return result;
            }
        }
        /// <summary>
        /// 根据ID删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            int? result;
            using (_connection = GetOpenConnection())
            {
                result = _connection.Delete<T>(id);
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 按条件删除数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool DeleteList(string strWhere, object parameters)
        {
            int? result;
            using (_connection = GetOpenConnection())
            {
                result = _connection.DeleteList<T>(strWhere, parameters);
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(T model)
        {
            int? result;
            using (_connection = GetOpenConnection())
            {
                result = _connection.Update(model);
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据ID获取实体对象
        /// </summary>
        public T GetModel(int id)
        {
            using (_connection = GetOpenConnection())
            {
                return _connection.Get<T>(id);
            }
        }

        /// <summary>
        /// 根据ID获取实体对象
        /// </summary>
        public T GetModel(long id)
        {
            using (_connection = GetOpenConnection())
            {
                return _connection.Get<T>(id);
            }
        }
        /// <summary>
        /// 获取实体对象列表
        /// </summary>
        public IEnumerable<T> GetModelList()
        {
            using (_connection = GetOpenConnection())
            {
                return _connection.GetList<T>();
            }
        }
        /// <summary>
        /// 根据条件获取实体对象集合
        /// </summary>
        public IEnumerable<T> GetModelList(object dynamic)
        {
            using (_connection = GetOpenConnection())
            {
                return _connection.GetList<T>(dynamic);
            }
        }
        //var user = connection.GetList<User>("where age = 10 or Name like '%Smith%'");
        //var user = connection.GetList<User>(new { Age = 10 });
        /// <summary>
        /// 根据条件分页获取实体对象集合
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="rowsNum"></param>
        /// <param name="strWhere"></param>
        /// <param name="orderBy"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<T> GetListPage(int pageNum, int rowsNum, string strWhere = "", string orderBy = "", object parameters = null)
        {
            using (_connection = GetOpenConnection())
            {
                return _connection.GetListPaged<T>(pageNum, rowsNum, strWhere, orderBy, parameters); ;
            }
        }
        //var user = connection.GetListPaged<User>(1, 10, "where age = 10 or Name like '%Smith%'", "Name desc");


        /// <summary>
        /// 通过存储过程分页
        /// </summary>
        /// <returns></returns>
        public List<U> GetAllList<U>(string sql, object parm)
        {
            var list = new List<U>();
            using (_connection = GetOpenConnection())
            {
                //标准写法
                list = _connection.Query<U>(sql, parm, commandType: CommandType.StoredProcedure).AsList();
                //dapper扩展写法 如果没有where语句 可以用此方法
                //list = _connection.GetList<T>().AsList();
            }
            return list;
        }
        public bool ExecuteSql(string sql, object param = null)
        {
            using (_connection = GetOpenConnection())
            {
                return _connection.Execute(sql, param)>0;
            }
        }
        public List<T> GetList(string sql)
        {
            using (_connection = GetOpenConnection())
            {
                return _connection.Query<T>(sql).AsList();
            }
        }
        public List<T> Query(string sql, object param = null)
        {
            using (_connection = GetOpenConnection())
            {
                return _connection.Query<T>(sql, param).AsList();
            }
        }
        #endregion
    }
}
