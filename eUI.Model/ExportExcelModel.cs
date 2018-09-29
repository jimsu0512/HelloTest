using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{

    public class ExportParams
    {
        //导出的数据内容(grid内数据内容)
        public string data { get; set; }
    }

    public class ExportExcelModel
    {
        public string filename { get; set; }
    }
}
