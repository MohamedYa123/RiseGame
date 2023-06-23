using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rise
{
    public class game
    {
        public string name = "";
        public map map;
        public List<player> players = new List<player>();
        public long time;
        public player plformap;
        int frame = 0;
        public List<square> squaresofinterest=new List<square>();
         List<square> squaresofinterest2=new List<square>();
        public void tick(player pl)
        {
            plformap = pl;


            for (int i = 0; i < map.items.Count; i++)
            {
                var it = map.items[i];
                it.health -= 0.005f;
                it.health += 0.005f;
                if (it.health < 0 || ((it.x <= 0 || it.x > map.width + 300 || it.y <= 0 || it.y > map.height + 300) && it.timed))
                {
                    map.items[i].die();
                    map.items.RemoveAt(i);
                    i--;
                    continue;
                }
                it.read();
                if (it.health >= 0)//&&it.type!=type.bullet
                {
                    map.putpieceinsquares(it);
                }
            }
            map.reset_squares();
            for (int i = 0; i < map.items.Count; i++)
            {

                var it = map.items[i];
                if (it.health >= 0)
                {
                    map.putpieceinsquares(it);
                }
            }

            map.sqauresreset = false;
            map.cloneitems();
            var gm = this;
            squaresofinterest2.Clear();
            for (int i = pl.x / gm.map.mod; i - pl.x / gm.map.mod < pl.width && i < gm.map.xlen; i++)
            {
                for (int j = pl.y / gm.map.mod; j - pl.y / gm.map.mod < pl.height && j < gm.map.ylen; j++)
                {
                    var sqrc = gm.map.squares[i, j];
                    sqrc.x = i;
                    sqrc.y = j;
                    float fact = (float)(sqrc.Rockettail / 50.0);
                    int sz = (int)(fact * 10);
                    // sqrc.Rockettail++;
                    bool added=false;
                    if (sz > 0)
                    {
                   //     System.Drawing.Rectangle rect = new Rectangle(i * gm.map.mod - pl.x - sz / 2, j * gm.map.mod - pl.y - sz / 2, sz, sz);
                     //   g.FillEllipse(Brushes.DarkRed, rect);
                     squaresofinterest2.Add(sqrc);
                        added = true;
                    }
                    fact = (float)(sqrc.Explosion / 150.0);
                    sz = (int)((1 - fact) * 150);
                    if ((sz > 0 && sqrc.Explosion > 0||sqrc.thinpasses>1)&&!added)
                    {
                        squaresofinterest2.Add(sqrc);
                     //   Brush b = new SolidBrush(Color.FromArgb(50, 100, 100, 0));
                     //   System.Drawing.Rectangle rect = new Rectangle(i * gm.map.mod - pl.x - sz / 2, j * gm.map.mod - pl.y - sz / 2, sz, sz);
                     //   g.FillEllipse(b, rect);
                    }
                }
            }
            time++;
            squaresofinterest.Clear();
            squaresofinterest.AddRange(squaresofinterest2);
            frame++;
        }
        public void drawmap(int enginespeed,player pl) {
            hello:
            while (map.sqauresreset)
            {
                if (enginespeed > 8)
                {
                    Thread.Sleep(1);
                }
            }
            var dx = map.xlen / 100;
            var dx2 = (int)(Math.Min(map.width, map.height) / 10000);
            if (dx == 0 || true)
            {
                dx = 1;
            }
            if (dx2 == 0)
            {
                dx2 = 1;
            }
            var mappic = (Bitmap)map.image.bitmap2.Clone();
            if (mappic.Width != map.xlen && mappic.Height != map.ylen)
            {
                mappic = map.image.bitmap2;// resource.ResizeImage(map.image.bitmap2, new Size(map.xlen / dx, map.ylen / dx));
                mappic =resource.ResizeImage(map.image.bitmap2, new Size(map.xlen / dx, map.ylen / dx));
                map.image.bitmap2 = (Bitmap)mappic.Clone();
            }
            //  var mappic = resource.ResizeImage(map.image.bitmap2, new Size(map.xlen / dx, map.ylen / dx));
            // var mappic = new Bitmap(map.xlen, map.ylen);
            int squares = 0;
            if (true)
            {
                int skipfactor = dx2;
                Dictionary<item, int> drawnpieces=new Dictionary<item,int>();
                for (int i = 0; i < map.xlen; i += skipfactor)
                {
                    for (int j = 0; j < map.ylen; j += skipfactor)
                    {
                        var sqrpiece = map.squares[i, j].piecethere;
                        if (map.sqauresreset)
                        {
                            goto hello;
                        }
                        if (sqrpiece != null&& !(sqrpiece.stealth && sqrpiece.army.teamid != pl.army.teamid) && sqrpiece.type!=type.bullet&&!drawnpieces.ContainsKey(sqrpiece))
                        {
                            drawnpieces.Add(sqrpiece, sqrpiece.id);
                            squares++;
                            if (sqrpiece.basespeed > 0)
                            {

                            }
                            var x = (int)(sqrpiece.x / dx / map.mod);
                            var y = (int)(sqrpiece.y / dx / map.mod);
                            int sqrwidth = (int)sqrpiece.squarewidth / map.mod / dx;
                            if (sqrwidth <2)
                            {
                                sqrwidth = 2;
                            }
                            if (sqrwidth == 0)
                            {
                                sqrwidth = 1;
                            }
                            int alpha = (int)(sqrpiece.loadframe.opacity * 255);
                            //alpha = 255;
                            if (alpha < 255)
                            {
                                if (sqrpiece.loadframe.opacity < 0.55f)
                                {
                                    alpha -= 50;
                                }
                                else
                                {
                                    //alpha += 50;
                                }
                                alpha -= 50;
                                alpha = Math.Max(0, alpha);
                            }
                            Color armycolor = Color.FromArgb(alpha, sqrpiece.army.armycolor.R, sqrpiece.army.armycolor.G, sqrpiece.army.armycolor.B);
                            if (sqrpiece.loadframe.opacity>0.5f||true)
                            {
                                drawsquare(mappic, x, y, armycolor, sqrwidth);
                            }
                            if (sqrpiece.selected && frame % 2 == 0)
                            {
                                var a = sqrpiece;
                                if (a.targetx != -1 && a.target == null && a.walk)
                                {
                                    drawsquare(mappic, (int)(a.targetx / map.mod) + 1, (int)(a.targety / map.mod) + 1, Color.LightGreen, 4, true);
                                }
                                else if (a.target != null)
                                {
                                    drawsquare(mappic, (int)(a.target.x + a.target.width / 2) / map.mod, (int)(a.target.y + a.target.height / 2) / map.mod, Color.Red, 4, true);

                                }

                            }
                        }
                    }
                }
                drawpos(mappic, dx);
            }
            if (squares == 0)
            {

            }
            this.mappic = mappic;
        }
        void drawpos(Bitmap bitmp1,int dx)
        {
            var factorw=map.factorw; var factorh=map.factorh;
            var g = Graphics.FromImage(bitmp1);
            g.DrawRectangle(Pens.Blue, 3, 3, bitmp1.Width - 7, bitmp1.Height - 7);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            //  g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //    var area = new Rectangle(0, 0, bitmp2.Width / 2, bitmp2.Height / 2);
            //  g.FillRectangle(new LinearGradientBrush(area, Color.PaleGoldenrod, Color.OrangeRed, 45), area);
            //  var width = 1;
            var minx = (int)Math.Min(map.width, (plformap.width/factorw + plformap.x));
            var miny = (int)Math.Min(map.height, plformap.height/factorh + plformap.y);
            var x = minx - plformap.width/factorw;
            var y = miny - plformap.height/factorh;
            minx -= (int)x;
            miny -= (int)y;
            g.DrawRectangle(Pens.Black, (x / map.mod + 0) / dx, (y / map.mod  + 0) / dx, (minx / map.mod - 0)/dx, (miny / map.mod - 0)/dx);
            g.FillEllipse(Brushes.Yellow, (plformap.x+ plformap.mousex) / map.mod, (plformap.y  + plformap.mousey)  / map.mod, 5, 5);
            g = null;
        }
        public void drawmousselction(Bitmap bitmp1, int x1, int y1, int x2, int y2)
        {
            var g = Graphics.FromImage(bitmp1);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            //  g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //    var area = new Rectangle(0, 0, bitmp2.Width / 2, bitmp2.Height / 2);
            //  g.FillRectangle(new LinearGradientBrush(area, Color.PaleGoldenrod, Color.OrangeRed, 45), area);
            //  var width = 1;
            int newx = Math.Min(x1, x2);
            int newy = Math.Min(y1, y2);
            int absx = Math.Abs(x1 - x2);
            int absy = Math.Abs(y1 - y2);
            g.DrawRectangle(Pens.White, newx, newy, absx, absy);
            g = null;

        }
        public void drawstring(Bitmap bitmp1, string s, int x, int y, Color cl, int size = 1)
        {
            
            var g = Graphics.FromImage(bitmp1);
            
            g.SmoothingMode = SmoothingMode.AntiAlias;
            //  g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //    var area = new Rectangle(0, 0, bitmp2.Width / 2, bitmp2.Height / 2);
            //  g.FillRectangle(new LinearGradientBrush(area, Color.PaleGoldenrod, Color.OrangeRed, 45), area);
            var width = 1;
            //  g.DrawRectangle(Pens.LightCyan, x, y, 1, 1);
            var b = new SolidBrush(cl);
            Font f = new Font("tahoma", size, FontStyle.Bold);
            //Stopwatch sp = Stopwatch.StartNew();
            g.DrawString(s, f, b, x, y);
            //sp.Stop();
        }
        public void drawsquare(Bitmap bitmp1, int x, int y, Color cl, int size = 1, bool circle = false)
        {
            var g = Graphics.FromImage(bitmp1);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
          //  g.DrawRectangle(Pens.LightCyan, x, y, 1, 1);
            var b = new SolidBrush(cl);
            if (circle)
            {
                g.FillEllipse(b, x - 5, y - 5, size, size);
            }
            else
            {
                g.FillRectangle(b, x, y, size, size);
            }
            //  g.DrawRectangle(Pens.White, x, y, (int)size, (float)size / 50 * 8 + 1);
            // var b = new SolidBrush(Color.FromArgb(20, (byte)(255 * (1 - width / 100)), (byte)(155 * (width / 100)), 0));
            // g.FillRectangle(b, x + 1, y + 1, (int)(width * ((float)size / 50)) / 2 - 1, (float)size / 50 * 8);
            // g.FillRectangle(b, x, y, size, size);
            g = null;
        }
        public Bitmap mappic;
        public void start()
        {

        }

    }
}
