using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise
{
    public class army
    {
        public int id;
        public string name = "USA";
        public double safetylevel = 100;//0=>100
        public double peoplelove = 100;
        public int upgradelevel;
        public int energylevel = 0;
        public double speadfactor = 1;
        public double powerfactor = 1;
        public double healthfactor = 1;
        public double sellfactor = 1;
        public double buyfactor = 1;
        public player owner;
        public Color armycolor = Color.DarkCyan;
        public List<building> buildings = new List<building>();
        public GameEngine engine;
        public int teamid;
        public static army create_usa_army(GameEngine engine)
        {
            army army = new army(engine);
            army.teamid = 1;
            return army;
        }
        public army(GameEngine engine)
        {
            this.engine = engine;
        }
        public void load_resources()
        {
            foreach (var b in buildings)
            {
                b.load_resouces(engine.gm,1,1);
            }
        }
        public void loadresources()
        {

        }
        public army clone()
        {
            return (army)MemberwiseClone();
        }
    }
}
