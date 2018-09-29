using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class PermissionIDListModel
    {
        public int UserId { get; set; }

        public List<PermissionActionID> ActionIDList { get; set; }

    }

    public class PermissionActionID
    {
        public int ActionID { get; set; }
    }

    public class PermissionSetStatus
    {
        public bool IsSucceed { get; set; }

    } 

}
