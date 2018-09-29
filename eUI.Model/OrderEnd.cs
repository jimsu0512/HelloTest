using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Model
{
    public class OrderEnd
    {
        public long OEId { get; set; }
        public long OEOrderId { get; set; }
        public long OEUserIdOne { get; set; }
        public int OEState { get; set; }

        public DateTime OECreateDate { get; set; }
        public DateTime OEUpdateDate { get; set; }
        public int OEIsValid { get; set; }
        public int OEIsDel { get; set; }
    }
}
