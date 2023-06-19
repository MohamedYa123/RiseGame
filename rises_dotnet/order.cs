using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rises_dotnet
{
    
    public class order
    {
        public int orderid;
        public Random random=new Random();
        public order() { 
            orderid=random.Next(int.MinValue,int.MaxValue);
        }
    }
}
