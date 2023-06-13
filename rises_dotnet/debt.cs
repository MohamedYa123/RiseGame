using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise
{

    public enum debttype { gold, silver }
    public class debt
    {
        public double amount;
        public int id;
        public string name;
        public double profit;
        public double moreprofitrate;
        public debttype debttype;
        public player debter;
        public player debted;
        public long date_topay;
    }
}
