using eUI.DAL.DBUtility;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.DAL
{
    public class UserInfoDAL
    {
        //用户信息(表格显示)
        public DataTable SearchInfo(UserParams userInfo)
        {
            //如果你的数据库是sqlserver 2012以上,也可以使用getPage2012方法
            DataTable dtUserInfo = DBHelper.SearchSql(getPage2005(userInfo));

            return dtUserInfo;
        }

        //添加用户
        public bool Add(UserInfo userInfo)
        {

            string sbAddUser = string.Format(
            " INSERT INTO [dbo].[UserInfo]([Account],[Pwd],[Tel],[NickName],[Gender],[Birthday],[PjCount]) " +
            " VALUES('{0}','{1}','{2}','{3}',{4},'{5}',{6})",
            userInfo.Account, userInfo.Pwd, userInfo.Tel, userInfo.NickName, userInfo.Gender, userInfo.Birthday, userInfo.PjCount);

            int iResult = DBHelper.ExcuteNoQuerySql(sbAddUser.ToString());
            if (iResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //修改用户
        public bool Edit(UserInfo userInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            sbAddUser.Append("Update dbo.UserInfo Set Pwd ='" + userInfo.Pwd + "', ");
            sbAddUser.Append("Tel='" + userInfo.Tel + "',");            
            sbAddUser.Append("NickName='" + userInfo.NickName + "',");            
            sbAddUser.Append("Gender=" + userInfo.Gender + ",");
            sbAddUser.Append("Birthday='" + userInfo.Birthday + "',");
            sbAddUser.Append("PjCount='" + userInfo.PjCount + "',");            
            sbAddUser.Append("UserIsValid=" + userInfo.UserIsValid + ",");
            sbAddUser.Append("UserIsDel=" + userInfo.UserIsDel + " ");
            sbAddUser.AppendLine("where Id=" + userInfo.Id);
            int iResult = DBHelper.ExcuteNoQuerySql(sbAddUser.ToString());
            if (iResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //删除用户
        public bool Del(UserParams userInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            sbAddUser.Append("UPDATE [dbo].[UserInfo] SET UserIsValid=0,UserIsDel=1,UpdateTime='" + DateTime.Now + "' WHERE Id=" + userInfo.id);

            int iResult = DBHelper.ExcuteNoQuerySql(sbAddUser.ToString());
            if (iResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //修改权限
        public bool UpdatePermission(PermissionIDListModel permissionIDListModel)
        {
            StringBuilder sbUpdatePermission = new StringBuilder();

            //先删除所有权限
            sbUpdatePermission.AppendLine("Delete from dbo.UserPermission Where UserID = " + permissionIDListModel.UserId);
            sbUpdatePermission.AppendLine("Insert into dbo.UserPermission(UserID,ActionID)");
            sbUpdatePermission.Append("Values");

            foreach (PermissionActionID actionIDlist in permissionIDListModel.ActionIDList)
            {
                if (actionIDlist.ActionID == 0)
                {
                    continue;
                }
                sbUpdatePermission.Append("(");
                sbUpdatePermission.Append(permissionIDListModel.UserId + ",");
                sbUpdatePermission.Append(actionIDlist.ActionID);
                sbUpdatePermission.Append("),");
            }
            //移除最后一个字符','
            string permissionSql = sbUpdatePermission.ToString().Substring(0, sbUpdatePermission.ToString().Length - 1);
            int iResult = DBHelper.ExcuteNoQuerySql(permissionSql);
            if (iResult >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //获取用户权限
        public DataTable GetUserPermission(PermissionIDListModel permissionIDListModel)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendLine("Select ActionID From dbo.UserPermission With(Nolock)");
            sbSI.AppendLine("Where UserID =" + permissionIDListModel.UserId);

            DataTable dtUserInfo = DBHelper.SearchSql(sbSI.ToString());

            return dtUserInfo;
        }

        //根据用户ID获取用户信息
        public DataTable GetUserInfo(UserParams userInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendLine("Select Cast(Gender as char(1)) Gender,Convert(varchar(10), Birthday, 120) Birthday,Account,Pwd,Tel,NickName,PjCount from dbo.UserInfo With(Nolock)");
            sbSI.AppendLine("Where Id =" + userInfo.id);

            DataTable dtUserInfo = DBHelper.SearchSql(sbSI.ToString());

            return dtUserInfo;
        }

        //更新用户头像图片路径
        public bool UpdatePic(UserInfo userInfo)
        {
            StringBuilder sbAddUser = new StringBuilder();
            sbAddUser.Append("Update dbo.UserInfo Set UpdateTime='"+DateTime.Now+"', PicUrl ='" + userInfo.PicUrl + "' ");
            sbAddUser.AppendLine("Where Id=" + userInfo.Id);

            int iResult = DBHelper.ExcuteNoQuerySql(sbAddUser.ToString());
            if (iResult == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //获取用户总数(按性别分组)
        public DataTable GetUserGenderGroup()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendLine("Select Gender,Count(Gender) GenderCount From dbo.UserInfo With(Nolock) Group By Gender");

            DataTable dtUserGender = DBHelper.SearchSql(sbSI.ToString());

            return dtUserGender;
        }

        //获取用户总数(按天分组),显示最近7天数据
        public DataTable GetUserDateGroup()
        {
            StringBuilder sbSI = new StringBuilder();
            sbSI.AppendLine("Select Top 7 Convert(varchar(10), CreateTime, 120) CreateTime,");
	        sbSI.AppendLine("Count(*) CTCount From dbo.UserInfo With(Nolock)");
            sbSI.AppendLine("Group by  Convert(varchar(10), CreateTime, 120)");
            sbSI.AppendLine("Order by CreateTime Desc");

            DataTable dtUserGender = DBHelper.SearchSql(sbSI.ToString());

            return dtUserGender;
        }

        #region Sql Server2005及以上的分页
        // <summary>
        /// Sql Server2005及以上的分页
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>数据库查询语句</returns>
        private string getPage2005(UserParams userInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            //SqlServer2005及以上分页方式
            if (!userInfo.isExport)
            {
                sbSI.AppendLine("Declare @PageSize int, @PageIndex int  ");
                sbSI.AppendLine("Set @PageSize = " + userInfo.rows);
                sbSI.AppendLine("Set @PageIndex = " + userInfo.page);
                sbSI.AppendLine("Select Convert(varchar(10),Birthday,120) Birthday,Convert(varchar(10),CreateTime,120) CreateTime,Convert(varchar(10),UpdateTime,120) UpdateTime" +
                    ", Id, Account, Pwd, Tel, NickName, PicUrl, Gender, PjCount, UserIsValid, UserIsDel,Total  From ( ");
            }
            sbSI.Append("Select ");
            if (!userInfo.isExport)
            {
                sbSI.AppendLine("Row_Number() over (order by CreateTime Desc) RowNum,");
            }
            sbSI.AppendLine("Cast(Gender as char(1)) Gender,Id,Account,Pwd,Tel,NickName,PicUrl,Birthday,PjCount,UserIsValid,UserIsDel,CreateTime,UpdateTime");
            if (!userInfo.isExport)
            {
                sbSI.AppendLine(",COUNT(*) OVER(PARTITION BY '') AS Total ");
            }
            sbSI.AppendLine("From [dbo].UserInfo With(Nolock)");

            if (!userInfo.isExport)
            {
                sbSI.AppendLine(") A ");
            }
            sbSI.AppendLine("Where 1=1");
            if (!userInfo.isExport)
            {
                sbSI.AppendLine("And  A.RowNum between (((@PageIndex-1)*@PageSize)+1) and (@PageIndex*@PageSize)");
            }
            sbSI.AppendLine("And Account like '%" + userInfo.name + "%'");
            //添加创建日期查询
            if (userInfo.stTime.Year != 1 && userInfo.edTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime Between '" + userInfo.stTime + "' And '" + userInfo.edTime.AddDays(1).AddSeconds(-1) + "'");
            }
            else if (userInfo.stTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime >= '" + userInfo.stTime + "'");
            }
            else if (userInfo.edTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime <= '" + userInfo.edTime.AddDays(1).AddSeconds(-1) + "' ");
            }
            sbSI.AppendLine("Order by Id Desc");

            return sbSI.ToString();
        }
        #endregion

        #region Sql Server2012及以上的分页
        /// <summary>
        /// Sql Server2012及以上的分页
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>数据库查询语句</returns>
        private string getPage2012(UserParams userInfo)
        {
            StringBuilder sbSI = new StringBuilder();
            //SqlServer 2012及以上分页方式
            //如果是导出Excel，则不需要分页
            if (!userInfo.isExport)
            {
                sbSI.AppendLine("Declare @PageSize int, @PageIndex int  ");
                sbSI.AppendLine("Set @PageSize = " + userInfo.rows);
                sbSI.AppendLine("Set @PageIndex = " + userInfo.page);
            }
            sbSI.AppendLine("Select Id,Name,Tel,Address,Pwd,Email, Cast(Gender as char(1)) Gender,");
            sbSI.AppendLine("Convert(varchar(10),CreateTime,120) CreateTime,PicUrl");
            if (!userInfo.isExport)
            {
                sbSI.AppendLine(",COUNT(*) OVER(PARTITION BY '') AS Total ");
            }
            sbSI.AppendLine("From dbo.UserInfo With(Nolock)");
            sbSI.AppendLine("Where Name like '%" + userInfo.name + "%'");
            //添加创建日期查询
            if (userInfo.stTime.Year != 1 && userInfo.edTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime Between '" + userInfo.stTime + "' And '" + userInfo.edTime.AddDays(1).AddSeconds(-1) + "'");
            }
            else if (userInfo.stTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime >= '" + userInfo.stTime + "'");
            }
            else if (userInfo.edTime.Year != 1)
            {
                sbSI.AppendLine("And CreateTime <= '" + userInfo.edTime.AddDays(1).AddSeconds(-1) + "' ");
            }

            sbSI.AppendLine("Order by Id Desc");
            if (!userInfo.isExport)
            {
                sbSI.AppendLine("OFFSET (@PageIndex-1)*@PageSize Rows  ");
                sbSI.AppendLine("FETCH NEXT @PageSize ROWS ONLY; ");
            }

            return sbSI.ToString();
        }
        #endregion
    }
}
