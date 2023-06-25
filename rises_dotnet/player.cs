using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise
{

    public class player
    {
        public int mousex;
        public int mousey;
        public int fx = -1;
        public int fy = -1;
        public string name = "";
        public int id;
        //resources //
        public float silver;
        public float gold;
        public float iron;
        public float rock;
        public float food;
        public float wood;
        //
        public double debt_gold;
        public double debt_silver;
        public double inflationlevel;
        
        public int team;
        public List<debt> debts = new List<debt>();
        public army army;
        public int score;
        public int star;
        public resource flag;
        public GameEngine engine;
        public int realx;
        public int realy;
        public int realz;
        public command cmd;
        public int x
        {
            get { return realx; }
            set
            {
                realx = value;
                if (realx < 0)
                {
                    realx = 0;
                }
                if (realx > engine.gm.map.width)
                {
                    realx = (int)engine.gm.map.width;
                }
            }
        }
        public int y
        {
            get { return realy; }
            set
            {
                realy = value;
                if (realy < 0)
                {
                    realy = 0;
                }
                if (realy > engine.gm.map.height)
                {
                    realy = (int)engine.gm.map.height;
                }
            }
        }
        public int z
        {
            get { return realz; }
            set
            {
                realz = value;
                if (realz < 0)
                {
                    realz = 0;
                }
                if (realz > 100)
                {
                    realz = 100;
                }
            }
        }
        public int width;
        public int height;
        public settings settings;
        public void makeorder(int orderid, double ordervalue, int tag)
        {
            engine.makeorder(orderid, ordervalue, tag);
        }
        public void load_resources()
        {
            army.load_resources();
        }
    }
}
