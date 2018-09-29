using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ResponseModel
{
   

    public class PageList<T> {
        public List<T> rows { get; set; }
        public int total { get; set; }
    }

    public class PageParam
    {
        public int page { get; set; }
        public int rows { get; set; }
    }
}
