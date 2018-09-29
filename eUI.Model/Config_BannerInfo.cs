using System;
using System.ComponentModel.DataAnnotations;

namespace eUI.Model
{
    public class Config_BannerInfo
    {
        [Key]
        public long CBannerId { get; set; }
        public long CBannerType { get; set; }
        public string CBannerName { get; set; }
        public string CBannerUrl { get; set; } = string.Empty;
        public string CBannerTUrl { get; set; } = string.Empty;
        public long CBannerTId { get; set; } = 0;
        public int CSort { get; set; }
        public DateTime CBCreateDate { get; set; } = DateTime.Now;
        public DateTime CBUpdateDate { get; set; } = DateTime.Now;
        public int CBIsValid { get; set; }
        public int CBIsDel { get; set; }
    }
}
