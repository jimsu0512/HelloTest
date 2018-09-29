using eUI.BLL;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace easyUITest.Controllers
{
    public class PermissionSetController : ApiController
    {
        UserInfoBLL userInfoBLL = new UserInfoBLL();

        public PermissionSetStatus SetPermission(PermissionIDListModel permissionIDListModel)
        {
            return userInfoBLL.UpdatePermission(permissionIDListModel);
        }

        [HttpPost]
        public List<PermissionActionID> GetUserPermission(PermissionIDListModel permissionIDListModel)
        {
            return userInfoBLL.GetUserPermission(permissionIDListModel);
        }
    }
}
