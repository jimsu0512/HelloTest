using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL.DBUtility
{
    public class DBHelper
    {

        public static string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString();

        #region 执行sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExcuteNoQuerySql(string cmdText)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    //执行返回结果   
                    int iResult = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return iResult;
                }
                catch (Exception ex)
                {
                    //Response
                    return 0;
                }
            }
        }
        #endregion

        #region 查询数据库结果记录集
        /// <summary>
        /// 查询数据库结果记录集
        /// </summary>
        /// <param name="connStr">连接字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>返回DataTable</returns>
        public static DataTable SearchSql(string cmdText)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmdText, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds.Tables[0];
            }
        }
        #endregion

        #region 返回最大值
        /// <summary>
        /// 返回最大值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExcuteScalar(string sqlText)
        {
            int count = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                    cmd.Dispose();
                }

            }
            catch
            {
            }
            finally
            {
                //conn.Close();
                //conn.Dispose();
            }
            return count;
        }
        #endregion

        #region 返回首行首列
        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string ExcuteScalarFirstRow(string sqlText)
        {
            string content = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    content = cmd.ExecuteScalar().ToString();
                    cmd.Dispose();
                }

            }
            catch
            {
            }
            finally
            {
                //conn.Close();
                //conn.Dispose();
            }
            return content;
        }
        #endregion

        #region 通用存储过程查询方法
        /// <summary>
        /// 通用存储过程查询方法
        /// </summary>
        /// <param name="ProName">存储过程名称</param>
        /// <param name="pars">参数</param>
        /// <returns>返回DataTable数据集</returns>
        public static DataTable Pro_search(string ProName, SqlParameter[] pars)
        {
            DataTable dt = new DataTable();
            dt.TableName = "tb";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //创建命令对象
                SqlCommand cmd = new SqlCommand(ProName, conn);
                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1200;

                //conn.Open();
                //SqlDataReader rd = cmd.ExecuteReader();
                //dt.Load(rd);
                //rd.Close();
                //conn.Close();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;

        }
        #endregion

        #region 通用存储过程查询方法
        /// <summary>
        /// 通用存储过程查询方法
        /// </summary>
        /// <param name="ProName">存储过程名称</param>
        /// <param name="pars">参数</param>
        /// <returns>返回DataTable数据集</returns>
        public static DataSet Pro_searchDS(string ProName, SqlParameter[] pars)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //创建命令对象
                SqlCommand cmd = new SqlCommand(ProName, conn);
                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1200;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }

            return ds;

        }
        #endregion

        #region 通用存储过程返回首行首列
        /// <summary>
        /// 通用存储过程查询方法
        /// </summary>
        /// <param name="ProName">存储过程名称</param>
        /// <param name="pars">参数</param>
        /// <returns>返回DataTable数据集</returns>
        public static string Pro_ScalarFirstRow(string ProName, SqlParameter[] pars)
        {
            string content = "";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //创建命令对象
                SqlCommand cmd = new SqlCommand(ProName, conn);
                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }

                conn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                content = cmd.ExecuteScalar().ToString();
                cmd.Dispose();
            }
            return content;

        }
        #endregion

        #region 通用存储过程执行方法
        /// <summary>
        /// 通用存储过程执行方法
        /// </summary>
        /// <param name="ProName">存储过程名称</param>
        /// <param name="pars">参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int Pro_ExecSql(string ProName, SqlParameter[] pars)
        {
            int row = 0;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(ProName, conn);

                if (pars != null)
                {
                    cmd.Parameters.AddRange(pars);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                row = cmd.ExecuteNonQuery();
                conn.Close();
            }

            return row;

        }
        #endregion
    }
}
