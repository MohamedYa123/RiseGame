using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise
{

    public class square
    {
        public float x;
        public float y;
        public item piecethere;
        public mapzone mapzone = new mapzone();
        public float Gcost;//distance from source
        public float Hcost;//distance from target
        public int issitastarget;
        public float gcost
        {
            get { return Gcost*1; }
        } public float hcost
        {
            get { return Hcost *1; }
        }
        public bool startpoint; public bool targetpoint;
        public bool examined;
        public bool available;
        public square parent;
        public bool open;
        public float fcost
        {
            get {
                int tt = 1;
                if(piecethere != null) {
                    //tt = 3;
                }
                return (gcost+hcost)*timing*tt; }
        }
        //public float fcost
        //{
        //    get
        //    {
        //        return gcost + hcost;
        //    }
        //}
        public void makesweetswap(map mp,int pathfindingorder,int orderid,item leader)
        {
          //  return;
            for (int i = 0; i < piecestoswapaway.Count; i++)
            {
                var pc = piecestoswapaway[i];
                var dist=Math.Abs(pc.x - leader.x)+ Math.Abs(pc.y - leader.y);
                if (pc != null && (pc.pathfindingorder != pathfindingorder||!pc.walk)&&pc.orderid!=orderid&&dist<leader.squarewidth*3)
                {
                    pc.leader=leader;
                    mp.swapawayfrompoint(pc, (int)(leader.x+pc.x)/2, (int)(leader.y+pc.y)/2, true);
                }
            }
        }
        public override string ToString()
        {
            try
            {
                return $"{x}:{y} gcost:{gcost} hcost:{hcost} cost:{gcost + hcost}";
            }
            catch
            {
                return "";
            }
        }
        public float product;
        enum Calcmode { mathematic,computeric}
        Calcmode calcmode = Calcmode.mathematic;
        public void calcg_and_f(square startpoint, square targetpoint)
        {
            if (gcost == -1 && hcost == -1)
            {
                // product = 1;
                float gcost=0;
                float hcost = 0;
                if (calcmode == Calcmode.mathematic)
                {
                     gcost = MathF.Sqrt(MathF.Pow(x - startpoint.x, 2) + MathF.Pow(y - startpoint.y, 2));
                     hcost = MathF.Sqrt(MathF.Pow(x - targetpoint.x, 2) + MathF.Pow(y - targetpoint.y, 2));
                }
                else
                {
                    gcost = MathF.Abs(MathF.Abs(x - startpoint.x) + MathF.Abs(y - startpoint.y));
                    hcost = MathF.Abs(MathF.Abs(x - targetpoint.x) + MathF.Abs(y - targetpoint.y));

                }
                this.Gcost = gcost*product;
                this.Hcost = hcost*product;
             //   fcost = this.gcost + this.hcost;
            }
        }
        public void checkpathprops(int pathfindingorder,int orderid)
        {
            if (pathfindingorder != lastpathfinding)
            {
                resetpathfindingstaff(pathfindingorder, orderid);
            }
        }
        int lastpathfinding;
        List<item>piecestoswapaway= new List<item>();
        int lastorderid;
        float timing = 1;
        public bool marked;
        public void settiming(square sqrc)
        {
            
            timing=Math.Max(sqrc.timing,timing);
        }
        public void resetpathfindingstaff(int pathfindingorder,int orderid)
        {
            Gcost = -1;
            Hcost = -1;
            startpoint = false;
            targetpoint = false;
            examined = false;
            available = false;
            parent = null;
            open = false;
            product = 1;
            if(lastpathfinding!= pathfindingorder&& orderid== lastorderid)
            {
                if (marked&&false)
                {
                    timing = 1.2f;
                }
            }
            else
            {
                timing = 1;
            }
            marked = false;
            lastpathfinding = pathfindingorder;
            lastorderid = orderid;
            piecestoswapaway.Clear();
        }
        public void decreasestaff()
        {
            Rockettail -= dx;
            Explosion -= 40;
            thinpasses -= 30;
            if (explosion >= 100)
            {

            }
        }
        public  float realxx = 0;
        public float realyy = 0;
        int rockettail;
        int explosion;
        public float thinpasses;
        public int dx = 7;
        public int Rockettail
        {
            get
            {
                if (rockettail > 0)
                {
                   // rockettail--;
                }
                return rockettail;
            }
            set
            {
                if (value > rockettail)
                {
                    dx = 3;
                }
                rockettail = value;
            }
        }
        public int Explosion
        {
            get
            {
                if (explosion > 0)
                {
                   // rockettail--;
                }
                return explosion;
            }
            set
            {
                explosion = value;
            }
        }
        public void addswaps(square sqrc)
        {
            for (int i = 0; i < sqrc.piecestoswapaway.Count; i++)
            {
                var pc = sqrc.piecestoswapaway[i];
                try
                {
                    piecestoswapaway.Remove(pc);
                }
                catch { }
                piecestoswapaway.Add(pc);
            }
        }
        public bool isavailable(item it, item target)
        {
            if (piecethere == null || piecethere.health < 0 || piecethere == it || piecethere == target || piecethere.type == type.bullet || it.type == type.bullet ||(piecethere.type!=type.building&&it.army==piecethere.army))
            {
                if (piecethere != null)
                {
                    piecestoswapaway.Remove(piecethere);
                    piecestoswapaway.Add(piecethere);
                }
                available = true;
                return true;
            }
            available = false;
            return false;
        }
        public bool accept(item it, map mp,bool timeaway,bool moving=false)
        {
            //||it.orderid==piecethere.orderid
            if (piecethere == null||(piecethere.type!=type.building&&timeaway) || piecethere.health < 0 || piecethere == it || piecethere.type == type.bullet || it.type == type.bullet)
            {
                if ((piecethere!=null&& piecethere.type != type.building)||piecethere==null||it.type==type.bullet)
                {
                    piecethere = it;
                    return true;
                }
                
            }
            else
            {
                if (moving)
                {
                    if (piecethere.army == it.army)
                    {
                        bool sw;
                        if (piecethere.orderid != it.orderid||!piecethere.walk)
                        {
                            sw = false;
                        }
                        else
                        {
                            sw = true;
                        }
                        mp.swapawayfrompoint(piecethere, (int)piecethere.x, (int)piecethere.y, sw);

                    }

                }
                return false;
            }
            return false;
            float oldx = MathF.Abs((it.x) - piecethere.x);
            float newx = MathF.Abs((it.x + it.speedx) - piecethere.x);
            float oldy = MathF.Abs((it.y) - piecethere.y);
            float newy = MathF.Abs((it.y + it.speedy) - piecethere.y);
            if (newx > oldx && newy > oldy)
            {
                return true;
            }
            mp.swapaway(piecethere, it, false);
            return false;
        }


    }
    public static class MathF
    {
        public static float negativeotzero(float x)
        {
            if (x > 0)
            {
                return 0;
            }
            return x;
        }
        public static float Abs(float x)
        {
            if (x < 0)
            {
                return -x;
            }
            else
            {
                return x;
            }
        }
        public static float Min(float x, float y)
        {
            if (x < y)
            {
                return x;
            }
            return y;
        }public static float Max(float x, float y)
        {
            if (x > y)
            {
                return x;
            }
            return y;
        }
        public static float Atan(float x)
        {
            return (float)Math.Atan(x);
        }
        public static float Sqrt(float x)
        {
            return (float)Math.Sqrt(x);
        }
        public static float Pow(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }
    }
}
