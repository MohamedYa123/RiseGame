using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise
{
    public enum zonetype { water, land, high, rock, beach }
    public class mapzone
    {
        public zonetype zonetype = zonetype.land;
        public int x;
        public int y;
        public int width;
        public int height;
    }
}
