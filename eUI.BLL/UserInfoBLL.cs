using eUI.DAL;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUI.Common;

namespace eUI.BLL
{
    public class UserInfoBLL
    {
        UserInfoDAL userInfoDAL = new UserInfoDAL();

        //用户信息(表格显示)
        public UserInfoList SearchInfo(UserParams userInfo)
        {
            UserInfoList userInfoList = new UserInfoList();
            userInfoList.rows = new List<UserInfo>();

            DataTable dtUerInfo = userInfoDAL.SearchInfo(userInfo);

            //总记录数
            if (dtUerInfo.Rows.Count > 0)
            {
                userInfoList.total = Convert.ToInt32(dtUerInfo.Rows[0]["Total"]);
            }
            //将DT转换成对象并返回
            userInfoList.rows = dtUerInfo.toList<UserInfo>();
            return userInfoList;
        }

        //添加、修改用户
        public bool AddEdit(UserInfo userInfo)
        {
            bool iResult = false;
            if (userInfo.Id > 0)
            {
                iResult = userInfoDAL.Edit(userInfo);
            }
            else
            {
                iResult = userInfoDAL.Add(userInfo);                 
            }

            return iResult;
        }

        //删除用户
        public bool Del(UserParams userInfo)
        {
            bool iResult = iResult = userInfoDAL.Del(userInfo);

            return iResult;
        }

        //修改权限
        public PermissionSetStatus UpdatePermission(PermissionIDListModel permissionIDListModel)
        {
            PermissionSetStatus permissionSetStatus = new PermissionSetStatus();

            bool iResult = userInfoDAL.UpdatePermission(permissionIDListModel);
            if (iResult)
            {
                permissionSetStatus.IsSucceed = true;
            }
            else
            {
                permissionSetStatus.IsSucceed = false;
            }

            return permissionSetStatus;
        }

        //获取用户权限
        public List<PermissionActionID> GetUserPermission(PermissionIDListModel permissionIDListModel)
        {
            List<PermissionActionID> permissionActionList = new List<PermissionActionID>();

            DataTable dtUserPermission = userInfoDAL.GetUserPermission(permissionIDListModel);

            permissionActionList = dtUserPermission.toList<PermissionActionID>();

            return permissionActionList;
        }

        //根据用户ID获取用户信息
        public UserInfo GetUserInfo(UserParams userInfo)
        {
            DataTable dtUserInfo = userInfoDAL.GetUserInfo(userInfo);

            //序列化
            return dtUserInfo.Rows[0].rowToList<UserInfo>();
        }

        //导出Excel
        public ExportExcelModel ExportExcel(UserParams userInfo)
        {
            ExportExcelModel exportExcelModel = new ExportExcelModel();

            DataTable dtUerInfo = userInfoDAL.SearchInfo(userInfo);
            exportExcelModel.filename = OperateExcel.ExportToExcel(dtUerInfo, DateTime.Now.ToString("yyyyMMddHHmmss"));

            return exportExcelModel;
        }

        //更新用户头像图片路径
        public bool UpdatePic(UserInfo userInfo)
        {
            return userInfoDAL.UpdatePic(userInfo);
        }

        //用户报表(饼图)
        public UserReportModel GetUserReport()
        {
            UserReportModel userReportModel = new UserReportModel();

            DataTable dtList = userInfoDAL.GetUserGenderGroup();

            userReportModel.title = new TitleAttribute() { text = "用户性别比例", x = "center" };
            userReportModel.tooltip = new TooltipAttribute() { formatter = "{b} : {c} ({d}%)" };
            userReportModel.legend = new LegendAttribute() { data = new List<string> { "男", "女" }, left = "left", orient = "vertical" };
            userReportModel.series = new List<SeriesAttribute>() { 
                new SeriesAttribute(){  
                    type="pie",  
                    data= new List<SeriesDataAttribute>(){  
                        new SeriesDataAttribute(){ 
                            value= (dtList.Select("gender = 0").Count()>0)?Convert.ToInt32(dtList.Select("gender = 0")[0]["GenderCount"]):0, 
                            name="女"},
                        new SeriesDataAttribute(){ 
                            value=(dtList.Select("gender = 1").Count()>0)?Convert.ToInt32(dtList.Select("gender = 1")[0]["GenderCount"]):0, 
                            name="男"}
                    }}
            };

            return userReportModel;
        }

        //用户报表(柱状)
        public UserColumnReportModel GetUserColumnReport()
        {
            UserColumnReportModel userColumnReportModel = new UserColumnReportModel();

            DataTable dtList = userInfoDAL.GetUserDateGroup();
            //排序
            DataTable dtCopy = dtList.Copy();
            DataView dv = dtList.DefaultView;
            dv.Sort = "CreateTime Asc";
            dtCopy = dv.ToTable();

            List<string> xAxisData = new List<string>();
            List<string> seriesData = new List<string>();

            foreach (DataRow row in dtCopy.Rows)
            {
                xAxisData.Add(row["CreateTime"].ToString());
                seriesData.Add(row["CTCount"].ToString());
            }

            userColumnReportModel.color = new string[] { "#3398DB" };
            userColumnReportModel.tooltip = new TooltipCAttribute()
            {
                axisPointer = new AxisColumnPointer() { type = "shadow" },
                trigger = "axis"
            };
            userColumnReportModel.grid = new GridColumn()
            {
                left = "3%",
                right = "4%",
                bottom = "3%",
                containLabel = true
            };
            userColumnReportModel.xAxis = new List<XAxisColumn>()
            {
                 new XAxisColumn()
                 {  
                     type="category", 
                     data=xAxisData,
                     axisTick=new AxisTickColumn()
                     { 
                         alignWithLabel=true
                     }
                 }
            };
            userColumnReportModel.yAxis = new List<YAxisColumn>()
            {
                new YAxisColumn()
                {
                    type="value"
                }
            };
            userColumnReportModel.series = new List<SeriesColumn>()
            {
                new SeriesColumn()
                { 
                    name="总人数", 
                    type="bar", 
                    barWidth="60%",
                    data=seriesData
                }
            };

            return userColumnReportModel;
        }

        //用户报表(线形)
        public UserColumnReportModel GetUserLineReport()
        {
            UserColumnReportModel userColumnReportModel = new UserColumnReportModel();

            DataTable dtList = userInfoDAL.GetUserDateGroup();
            //排序
            DataTable dtCopy = dtList.Copy();
            DataView dv = dtList.DefaultView;
            dv.Sort = "CreateTime Asc";
            dtCopy = dv.ToTable();

            List<string> xAxisData = new List<string>();
            List<string> seriesData = new List<string>();

            foreach (DataRow row in dtCopy.Rows)
            {
                xAxisData.Add(row["CreateTime"].ToString());
                seriesData.Add(row["CTCount"].ToString());
            }

            userColumnReportModel.color = new string[] { "red" };
            userColumnReportModel.tooltip = new TooltipCAttribute()
            {
                //axisPointer = new AxisColumnPointer() { type = "shadow" },
                trigger = "item"
            };
            userColumnReportModel.xAxis = new List<XAxisColumn>()
            {
                 new XAxisColumn()
                 {  
                     type="category", 
                     data=xAxisData,
                     //axisTick=new AxisTickColumn()
                     //{ 
                     //    alignWithLabel=true
                     //}
                 }
            };
            userColumnReportModel.yAxis = new List<YAxisColumn>()
            {
                new YAxisColumn()
                {
                    type="value"
                }
            };
            userColumnReportModel.series = new List<SeriesColumn>()
            {
                new SeriesColumn()
                { 
                    name="总人数", 
                    type="line", 
                   //barWidth="60%",
                    data=seriesData
                }
            };

            return userColumnReportModel;
        }

        //导出Excel
        //public ExportExcelModel ExportExcel(ExportParams exportParams)
        //{
        //    ExportExcelModel exportExcelModel = new ExportExcelModel() ;
        //    //exportExcelModel.filename = OperateExcel.GridToExcel(
        //    //    exportParams.data,
        //    //    DateTime.Now.ToString("yyyyMMddHHmmss"));
        //    DataTable dtUerInfo = userInfoDAL.SearchInfo(new UserParams(){ name="", isExport=true});
        //    exportExcelModel.filename = OperateExcel.ExportToExcel(dtUerInfo, DateTime.Now.ToString("yyyyMMddHHmmss"));

        //    return exportExcelModel;
        //}

    }
}
