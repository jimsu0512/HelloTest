using System;
using System.ComponentModel.DataAnnotations;

namespace eUI.Model
{
    public class Config_ServiceType
    {
        [Key]
        public long CSTTypeId { get; set; }
        public string CSTTypeName { get; set; } = string.Empty;
        public double CSTDaliPrice { get; set; } = 0;
        public DateTime CSTCreateDate { get; set; } = DateTime.Now;
        public DateTime CSTUpdateDate { get; set; } = DateTime.Now;
        public int CSTIsValid { get; set; } = 0;
        public int CSTIsDel { get; set; } = 1;
        public string CSTImg { get; set; } = string.Empty;
    }
    public class Config_ServiceTypeSearch
    {
        public long CSTTypeId { get; set; }
        public string CSTTypeName { get; set; }
        public double CSTDaliPrice { get; set; }
        public string CSTCreateDate { get; set; }
        public string CSTUpdateDate { get; set; }
        public int CSTIsValid { get; set; }
        public int CSTIsDel { get; set; }
        public string CSTImg { get; set; }
    }
}
