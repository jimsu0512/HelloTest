using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class UserFocus
    {
        public long UFocusId { get; set; }
        public long UUserId { get; set; }
        public long UFocusUserId { get; set; }
        public DateTime UFCreateTime { get; set; }
        public DateTime UFUpdateTime { get; set; }
        public int UFIsValid { get; set; }
        public int UFIsDel { get; set; }
    }
}
