using System.ComponentModel.DataAnnotations;

namespace eUI.Model
{
    public class Config_BannerType
    {
        [Key]
        public long CBTypeId { get; set; }
        public string CBTypeName { get; set; }
        public int CBIsValid { get; set; } = 1;

        public int CBIsDel { get; set; }
    }
}
