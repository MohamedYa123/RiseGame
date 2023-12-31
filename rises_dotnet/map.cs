﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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
        public List<mapzone> mapzones = new List<mapzone>();
        public List<item> asstes = new List<item>();
        public square[,] squares;
         public List<item> itemsclonned=new List<item>();
       
        public void addmapzone(mapzone zone)
        {
            mapzones.Add(zone);
            var x = (int)zone.x / mod;
            var y = (int)zone.y / mod;
            for (int i = x-1; i < x+zone.width/mod+1; i++)
            {
                for (int j = y-1; j < y+zone.height / mod+1; j++)
                {
                    squares[i, j].mapzone = zone;
                }
            }
        }
        public void cloneitems()
        {
            List<item> itemsclonned2 = new List<item>();
           // itemsclonned.Clear();
            for(int i=0;i<items.Count; i++)
            {
                var g = items[i].clone();
                g.loadframe = items[i].loadframe;
                itemsclonned2.Add(g);
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
        public void reset_squares_parallel()
        {
            sqauresreset = true;
            int sets = 0;
            Stopwatch sp = new Stopwatch();
            sp.Start();
            //(int i = 0; i < xlen; i++)
            Parallel.For(0, 6, h =>
            {
                for (int i = ylen * h/6; i < ylen * (h + 1)/6; i++)
                {
                    
                    for (int j = 0; j < ylen; j++)
                    {
                        int ii = i;
                        int jj = j;
                        if (squares[i, j].isaffected)
                        {
                            squares[i, j].piecethere2 = null;
                            squares[i, j].decreasestaff();
                        }
                    }
                }
            });
            //   while(sets< xlen * ylen)
            {

            }
            sp.Stop();
            var s = sp.ElapsedMilliseconds;
          //  engine.GameEngineManager.message = $"{s}";
        }
        public void reset_squares()
        {
            sqauresreset=true;

            Stopwatch sp=new Stopwatch();
            sp.Start();
            for (int i = 0; i < xlen; i++)
            {
                for (int j = 0; j < ylen; j++)
                {
                    // Task.Run(() =>
                    if (squares[i,j].isaffected){
                        squares[i, j].piecethere2 = null;
                        squares[i, j].decreasestaff();
                    }
                }
            }
         //   while(sets< xlen * ylen)
            {

            }
            sp.Stop();
            var s= sp.ElapsedMilliseconds;
            engine.GameEngineManager.message = $"{s}";
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
            it.timeaway = Math.Max(4,it.timeaway);
            if (!it.walk)
            {
                it.timeaway = 35;
            }
            if (sweet)
            {
               // return;
              //  it.timeaway = Math.Max(2,it.timeaway);
            }
            it.sweetswap = sweet;
            it.swapingitem = null;
            engine.change_direction_direct(it, x, y);
            it.newspeedxtimed = it.newspeedx * -1;
            it.newspeedytimed = it.newspeedy * -1;
            it.newspeedx = ot;
            it.newspeedy = ot2;
        }
        public void swapaway(item old, item it, bool real)
        {
            if (old == it||old.type==type.bullet||it.type==type.bullet)
            {
                return;
            }
            if (!real)
                return;
            var ot = old.newspeedx; var ot2 = old.newspeedy;
            //   it.x -= 1;
            //   it.y -= 1;
            old.swapingitem= it;
            
            engine.change_direction_direct(old, (int)it.x + (int)it.width / 2, (int)it.y + (int)it.height / 2);
            old.timeaway = 5;//زمن التباعد
            if (it.type == type.building)
            {
                old.timeaway += 15;
            }
            old.newspeedxtimed = old.newspeedx * -1;
            old.newspeedytimed = old.newspeedy * -1;
            old.newspeedx = ot;
            old.newspeedy = ot2;
            //  old.newspeedx *= -1;
            //  old.newspeedy *= -1;
            ot = it.newspeedx; ot2 = it.newspeedy;
            it.timeaway = 5;
            it.swapingitem = old;
            engine.change_direction_direct(it, (int)old.x - (int)old.width / 2, (int)old.y - (int)old.height / 2);
            it.newspeedxtimed = it.newspeedx * -1;
            it.newspeedytimed = it.newspeedy * -1;
            it.newspeedx = ot;
            it.newspeedy = ot2;
            if(it.type == type.building)
            {
                old.timeaway += 15;
            } 
            if(old.type == type.building)
            {
                it.timeaway = 35;
            }
            if (old.newspeedxtimed > 0 == it.newspeedxtimed>0)
            {
                it.newspeedxtimed *= -1;
            }
            if (old.newspeedytimed > 0 == it.newspeedytimed>0)
            {
                it.newspeedytimed *= -1;
            }
            //  it.newspeedx *= -1;
            //  it.newspeedy *= -1;
        }
        int tx = 2;
        public void putpieceinsquares(item it)
        {
            if (it.type == type.air)
            {
                return;
            }
            tx++;
            int ax = (int)it.x / mod;
            int ay = (int)it.y / mod;
            int adx = 0;

                int vx = (int)(it.x + it.squarewidth) / mod + adx;
                int vy = (int)(it.y + it.squareheight) / mod + adx;
                if (it.squarewidth <=50||true)
                {
                // adx = 1;
                    if(it.emptypixels.Count == 0)
                {
                    adx = 1;
                }
                   vx= (int)(it.x / mod) + (int)(it.squarewidth) / mod + adx;
                    vy = (int)(it.y / mod) + (int)(it.squareheight) / mod + adx;
                }
            for (int i = 0; i < it.emptypixels.Count; i++)
            {
                var point = it.emptypixels[i];
                squares[ax + point.X, ay + point.Y].oid = tx;//removing building from empty squares
            }
            bool timeaway = false;
            if (it.timeaway > 0)
            {
                timeaway = true;
                // return true;
            }
            for (int i = (int)it.x / mod; i < vx; i++)
            {
                for (int j = (int)it.y / mod; j < vy; j++)
                {
                    if (i >= xlen || j >= ylen)
                    {
                        continue;
                    }
                    if (squares[i, j].mapzone != null && it.available && it.type == type.building)
                    {
                        squares[i, j].mapzone.length = ((building)it).farmmode;
                    }
                    if (squares[i, j].oid == tx)
                    {
                        
                        continue;
                    }
                    var old = squares[i, j].piecethere;

                    if (old!=null&& old.type!=type.bullet&&it.type==type.bullet)
                    {
                        continue;
                    }
                    //&&old.orderid!=it.orderid
                    if (!squares[i, j].accept(it,this,timeaway))//old != null &&old.leader!=it&& true && old.type != type.bullet && it.type != type.bullet)
                    {
                        swapaway(old, it, true);
                    }
                    squares[i, j].piecethere = it;
                }
            }
           
           
        }
        int test = 11;
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
            int ax = nx - safzone / mod;
            int ay = ny - safzone / mod;
            for (int i = 0; i < it.emptypixels.Count; i++)
            {
                var point = it.emptypixels[i];
                squares[ax + point.X, ay + point.Y].oid2 = test;//removing building from empty squares
            }

            for (int i = ax; i < (int)(it.squarewidth) / mod + nx + safzone / mod; i++)
            {
                for (int j = ay; j < (int)(it.squareheight) / mod+ ny + safzone / mod; j++)
                {
                   
                    if (i < 0 || j < 0 || i >= xlen || j >= ylen)
                    {
                        return false;
                    }
                    if (squares[i, j].oid2 == test)
                    {
                        continue;
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
            test++;
            return true;
        }
        public float factorw;
        public float factorh;
        public void load_resources(int realwidth,int realheight)
        {
            image = new resource($"maps/{picture}", "", "mappicture", null,1,1,1,1,1,1);
            image.Bitmap = resource.ResizeImage(image.Bitmap, new Size(width_resolution, height_resolution));
            engine.progress += 20;
            //Stopwatch sp = Stopwatch.StartNew();
           // engine.gm.drawstring(image.Bitmap, "cool", 0, 0, Color.Red, 10);
            //sp.Stop();
            factorw = (float)(width_resolution) / (float)(realwidth);
            factorh = (float)(height_resolution) / (float)(realheight);
            float dm = 70.0f/asstes.Count;
            foreach (var a in asstes)
            {
                a.load_resouces(gm,factorw,factorh);
                engine.progress += dm;
            }
        }
        int resourcesnum1=361;
        int resourcesnum2=21;
        int resourcesnum3=21;
        int resourcesnum4=20;
        public int load_resource(string pic, string sound, string name, item item,float fw,float fh)
        {
            resource rsc = new resource(pic, sound, name, item,fw,fh, resourcesnum1, resourcesnum2, resourcesnum3,resourcesnum4);
            resources.Add(rsc);
            return resources.Count - 1;
        }
        public int mod = 10;//بيقسم مربعات الخريطة لمربعات أكبر علشان يوفر وقت في البروسيسنج
        public int safzone = 0;
        public int xlen;
        public int ylen;
        public  int width_resolution;
        public int height_resolution;
        public map(string name, int x, int y, string picture, int width, int height,GameEngine engine)
        {
            width_resolution = width;
            height_resolution = height;
            this.name = name;
            this.width = x; this.height = y;

            int newx = x / mod + 1;
            int newy = y / mod + 1;
            xlen = newx;
            ylen = newy;
            this.engine=engine;
            squares = new square[newx, newy];
            var gg =100.0f/( newx * newy);
            for (int i = 0; i < newx; i++)
            {
                for (int j = 0; j < newy; j++)
                {
                    engine.progress += gg;
                    square square = new square();
                    square.x =  i; square.y = j;
                    squares[i, j] = square;
                }
            }
            this.picture = picture;
           // public map()
            {
                type = type.map;
            }
        }
    }
}
