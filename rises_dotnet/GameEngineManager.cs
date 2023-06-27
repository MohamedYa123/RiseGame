//using Microsoft.VisualBasic.Devices;
using rises_dotnet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Forms;
//using System.Windows.Media;
using System.Xml.Linq;


namespace Rise
{

    public enum building_type { commandcenter, supply, silvergate, goldgate, warfactory, airport, seaport, barracks, defense, power, stockmarket, diplomatic_center, hospital, bank, other }
    //stock market is an intelligent market where the stock learns by time and increases its winning chances by experience and by safety level and people satsification
    //diplomatic center is for makong agreements and sending money to alies
    //bank is responsible for printing money and controling inflation
    public class GFG : IComparer<square>
    {
        public int Compare(square x, square y)
        {
            var fg1 = (x.hcost + x.gcost);
            var fg2 = (y.hcost + y.gcost);



            if (fg1 == fg2)
            {
                fg1 = x.hcost;
                fg2 = y.hcost;
            }
            if (fg1 == fg2)
            {
                //        fg1 = x.gcost;
                //       fg2 = y.gcost;
            }
            // CompareTo() method
            return fg1.CompareTo(fg2);
        }

    }
    public class GFGBuildings : IComparer<item>
    {
        public int Compare(item x, item y)
        {
            var fg1 = (0.0);
            var fg2 = (0.0);
            if(x.type== type.air)
            {
                fg1 = 700;
            }
            if(y.type== type.air)
            {
                fg2 = 700;
            }
            if (x.timeaway >= 0)
            {
                fg1 = 600;
            }
            if(y.timeaway >= 0)
            {
                fg2 = 600;
            }
            if (x.type == type.building)
            {
                fg1 = 500;
            }
            if (y.type == type.building)
            {
                fg2 = 500;
            }
            fg1 += ((float)x.id)/ int.MaxValue;
            fg2 += ((float)y.id)/ int.MaxValue;
            return fg1.CompareTo(fg2);
        }

    }
    public class GFGworkers : IComparer<item>
    {
        public  float xi; public float yi;
        public int Compare(item x, item y)
        {
            var fg1 = Math.Sqrt(Math.Pow(x.centerx-xi,2)+Math.Pow(x.centery-yi,2));
            var fg2 = Math.Sqrt(Math.Pow(y.centerx-xi,2)+Math.Pow(y.centery-yi,2));
            

            return fg1.CompareTo(fg2);
        }

    }
    public class GameEngineManager
    {
        public GameEngineManager(GameEngine engine, player player, int realwidth, int realheight, int mappicwidth, int mappicheight)
        {
            GameEngine = engine;
            this.player = player;
            this.realwidth = realwidth;
            this.realheight = realheight;
            this.mappicwidth = mappicwidth;
            this.mappicheight = mappicheight;
            selected = null;
        }
        int mappicwidth;
        int mappicheight;
        GameEngine GameEngine;
        bool run = true;
        public int plusx;
        public int plusy;
        public int mousex;
        public int mousey;
        public int plusz;
        public int mousemode;
        public System.Drawing.Bitmap imagetoshowready;
        Thread thr;
        Thread thr2;
        Thread thr3;
        Thread thr4;
        Thread thr5;
        player player;
        int mapframe = 0;
        public System.Drawing.Bitmap mappic;
        int lastcleanup;
        bool mapreading = false;
        void readmap()
        {
            //  return;
            while (true)
            {
                try
                {
                    //  Stopwatch sw = Stopwatch.StartNew();
                    if (imsleepy || true)
                    {
                        mapreading = true;
                        GameEngine.gm.drawmap(enginespeed, player);
                        mappic = resource.ResizeImage(GameEngine.gm.mappic, new Size(mappicwidth, mappicheight));
                        mapreading = false;
                        if (enginespeed < 64)
                        {
                            Thread.Sleep(enginespeed);
                        }
                        else
                        {
                            Thread.Sleep(30);
                        }
                    }
                    else
                    {
                        //   Thread.Sleep(1);
                    }

                    //  sw.Stop();
                    //var x = sw.ElapsedMilliseconds;
                    // GameEngine.gm.drawmap();
                    //  Thread.Sleep(5);
                    //  break;
                    mapframe++;
                }
                catch (Exception ex)
                {
                    if (mapframe > 5)
                    {
                        message = $"(%)Map read error : ' {ex.Message} '";
                    }
                    if (ex.Source == "System.Private.CoreLib")
                    {
                        break;
                    }
                    Thread.Sleep(3);
                }
            }
        }
        public bool imsleepy = false;
        string msg;
        public string message
        {
            get
            {
                return msg;
            }
            set { msg = value; GameEngine.fullmessage = 0; }
        }
        int restnum = 200;
        void readimage()
        {
            int i = 0;
            int lastms = 0;
            while (true)
            {
                try
                {
                    imsleepy = false;
                    player.mousex = mousex; player.mousey = mousey;
                    try
                    {
                        GC.TryStartNoGCRegion(1);
                    }
                    catch { }
                    Stopwatch sp = new Stopwatch();
                    sp.Start();
                    GameEngine.fill(player, ref msg, resizingframe,lastms);

                    imresizing = true;
                   // imagetoshowready = resource.ResizeImage(GameEngine.todraw, new Size(realwidth, realheight));
                    imresizing = false;
                    imsleepy = true;

                    sp.Stop();
                    lastms = (int)sp.ElapsedMilliseconds+1;
                  //  message = $"lastms : {lastms}";
                    //  Thread.Sleep(1);
                    if (i % 1 == 0&&lastms<12)
                    {
                        Thread.Sleep(2);
                    }
                 
                    if (false)
                    {

                        Thread.Sleep(50);
                    }

                    if (i % restnum == 0)
                    {


                       // Thread.Sleep(20);
                    }
                    i++;
                }
                catch (Exception ex)
                {

                    if (ex.Source == "System.Private.CoreLib")
                    {
                        break;
                    }
                    if (ex.Message == "Out of memory.")
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                        Thread.Sleep(15);
                    }
                    message = $"(*)Frame read error : ' {ex.Message} '";
                    Thread.Sleep(3);
                }
            }
        }
        int enginespeed;
        public void setenginespeednext()
        {
            switch (enginespeed)
            {
                case 0:
                    enginespeed = 64; break;
                case 2:
                    enginespeed = 1;
                    break;
                case 1:
                    enginespeed = 200000000;
                    break;
                case 200000000:
                    thr3.Abort();
                    thrr = new ThreadStart(runme);
                    thr3 = new Thread(thrr);
                    thr3.Start();
                    enginespeed = 256;
                    break;
                case 256:
                    enginespeed = 128;
                    break;
                case 128:
                    enginespeed = 64;
                    break;
                case 64:
                    enginespeed = 32;
                    break;
                case 32:
                    enginespeed = 16;
                    break;
                case 16:
                    enginespeed = 8;
                    break;
                case 8:
                    enginespeed = 4;
                    break;
                case 4:
                    enginespeed = 2;
                    break;
            }
            message = $"Engine speed set to {Math.Round(64.0 / enginespeed, 3)}x";
        }
        int engineframe = 0;
        bool engineworking;
        public int enginetime;
        void runme()
        {
            GameEngine.GameEngineManager = this;
            if (run)
            {
                //   return;
                ThreadStart thrr = new ThreadStart(readimage);
                thr = new Thread(thrr);
                thr.Start();
                thr2 = new Thread(() => readmap());
                thr2.Start();
            }
            while (true)
            {
                run = false;

                //   try
                {
                    engineworking = true;
                    Stopwatch sw = Stopwatch.StartNew();
                    GameEngine.update(player);
                    sw.Stop();
                    enginetime = (int)sw.ElapsedMilliseconds;
                    engineworking = false;
                    Thread.Sleep(enginespeed);
                    engineframe++;
                }
                //   catch (Exception ex)
                {
                    //     message = $"($)Engine error {ex.Message}";
                    //   Thread.Sleep(100);
                }


            }
        }
        int realwidth;
        int realheight;
        void clean()
        {
            while (true)
            {
                Thread.Sleep(1);
            }
        }
        bool imresizing = false;
        int resizingframe = 0;
        int lastresizingframe = 0;
        void collect()
        {
            Thread.Sleep(50);
            if (notcollecting)
            {
                notcollecting = false;
                GC.Collect(int.MaxValue, GCCollectionMode.Optimized);
                GC.WaitForPendingFinalizers();
                notcollecting = true;
            }
        }
        bool notcollecting = true;
        void resizeandshow()
        {
            while (true)
            {
                try
                {
                    if (imsleepy || true)
                    {
                        imresizing = true;
                        imagetoshowready = resource.ResizeImage(GameEngine.todraw, new Size(realwidth, realheight));
                        imresizing = false;
                        if (imsleepy && !imresizing && resizingframe - lastresizingframe > 500 && !engineworking)
                        {//&&resizingframe==0
                            Stopwatch sp = new Stopwatch();
                            sp.Start();
                            //    GC.Collect();
                            Task.Run(() => collect());
                            if (resizingframe == 0)
                            {
                                //GC.WaitForPendingFinalizers();
                            }
                            sp.Stop();
                            lastresizingframe = resizingframe;
                            message = $"Cleanup took time {sp.ElapsedMilliseconds} ms";
                            //    Thread.Sleep(25);
                        }
                        else
                        {
                            Thread.Sleep(1);
                        }
                        resizingframe++;

                    }
                    else
                    {
                        //   Thread.Sleep(3);
                    }
                }
                catch (Exception e)
                {
                    if (e.Source == "System.Private.CoreLib")
                    {
                        break;
                    }
                    Thread.Sleep(5);
                }

            }
        }
        ThreadStart thrr;
        public void runorclose(bool ifpossible)
        {
            if (run)
            {
                setenginespeednext();
                thrr = new ThreadStart(runme);
                thr3 = new Thread(thrr);
                thr3.Start();
                thrr = new ThreadStart(resizeandshow);
                thr4 = new Thread(thrr);
                thr4.Start();
                thrr = new ThreadStart(clean);
                thr5 = new Thread(thrr);
                //   thr5.Start();
                //Task.Run(() => { runme(); });
            }
            else if (!ifpossible)
            {
                shutdown();
            }
        }
        List<order> orders = new List<order>();
        public int fx;
        public int fy;
        item sc;
        public item selected
        {
            get
            {
                return sc;
            }
            set
            {
                sc = value;
                if (value == null)
                {
                    sc = GameEngine.gm.map.items[0];
                    if (GameEngine.ppanel != null)
                    {
                      //  GameEngine.fill_selection(GameEngine.ppanel, sc, player);
                    }
                }
            }
        } 
        public building selected_b;
        item a;
        public void cancelselection()
        {
            foreach (var it in GameEngine.gm.map.items)
            {
                it.selected = false;
            }
        }
        square getnextsquare(int x, int y, item it, int orderid, int plusorminusx, int plusorminusy)
        {
            square square = null;// = new square();
            int modx = ((int)it.squarewidth) / GameEngine.gm.map.mod + 2;
            x = x / GameEngine.gm.map.mod;
            y = y / GameEngine.gm.map.mod;
            var xlen = GameEngine.gm.map.xlen;
            var ylen = GameEngine.gm.map.ylen;
            //for (  i += modx * plusorminusx)
            {
                for (int j = 0, i = 0; j + y < ylen && j + y >= 0 && i + x < xlen && x + i >= 0; j += modx * plusorminusy, i += modx * plusorminusx)
                {
                    var square2 = GameEngine.gm.map.squares[x + i, y + j];
                    if (square2.issitastarget != orderid && square2.isavailable(it, null) && (true||it.checksurroundings(square2, x + i, y + j, null, null)))
                    {
                        if (square2.piecethere != null && square2.piecethere.type == type.building)
                        {

                        }
                        square2.issitastarget = orderid;
                        square2.x = x + i;
                        square2.y = y + j;
                        return square2;
                    }

                }
            }
            return square;
        }
        internal class comparesquares : IComparer<square>
        {
            public int targetx;
            public int targety;
            public comparesquares(int mousex, int mousey, int mod)
            {
                targetx = mousex / mod;
                targety = mousey / mod;
            }
            public int Compare(square x, square y)
            {
                float dist1 = (float)Math.Abs(Math.Abs(x.x - targetx) + Math.Abs(x.y - targety));
                float dist2 = (float)Math.Abs(Math.Abs(y.x - targetx) + Math.Abs(y.y - targety));
                return -dist1.CompareTo(dist2);
            }
        }

        public void sendorder()
        {
            Stopwatch sp=new Stopwatch();
            sp.Start();
            while (!GameEngine.gm.map.sqauresreset)
            {
                if (sp.ElapsedMilliseconds > 5)
                {
                //    break;
                }
            }
            sp.Stop();
            order order = new order();
            //  GameEngine.selectitems(player, fx, fy, mousex, mousey);
            item fv = GameEngine.match(mousex, mousey, player);
            if (selected == null)
            {
                //a = GameEngine.match(mousex, mousey, player);
            }
            int ise = 0;
            //x += pl.x; y += pl.y;
            int nextx = mousex + player.x;
            int nexty = mousey + player.y;
            int mx = mousex + player.x;
            int my = mousey + player.y;
            if (GameEngine.tobuild != null)
            {
                var xxg = player.silver - GameEngine.tobuild.silver;
                if (xxg < 0)
                {
                    message = "Insuffcient funds ";
                    return;
                }

                var xb = GameEngine.tobuild.clone();
                xb.available = false;
                xb.x = mx - xb.width / 2;
                xb.y = my - xb.height / 2;
                xb.health = 1;
                int w = 0;
                while (engineworking)
                {
                    if (w % 50 == 0)
                    {
                        Thread.Sleep(1);
                    }
                    w++;
                }
                xb.canceledx = false;
                xb.canceledy = false;
                message = "";
                GameEngine.requestchangeposition(xb);
                if (xb.canceledx || xb.canceledy)
                {
                    if (message == "")
                    {
                        message = "Inavailable place to build";
                    }
                    return;
                }

                var b = GameEngine.additem(xb);
                if (b)
                {
                    player.silver = xxg;
                    GameEngine.tobuild = null;
                }
            }
            int plusorminusx = 1;
            int plusorminusy = 1;
            int time = 0;
            void getnext()
            {
                switch (time)
                {
                    case 0:
                        plusorminusx = 1;
                        plusorminusy = 1;
                        break;
                    case 1:
                        plusorminusx = 1;
                        plusorminusy = -1;
                        break;
                    case 2:
                        plusorminusx = -1;
                        plusorminusy = 1;
                        break;
                    case 3:
                        plusorminusx = 1;
                        plusorminusy = 0;
                        break;
                    case 4:
                        plusorminusx = 0;
                        plusorminusy = 1;
                        break;
                    case 5:
                        plusorminusx = -1;
                        plusorminusy = -1;
                        break;
                    case 6:
                        plusorminusx = -1;
                        plusorminusy = 0;
                        break;
                    case 7:
                        plusorminusx = 0;
                        plusorminusy = -1;
                        break;
                }
                if (time == 7)
                {
                    time = -1;
                }
                time++;

            }
            List<item> selecteditems = new List<item>();
            List<square> selectedsquares = new List<square>();
            int found = 7;
            int iss = 0;
            int cx = mx; int cy = my;
            void getnextsq()
            {
                if (selectedsquares.Count > iss)
                {
                    cx = (int)selectedsquares[iss].x * GameEngine.gm.map.mod;
                    cy = (int)selectedsquares[iss].y * GameEngine.gm.map.mod;
                }
                if (iss < selectedsquares.Count)
                {
                    iss++;
                }
            }
            for (int i = 0; i < GameEngine.gm.map.items.Count; i++)
            {
                var it = GameEngine.gm.map.items[i];
                if (it.selected && it.army.owner == player && it != fv && it.type != type.building)
                {
                    if (fv == null)
                    {

                        square s = null;// getnextsquare(mx, my, it, order.orderid,plusorminus);
                        int counts = 0;

                        while (counts < 8)
                        {
                            if (found >= 7)
                            {
                                getnextsq();
                                found = 0;
                            }
                            found++;
                            getnext();
                            if (s == null)
                            {

                                s = getnextsquare(cx, cy, it, order.orderid, plusorminusx, plusorminusy);
                                if (s != null)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                            counts++;
                        }
                        if (s != null)
                        {

                            selectedsquares.Add(s);
                            nextx = (int)s.x * GameEngine.gm.map.mod;
                            nexty = (int)s.y * GameEngine.gm.map.mod;
                            //GameEngine.change_direction(it, nextx, nexty, player);
                            selecteditems.Add(it);
                        }
                    }
                    it.walk = true;
                    it.target = fv;
                    it.orderid = order.orderid;
                    ise++;
                }
            }
            float centerx = 0;
            float centery = 0;
            for (int i = 0; i < selecteditems.Count; i++)
            {
                centerx += selecteditems[i].x;
                centery += selecteditems[i].y;
            }
            centerx /= selecteditems.Count;
            centery /= selecteditems.Count;
            comparesquares comp = new comparesquares((int)centerx, (int)centery, GameEngine.gm.map.mod);
            selectedsquares.Sort(comp);
            int pointt = 0;

            while (selecteditems.Count > 0)
            {
                item topcloses = null;
                float topdist = float.MaxValue;
                int dx = (int)selectedsquares[pointt].x * GameEngine.gm.map.mod;
                int dy = (int)selectedsquares[pointt].y * GameEngine.gm.map.mod;
                for (int i = 0; i < selecteditems.Count; i++)
                {
                    var dist = selecteditems[i].getdistance(dx, dy);
                    if (dist < topdist)
                    {
                        topdist = dist;
                        topcloses = selecteditems[i];
                    }
                }
                selecteditems.Remove(topcloses);
                GameEngine.change_direction(topcloses, dx, dy, player);
                pointt++;
            }
            if (ise == 0)
            {
                selected = fv;
                selected.selected = true;

            }
            else
            {
                if (selected != null)
                {
              //      selected.selected = false;
                  //  selected = null;
                    sc = null;
                }
            }
            fx = -1;
            fy = -1;
            orders.Add(order);
            if (selected != null && selected.type == type.building)
            {
                selected_b = (building)selected;
                // selected = null;
            }
            else
            {
                selected_b = null;
            }
        }
        public void shutdown()
        {
            thr.Abort();
            thr2.Abort();
            thr3.Abort();
            thr4.Abort();
            thr5.Abort();
        }
    }

}
