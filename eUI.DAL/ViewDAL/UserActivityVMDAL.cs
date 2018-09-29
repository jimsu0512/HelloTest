using eUI.DAL.DBUtility;
using eUI.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL.ViewDAL
{
    public class UserActivityVMDAL : BaseRepository<UserActivityVM>
    {
        private static UserActivityVMDAL instance;
        private static readonly object locker = new object();
        public UserActivityVMDAL() { }
        public static UserActivityVMDAL Instance()
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new UserActivityVMDAL();
                    }
                }
            }
            return instance;
        }

        /// <summary>
        ///  多表分页查询
        /// </summary>
        /// <param name="LogSearchCriteria criteria">WHERE语句中的查询条件</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="string[] asc">需要用来排序的字段</param>
        /// <param name="string[] desc">需要用来排序的字段</param>
        /// <returns></returns>
        public Tuple<IEnumerable<UserActivityVM>, int> FindWithOffsetFetch(int pageIndex, int pageSize, string[] desc)
        {
            const string selectQuery = @" ;WITH _data AS (
                                            SELECT UA.UActivityId, UI.NickName,UA.UAActivityName,UA.UAIntroduce,UA.UAPrice
                                            ,UA.UAState,UA.UACreateTime,UA.UAUpdateTime FROM [dbo].[UserActivity] UA
                                            INNER JOIN [dbo].[UserInfo] UI
                                            ON UA.UAUserId=UI.Id 
                                            /**where**/
                                        ),
                                            _count AS (
                                                SELECT COUNT(1) AS TotalCount FROM _data
                                        )
                                        SELECT * FROM _data CROSS APPLY _count /**orderby**/ OFFSET @PageIndex * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";

            SqlBuilder builder = new SqlBuilder();

            var selector = builder.AddTemplate(selectQuery, new { PageIndex = pageIndex - 1, PageSize = pageSize });

            //if (!string.IsNullOrEmpty(criteria.Level))
            //    builder.Where("lv.Name = @Level", new { Level = criteria.Level });

            //if (!string.IsNullOrEmpty(criteria.Message))
            //{
            //    var msg = "%" + criteria.Message + "%";
            //    builder.Where("l.Message Like @Message", new { Message = msg });
            //}

            //foreach (var a in asc)
            //{
            //    if (!string.IsNullOrWhiteSpace(a))
            //        builder.OrderBy(a);
            //}

            foreach (var d in desc)
            {
                if (!string.IsNullOrWhiteSpace(d))
                    builder.OrderBy(d + " desc");
            }

            var rows = this.Query(selector.RawSql, selector.Parameters);

            if (rows.Count == 0)
                return new Tuple<IEnumerable<UserActivityVM>, int>(rows, 0);


            return new Tuple<IEnumerable<UserActivityVM>, int>(rows, rows[0].TotalCount);
        }
    }
}