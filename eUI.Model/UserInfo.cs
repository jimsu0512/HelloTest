using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace eUI.Model
{

    public class UserParams
    {
        public int id { get; set; }

        public string name { get; set; }

        public DateTime stTime { get; set; }

        public DateTime edTime { get; set; }

        public int page { get; set; }

        public int rows { get; set; }

        public bool isExport { get; set; }
    }


    public class UserInfo
    {
        public long Id { get; set; }
        public string Account { get; set; }
        public string Pwd { get; set; }
        public string Tel { get; set; }
        public string NickName { get; set; }
        public string PicUrl { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public int PjCount { get; set; }
        public string CreateTime { get; set; }
        public string UpdateTime { get; set; }
        public int UserIsValid { get; set; }
        public int UserIsDel { get; set; }

    }

    public class UserInfoList
    {
        public int total { get; set; }
        public List<UserInfo> rows { get; set; }
    }

}
