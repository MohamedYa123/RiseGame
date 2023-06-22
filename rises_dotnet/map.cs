using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rise
{

    public class map : item
    {
        public string name = "";
        public int id;
        public string picture = "";
        public List<mapzone> zones = new List<mapzone>();
        public resource image;
        public List<resource> resources = new List<resource>();
        public game gm;
        public List<item> items = new List<item>();
        public List<item> asstes = new List<item>();
        public square[,] squares;
         public List<item> itemsclonned=new List<item>();
        public void cloneitems()
        {
            List<item> itemsclonned2 = new List<item>();
           // itemsclonned.Clear();
            for(int i=0;i<items.Count; i++)
            {
                itemsclonned2.Add(items[i].clone());
            }
            itemsclonned = itemsclonned2;
        }

        public void reset_pathfinding()
        {
            return;
            for (int i = 0; i < xlen; i++)
            {
                for (int j = 0; j < ylen; j++)
                {
                    squares[i, j].resetpathfindingstaff(0,0);
                    squares[i, j].x = i;
                    squares[i, j].y = j;
                }
            }
        }
        public bool sqauresreset;
        public void reset_squares()
        {
            sqauresreset=true;
            for (int i = 0; i < xlen; i++)
            {
                for (int j = 0; j < ylen; j++)
                {
                    squares[i, j].piecethere = null;
                    squares[i, j].decreasestaff();
                }
            }
            
        }
        public void swapawayfrompoint(item it,int x,int y,bool sweet=false)
        {
            var ot =0f; var ot2 = 0f;
            //  old.newspeedx *= -1;
            //  old.newspeedy *= -1;
            ot = it.newspeedx; ot2 = it.newspeedy;
            if (it.timeaway >= 0)
            {
             //   return;
            }
            it.timeaway = 4;
            if (sweet)
            {
               // return;
                it.timeaway = 2;
            }
            it.sweetswap = sweet;
            engine.change_direction_direct(it, x, y);
            it.newspeedxtimed = it.newspeedx * -1;
            it.newspeedytimed = it.newspeedy * -1;
            it.newspeedx = ot;
            it.newspeedy = ot2;
        }
        public void swapaway(item old, item it, bool real)
        {
            if (old == it)
            {
                return;
            }
            if (!real)
                return;
            var ot = old.newspeedx; var ot2 = old.newspeedy;
            //   it.x -= 1;
            //   it.y -= 1;
            engine.change_direction_direct(old, (int)it.x + (int)it.width / 2, (int)it.y + (int)it.height / 2);
            old.timeaway = 5;//زمن التباعد
            old.newspeedxtimed = old.newspeedx * -1;
            old.newspeedytimed = old.newspeedy * -1;
            old.newspeedx = ot;
            old.newspeedy = ot2;
            //  old.newspeedx *= -1;
            //  old.newspeedy *= -1;
            ot = it.newspeedx; ot2 = it.newspeedy;
            it.timeaway = 40;
            engine.change_direction_direct(it, (int)old.x - (int)old.width / 2, (int)old.y - (int)old.height / 2);
            it.newspeedxtimed = it.newspeedx * -1;
            it.newspeedytimed = it.newspeedy * -1;
            it.newspeedx = ot;
            it.newspeedy = ot2;
            //  it.newspeedx *= -1;
            //  it.newspeedy *= -1;
        }
        public void putpieceinsquares(item it)
        {

            for (int i = (int)it.x / mod; i <= (it.x + it.squarewidth) / mod; i++)
            {
                for (int j = (int)it.y / mod; j <= (it.y + it.squareheight) / mod; j++)
                {
                    if (i >= xlen || j >= ylen)
                    {
                        continue;
                    }
                    var old = squares[i, j].piecethere;

                    if (old!=null&& old.type!=type.bullet&&it.type==type.bullet)
                    {
                        continue;
                    }
                    //&&old.orderid!=it.orderid
                    if (old != null &&old.leader!=it&& true && old.type != type.bullet && it.type != type.bullet)
                    {
                        swapaway(old, it, true);

                    }
                    squares[i, j].piecethere = it;
                }
            }
        }
        public bool accept(item it, float newx, float newy, float newz,bool moving=false)
        {
            
            int nx = (int)(newx / mod);
            int ny = (int)(newy / mod);
            int nz = (int)(newz / mod);
            if (newx >= width||newx<0||newy<0||newy>=height)
            {
                if (newx >= width)
                {
                    it.newspeedxtimed = -it.basespeed;
                }
                if (newx <0)
                {
                    it.newspeedxtimed = it.basespeed;
                }
                if (newy >= height)
                {
                    it.newspeedytimed = -it.basespeed;
                }
                if (newy <0)
                {
                    it.newspeedytimed = it.basespeed;
                }

                return false;
            }
            bool timeaway=false;
            if (it.timeaway > 0)
            {
                timeaway = true;
               // return true;
            }
            int tt = 0;
            int tf = 0;
            bool minimum;
            for (int i = nx - safzone / mod; i < (newx + it.squarewidth) / mod + safzone / mod; i++)
            {
                for (int j = ny - safzone; j < (newy + it.squareheight) / mod + safzone / mod; j++)
                {
                    if (i < 0 || j < 0 || i >= xlen || j >= ylen)
                    {
                        return false;
                    }

                    if (!squares[i, j].accept(it, this,timeaway,moving))
                    {
                        tf++;
                        return false;
                    }
                    tt++;
                }
            }
            if (tf > tt * 0.1)
            {
                return false;
            }
            return true;
        }
        public float factorw;
        public float factorh;
        public void load_resources(int realwidth,int realheight)
        {
            image = new resource($"maps/{picture}", "", "mappicture", null,1,1,1,1,1,1);
            image.Bitmap = resource.ResizeImage(image.Bitmap, new Size(width_resolution, height_resolution));
            Stopwatch sp = Stopwatch.StartNew();
           // engine.gm.drawstring(image.Bitmap, "cool", 0, 0, Color.Red, 10);
            sp.Stop();
            factorw = (float)(width_resolution) / (float)(realwidth);
            factorh = (float)(height_resolution) / (float)(realheight);
            foreach (var a in asstes)
            {
                a.load_resouces(gm,factorw,factorh);
            }
        }
        int resourcesnum1=80;
        int resourcesnum2=21;
        int resourcesnum3=41;
        int resourcesnum4=20;
        public int load_resource(string pic, string sound, string name, item item,float fw,float fh)
        {
            resource rsc = new resource(pic, sound, name, item,fw,fh, resourcesnum1, resourcesnum2, resourcesnum3,resourcesnum4);
            resources.Add(rsc);
            return resources.Count - 1;
        }
        public int mod = 20;//بيقسم مربعات الخريطة لمربعات أكبر علشان يوفر وقت في البروسيسنج
        public int safzone = 0;
        public int xlen;
        public int ylen;
        public  int width_resolution;
        public int height_resolution;
        public map(string name, int x, int y, string picture, int width, int height)
        {
            width_resolution = width;
            height_resolution = height;
            this.name = name;
            this.width = x; this.height = y;

            int newx = x / mod + 1;
            int newy = y / mod + 1;
            xlen = newx;
            ylen = newy;
            squares = new square[newx, newy];
            for (int i = 0; i < newx; i++)
            {
                for (int j = 0; j < newy; j++)
                {
                    square square = new square();
                    square.x =  i; square.y = j;
                    squares[i, j] = square;
                }
            }
            this.picture = picture;
        }
    }
}
