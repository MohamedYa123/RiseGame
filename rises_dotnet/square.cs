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
        public float gcost;//distance from source
        public float hcost;//distance from target
        public bool startpoint; public bool targetpoint;
        public bool examined;
        public bool available;
        public square parent;
        public bool open;
        public float fcost;
        //public float fcost
        //{
        //    get
        //    {
        //        return gcost + hcost;
        //    }
        //}
        public override string ToString()
        {
            return $"{x}:{y} gcost:{gcost} hcost:{hcost} cost:{gcost + hcost}";
        }
        public float product;
        public void calcg_and_f(square startpoint, square targetpoint)
        {
            if (gcost == -1 && hcost == -1)
            {
               // product = 1;
                var gcost = MathF.Sqrt(MathF.Pow(x - startpoint.x, 2) + MathF.Pow(y - startpoint.y, 2));
                var hcost = MathF.Sqrt(MathF.Pow(x - targetpoint.x, 2) + MathF.Pow(y - targetpoint.y, 2));
                this.gcost = gcost*product;
                this.hcost = hcost*product;
                fcost = this.gcost + this.hcost;
            }
        }
        public void checkpathprops(int pathfindingorder)
        {
            if (pathfindingorder != lastpathfinding)
            {
                resetpathfindingstaff(pathfindingorder);
            }
        }
        int lastpathfinding;
        public void resetpathfindingstaff(int pathfindingorder)
        {
            gcost = -1;
            hcost = -1;
            startpoint = false;
            targetpoint = false;
            examined = false;
            available = false;
            parent = null;
            open = false;
            product = 1;
            lastpathfinding = pathfindingorder;
        }
        public bool isavailable(item it, item target)
        {
            if (piecethere == null || piecethere.health < 0 || piecethere == it || piecethere == target || piecethere.type == type.bullet || it.type == type.bullet)
            {
                available = true;
                return true;
            }
            available = false;
            return false;
        }
        public bool accept(item it, map mp,bool timeaway)
        {
            if (piecethere == null||(piecethere.type!=type.building&&timeaway) || piecethere.health < 0 || piecethere == it || piecethere.type == type.bullet || it.type == type.bullet)
            {
                piecethere = it;
                return true;
            }

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
