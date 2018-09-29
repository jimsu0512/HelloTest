using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
    public class UserActivityVM
    {
        public long UActivityId { get; set; }
        public string NickName { get; set; }
        public string UAActivityName { get; set; }
        public string UAIntroduce { get; set; }
        public double UAPrice { get; set; }
        public int UAState { get; set; }

        public string UACreateTime { get; set; }
        public string UAUpdateTime { get; set; }

        public int TotalCount { get; set; }
    }
}
