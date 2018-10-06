using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class N_NewsInfo
    {
        public long NId { get; set; }
        public string NType { get; set; }
        public string NTitle { get; set; }
        public string NContent { get; set; }
        public DateTime NCreateTime { get; set; }
        public DateTime NUpdateTime { get; set; }
        public int NIsValid { get; set; }
    }
}
