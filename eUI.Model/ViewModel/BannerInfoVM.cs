using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model.ViewModel
{
    public class BannerInfoVM
    {
        public long CBannerId { get; set; }
        public string CBTypeName { get; set; }

        public string CBannerName { get; set; }

        public string CBannerUrl { get; set; }
        public int CSort { get; set; }

        public string CBCreateDate { get; set; }

        public string CBUpdateDate { get; set; }
        public int CBIsValid { get; set; }
        public int CBIsDel { get; set; }

        public int TotalCount { get; set; }
    }
}
