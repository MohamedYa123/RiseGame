using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise
{
    public class item
    {
        public List<square> pathsquares = new List<square>();
        public float change = 0.15f;
        public int pointer;
        public float targetx = -1;
        public float targety = -1;
        public float minitargetx = -1;
        public float minitargety = -1;
        public bool canceledx;
        public bool canceledy;
        public GameEngine engine;
        public player owner;
        public string image;
        public int resourceid;
        public float x;
        public float y;
        public float z;
        public float width;
        public float height;
        public type type;
        public army army;
        public tracktype track;
        public bool resettarget;
        protected int num;//عدد المربعات علشان مع كل تشيك على الباث فايندينج أشوفها
        public List<square> openlist = new List<square>();
        protected int minus = 0;
        protected int originalnum;
        int pathfindingorder;
        Random random=new Random();
        public int id;
        public item() { 
            id=random.Next();
        }
        public List<square> pathfinding(square startpoint, square targetpoint, float x, float y)
        {
            originalnum = (int)Math.Ceiling(squarewidth / engine.gm.map.mod);
            num = originalnum+minus ;//لازم أتأكد أن المسار مناسب لحجم الدبابة
            List<square> path = new List<square>();
            engine.gm.map.reset_pathfinding();
            if (!targetpoint.isavailable(this, target))
            {
                //var p = targetpoint.piecethere;
                //if (p.army == army) {
                //   // engine.gm.map.swapawayfrompoint(p, (int)targetx, (int)targety);
                //    if(p.targetx==targetx&&p.targety==targety)
                //    {
                //        p.targetx = -1;
                //        p.targety = -1;
                //    }  
                //    }
                return path;
            }
            pathfindingorder = random.Next(int.MinValue,int.MaxValue);
            startpoint.checkpathprops(pathfindingorder);
            targetpoint.checkpathprops(pathfindingorder);
            startpoint.startpoint = true;
            targetpoint.targetpoint = true;
            openlist.Clear();
            var curretpoint = startpoint;
            var b = autosearch(startpoint, targetpoint);
            //  var b=search(curretpoint,startpoint,targetpoint);
            if (!b)
            {
                return path;
            }
            curretpoint = targetpoint;
            var last = curretpoint;
            x /= engine.gm.map.mod;
            y /= engine.gm.map.mod;

            while (true)
            {
                if (curretpoint == null || curretpoint.startpoint)
                {
                    break;
                }
                last = curretpoint;
                curretpoint = curretpoint.parent;
                // var dist = MathF.Sqrt(MathF.Pow(curretpoint.x - x, 2) + MathF.Pow(curretpoint.y - y, 2));
                // path.Add(curretpoint);
                if (true)
                {
                    path.Add(curretpoint);
                    //   break;
                }
            }
            return path;
        }
        void open(square square, square current)
        {
            if (!square.open)
            {
                square.open = true;
                square.parent = current;
                openlist.Add(square);
            }
        }
        square best = null;
        public bool autosearch(square startpoint, square targetpoint)
        {
            best = startpoint;
            int step = 0;
            while (step < 5000 && !(best == null && step > 0))
            {
                var b = search(best, startpoint, targetpoint);
                if (b||best!=null&&best.targetpoint)
                {
                    return true;
                }
                step++;
            }
            return false;
        }
        protected float basicdoublingfactor = 1;
        bool checksurroundings(int x, int y,square targetpoint,square startpoint)
        {
            if (num == 0)
            {
                return true;
            }
            float doublingfactor = basicdoublingfactor;
            var diffx=Math.Abs(x-startpoint.x);
            var diffy=Math.Abs(y-startpoint.y);
            float product = 1;
            bool doubleme = false;
            if (diffx <= num && diffy <= num)
            {
                doubleme = true;
                if (basicdoublingfactor != 1)
                {
                    doublingfactor = basicdoublingfactor * 1.5f;
                }
              //  product = 5;
               // return true;
            }
             diffx = Math.Abs(x - targetpoint.x);
             diffy = Math.Abs(y - targetpoint.y);
            if (diffx <= num && diffy <= num)
            {
                doubleme= true;
               // product = 5;
               // return true;
            }
            var xlen = engine.gm.map.xlen - 1;
            var ylen = engine.gm.map.ylen - 1;
            if (engine.gm.map.squares[x, y].startpoint|| engine.gm.map.squares[x, y].targetpoint)
            {
                return true;
            }
            for (int i = -num; i < num; i++)
            {
                for (int j = -num; j < num; j++)
                {
                    var nx = x + i;
                    var ny = y + j;
                    
                    if (nx <= 0 || ny <= 0 || nx >= xlen || ny >= ylen)
                    {
                        continue;
                    }
                    engine.gm.map.squares[nx, ny].checkpathprops(pathfindingorder);
                    engine.gm.map.squares[nx, ny].isavailable(this, target);
                    if (!engine.gm.map.squares[nx, ny].available)
                    {
                        if (!doubleme)
                        {

                            return false;
                        }
                        product *= doublingfactor;
                    }
                }
            }
            //if(product != 50)
            {
                engine.gm.map.squares[x, y].product=product;
            }
            return true;
        }
        public bool search(square current, square startpoint, square targetpoint)
        {
            bool targetReached = false;
            var xlen = engine.gm.map.xlen - 1;
            var ylen = engine.gm.map.ylen - 1;
            var x = current.x;
            var y = current.y;
            //   List<square> list = new List<square>();
            current.examined = true;
            openlist.Remove(current);
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0)//||(i!=0&&j!=0)
                    {
                        continue;
                    }
                    var nx = x + i;
                    var ny = y + j;
                    if (nx <= 0 || ny <= 0 || nx >= xlen || ny >= ylen)
                    {
                        continue;
                    }

                    var squarec = engine.gm.map.squares[(int)nx, (int)ny];
                    squarec.checkpathprops(pathfindingorder);
                    if (!squarec.targetpoint)
                    {
                        if (i == 0 || j == 0)
                        {
                            //   continue;
                        }
                    }
                    squarec.isavailable(this, target);
                    if (squarec.examined || squarec.startpoint || !squarec.available || squarec.open || !checksurroundings((int)nx, (int)ny,startpoint,targetpoint))
                    {
                        continue;
                    }
                    if (squarec.product != 1)
                    {

                    }
                    squarec.calcg_and_f(startpoint, targetpoint);
                    open(squarec, current);
                    if (squarec.targetpoint)
                    {
                        return true;
                    }
                    //   list.Add(squarec);
                }
            }

            float bestfcost = float.MaxValue;
            best = null;
            for (int i = 0; i < openlist.Count; i++)
            {
                var sqrc = openlist[i];
                if (sqrc.fcost < bestfcost || (sqrc.fcost == bestfcost && sqrc.hcost < best.hcost))//|| (sqrc.fcost == bestfcost && sqrc.gcost < best.gcost)
                {
                    best = sqrc;
                    bestfcost = sqrc.fcost;
                }

            }
            // if(best!=null)
            //     return search(best, startpoint, targetpoint);
            //   list.Sort(engine.GFG);
            //for(int i = 0; i < list.Count; i++)
            //{
            //    var squarec = list[i];
            //    var b = search(squarec, startpoint, targetpoint);
            //    if (b)
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        for(int ii=0;ii<openlist.Count;ii++)
            //        {
            //            openlist[ii].open = false;
            //        }
            //        openlist.Clear();
            //    }
            //    break;
            //}
            return targetReached;
        }
        public float squarewidth
        {
            get { return MathF.Min(width, height); }
        }
        public float squareheight
        {
            get { return MathF.Min(width, height); }
        }
        public int depth;
        public float speedx;
        public float speedy;
        public float newspeedx=0;
        public float newspeedy=0;
        public float newspeedxtimed;
        public float newspeedytimed;
        public int timeaway;
        public float speedz;
        public float basespeed;//
        public float emergencyspeed;
        public float basicdirection;
        float olddirection;
        public string sound = "";
        public bool selected;
        public float health;
        public float maxhealth;
        public bool timed = false;
        public int buildtime_ms = 1000;
        public int buildtime;
        public bool available;
        public bool walk = true;
        public item target = null;
        public float rangeofattack;
        public item clone()
        {
            return (item)MemberwiseClone();
        }

        public virtual void read()
        {

        }
        private Bitmap RotateImageBasic(Bitmap bmp, double angle2)
        {
            float angle = Convert.ToSingle(angle2);
            bmp = (Bitmap)bmp.Clone();
            Bitmap rotatedImage = new Bitmap(bmp.Width * 1, bmp.Height * 1);
            rotatedImage.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {

                // Set the rotation point to the center in the matrix
                g.TranslateTransform(bmp.Width / 2 * 1, bmp.Height / 2 * 1);
                // Rotate
                g.RotateTransform(angle);
                // Restore rotation point in the matrix
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
                // Draw the image on the bitmap
                g.DrawImage(bmp, new Point(0, 0));
            }

            return rotatedImage;
        }
        private Bitmap RotateImage(Bitmap bmp, double angle2)
        {
            float angle = Convert.ToSingle(angle2);
            Bitmap rotatedImage = new Bitmap(bmp.Width * 3, bmp.Height * 3);
            //rotatedImage.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);

            using (Graphics g = Graphics.FromImage(rotatedImage))
            {

                // Set the rotation point to the center in the matrix
                g.TranslateTransform(bmp.Width / 2 * 3, bmp.Height / 2 * 3);
                // Rotate
                g.RotateTransform(angle);
                // Restore rotation point in the matrix
                g.TranslateTransform(-bmp.Width / 2, -bmp.Height / 2);
                // Draw the image on the bitmap
                g.DrawImage(bmp, new Point(0, 0));
            }

            return rotatedImage;
        }
        public float speed
        {
            get
            {
                return MathF.Sqrt(speedx * speedx + speedy * speedy);
            }
        }
        public static float getdirection(float speedx, float speedy)
        {
            float direction = 0;
            direction = MathF.Atan(MathF.Abs(speedy) / MathF.Abs(speedx));
            if (speedx > 0 && speedy < 0)
            {
                direction = 3.14f - direction;
                direction += 3.14f * 4 / 4;
            }
            else if (speedx < 0 && speedy > 0)
            {
                direction = 3.14f - direction;
                direction += 3.14f * 7 / 4;
                direction += 0.6f;
            }
            else if (speedx < 0 && speedy < 0)
            {
                direction += 3.14f * 3 / 4;
                direction += 0.7f;
            }
            else
            {
                //  direction += 3.14f/2f;
            }
            direction += 3.14f / 2f;
            return direction;
        }
        public void copybitmapdata(item it)
        {
            if (it.id != id)
            {
                return;
            }
           // lastbitmap = (Bitmap)it.lastbitmap.Clone();
            it.olddirection = olddirection;
        }
        public Bitmap resourcebitmap;
         void prepareresourcebitmap(game gm)
        {
            resourcebitmap = (Bitmap)gm.map.resources[resourceid].Bitmap.Clone();
        }
        float anglestep = 1f;
        float zstep = 1f;
        public Bitmap load(game gm, bool basic = false)
        {
            float trx = 1;
            float tryy = 1;
            if (canceledx)
            {
                trx = 0.001f;
            }
            if (canceledy)
            {
                tryy = 0.0001f;
            }
            float direction = 0;
            if (true)
            {
                if (speedy == 0)
                {
                    speedy = 0.00001f;
                }
                if (speedx == 0)
                {
                    speedx = 0.00001f;
                }
                direction = getdirection(speedx * trx, speedy * tryy);
            }

            direction += basicdirection / 180 * 3.14f;
            if (speedx == speedy && speedx == 0.00001f)
            {
                direction = olddirection;
            }
            
            Bitmap btmp;
            var directionindegrees = direction * 180 / 3.14;
            directionindegrees -= (int)(directionindegrees / 360)*360;
            var id1 = (int)Math.Round( directionindegrees/ anglestep);
            var id2 = (int)Math.Round(z / zstep);
            var id3 = 0;
            var sr = gm.map.resources[resourceid].GetSecondresource(id1, id2, id3);
            directionindegrees = id1 * anglestep;
            if (sr != null&&!basic)
            {
                return sr.bitmap;
            }
            prepareresourcebitmap( gm);
            btmp = resourcebitmap;
            if (!basic && type != type.bullet&&false)
            {
                int tz = 1;
                if (type == type.building)
                {
                    tz = 0;
                }
                gm.drawsquare(btmp, 0 + (btmp.Width / 2-btmp.Width/20) * tz, 0 + btmp.Height /2 * tz, army.armycolor, btmp.Width / 10);
            }
            if (true)
            {
                if (basic)
                {
                    btmp = RotateImageBasic(btmp, directionindegrees);
                }
                else
                {
                    btmp = RotateImage(btmp, directionindegrees);
                }
            }
            
            
            if (z <3)
            {
                btmp = resource.ResizeImage(btmp, new Size((int)(btmp.Width + btmp.Width / 20 * z), (int)(btmp.Height + btmp.Height / 20 * z)));
                
            }
            if (!basic)
            {
                gm.map.resources[resourceid].setsecondresource(btmp, id1, id2, id3);
            }
           // lastbitmapclonned = (Bitmap)btmp.Clone();
           // lastbitmap = btmp;
            return btmp;
        }
        public virtual void load_resouces(game gm,float fw,float fh)
        {
            throw new Exception("");
        }

    }
}
