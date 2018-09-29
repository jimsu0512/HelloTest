using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BannerType.SearchInfo();
            var result = BannerType.AddEdit(new Model.Config_BannerType { CBTypeId = 5, CBIsValid = 1, CBTypeName = "asdf", CBIsDel = 0 });
            Console.WriteLine("Addresult:" + result);
            BannerType.SearchInfo();
            Console.ReadKey();
        }
    }
}
