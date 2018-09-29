using eUI.BLL;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace easyUITest.Controllers
{
    public class UserController : ApiController
    {
        UserInfoBLL userInfoBLL = new UserInfoBLL();

        [HttpPost]
        public UserInfoList SearchInfo(UserParams userInfo)
        {
            UserInfoList listUserInfo = userInfoBLL.SearchInfo(userInfo);

            return listUserInfo;
        }

        [HttpPost]
        public ExportExcelModel ExportExcel(UserParams userInfo)
        {
            return userInfoBLL.ExportExcel(userInfo);
        }

        [HttpPost]
        public bool AddEdit(UserInfo userInfo)
        {
            return userInfoBLL.AddEdit(userInfo);
        }

        [HttpPost]
        public bool Del(UserParams userInfo)
        {
            return userInfoBLL.Del(userInfo);
        }

        [HttpPost]
        public UserInfo GetUserInfo(UserParams userInfo)
        {
            UserInfo listUserInfo = userInfoBLL.GetUserInfo(userInfo);

            return listUserInfo;
        }

        [HttpPost]
        public UserReportModel GetUserReport()
        {
            return userInfoBLL.GetUserReport();
        }

        [HttpPost]
        public UserColumnReportModel GetUserColumnReport()
        {
            return userInfoBLL.GetUserColumnReport();
        }

        [HttpPost]
        public UserColumnReportModel GetUserLineReport()
        {
            return userInfoBLL.GetUserLineReport();
        }

        [HttpGet]
        public List<string> ListString()
        {
            return new List<string>() { "100","200","300"};
        }

        //[HttpPost]
        //public ExportExcelModel ExportExcel(ExportParams exportParams)
        //{
        //    return userInfoBLL.ExportExcel(exportParams);
        //}
    }
}
