using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class UserActivity
    {
        public long UActivityId { get; set; }
        public long UAUserId { get; set; }
        public string UAActivityName { get; set; }
        public string UAIntroduce { get; set; }
        public double UAPrice { get; set; }
        public DateTime UACreateTime { get; set; }
        public DateTime UAUpdateTime { get; set; }
        public int UAIsValid { get; set; }
        public int UAIsDel { get; set; }
        public int UAState { get; set; }
    }
}
