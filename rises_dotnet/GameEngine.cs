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
    
    public class GameEngine
    {

        public GFG GFG=new GFG();
        public GameEngine() {
           // init();
        }
        public game gm = new game();
        piece createTank(player player)
        {
            piece pc = new piece();
            pc.generaltype = generaltype.vehicle;
            pc.salary = 5;
            pc.owner = player;
            pc.workersrequired = 2;
            pc.stealth = false;
            pc.buildtime_ms = 150;
            pc.buildtime = 150;
            pc.army = player.army;
            pc.image = "tank.png";
            pc.name = "tank";
            pc.width = 50;
            pc.height = 80;
            pc.speedx = 0f;
            pc.newspeedx = 0.0f;
            pc.speedy = -0.01f;
            pc.basespeed = 4f;
            pc.emergencyspeed = 14f;
            pc.newspeedy = 0.0001f;
            pc.power = 15;
            pc.maxpower = 3;
            pc.basicdirection = -1;
            pc.engine = this;
            pc.shot_time_ms = 30;
            pc.reloadtime_ms = 100;
            pc.maxbullets = 2;
            pc.health = 100;
            pc.maxhealth = 100;
            pc.x = 5600;
            pc.y = 5200;
            pc.silver = 500;
            //    pc.z = 20;
            pc.type = type.vehicle;
            pc.track = tracktype.full;
            pc.type = type.vehicle;
            pc.rangeofattack = 300f;
         //   pc.basicdirection = 90;
            return pc;
        }
        piece createautomachine(player player,piece bullet)
        {
            piece pc = new piece();
            pc.armedbullet = bullet;
            pc.generaltype = generaltype.vehicle;
            pc.salary = 2;
            pc.owner = player;
            pc.workersrequired = 1;
            pc.stealth = false;
            pc.buildtime_ms = 60;
            pc.buildtime = 60;
            pc.army = player.army;
            pc.image = "auto machine.png";
            pc.name = "auto machine";
            pc.width = 40;
            pc.height = 40;
            pc.speedx = 0f;
            pc.newspeedx = 0.0f;
            pc.speedy = -0.01f;
            pc.basespeed = 12f;
            pc.emergencyspeed = 14f;
            pc.newspeedy = 0.0001f;
            pc.power = 0.65f;
            pc.maxpower = 0.65f;
            pc.basicdirection = -1;
            pc.engine = this;
            pc.shot_time_ms = 1;
            pc.reloadtime_ms = 5;
            pc.maxbullets = 10;
            pc.health = 20;
            pc.maxhealth = 20;
            pc.x = 5600;
            pc.y = 5200;
            pc.silver = 500;
            //    pc.z = 20;
            pc.type = type.vehicle;
            pc.track = tracktype.full;
            pc.type = type.vehicle;
            pc.rangeofattack = 300f;
            pc.targettype = generaltype.infantry;
         //   pc.basicdirection = 90;
            return pc;
        }
        piece createhumvee(player player)
        {
            piece pc = new piece();
            pc.generaltype = generaltype.vehicle;
            pc.salary = 3;
            pc.owner = player;
            pc.workersrequired = 1;
            pc.stealth = false;
            pc.buildtime_ms = 80;
            pc.buildtime = 80;
            pc.army = player.army;
            pc.image = "humvee.png";
            pc.name = "humvee";
            pc.width = 45;
            pc.height = 27;
            pc.speedx = 0f;
            pc.newspeedx = 0.0f;
            pc.speedy = -0.01f;
            pc.basespeed = 8f;
            pc.emergencyspeed = 14f;
            pc.newspeedy = 0.0001f;
            pc.power = 5;
            pc.maxpower = 5;
            pc.basicdirection = -1;
            pc.engine = this;
            pc.shot_time_ms = 15;
            pc.reloadtime_ms = 30;
            pc.maxbullets = 3;
            pc.health = 25;
            pc.maxhealth = 25;
            pc.x = 5600;
            pc.y = 5200;
            pc.silver = 500;
            //    pc.z = 20;
            pc.type = type.vehicle;
            pc.track = tracktype.full;
            pc.type = type.vehicle;
            pc.rangeofattack = 200f;
            pc.basicdirection = 270;
            return pc;
        }
        piece createbullet(player player)
        {
            piece bullet = new piece();
            bullet.generaltype = generaltype.bullet;
            bullet.targettype = generaltype.vehicle;
            bullet.type = type.bullet;
            bullet.army = player.army;
            bullet.track = tracktype.simple;
            bullet.timed = true;
            bullet.type = type.bullet;
            bullet.change = 0.16f;
            bullet.image = "bullet1.png";
            bullet.name = "bullet";
            bullet.sound = "bullet.wav";
            bullet.type = type.bullet;
            bullet.width = 5;
            bullet.height = 10;
            bullet.speedx = 10;
            bullet.speedy = 10;
            bullet.basespeed = 25;
            bullet.x = -400;
            bullet.basicdirection = 7;
            bullet.engine = this;
            bullet.health = 5;
            bullet.maxhealth = 5;
            bullet.power = 10;
            return bullet;
        }
        piece createthinbullet(player player)
        {
            piece bullet = new piece();
            bullet.generaltype = generaltype.bullet;
            bullet.targettype = generaltype.infantry;
            bullet.type = type.bullet;
            bullet.army = player.army;
            bullet.track = tracktype.naive;
            bullet.timed = true;
            bullet.type = type.bullet;
            bullet.change = 0.16f;
            bullet.image = "bullet1.png";
            bullet.name = "bullet";
            bullet.sound = "bullet.wav";
            bullet.type = type.bullet;
            bullet.width = 1;
            bullet.height = 3;
            bullet.speedx = 10;
            bullet.speedy = 10;
            bullet.basespeed = 90;
            bullet.x = -400;
            bullet.basicdirection = 7;
            bullet.engine = this;
            bullet.health = 0.1f;
            bullet.maxhealth = 0.1f;
            bullet.power = 10;
            return bullet;
        }
        piece createworker(player player)
        {
            piece worker = new piece();
            worker.patience = 20;
            worker.generaltype = generaltype.infantry;
            worker.type = type.worker;
            worker.salary = 1;
            worker.basespeed = 5;
            worker.image = "h1.png";
            worker.width = 30;
            worker.height = 30;
            worker.buildtime = 50;
            worker.buildtime_ms = 50;
            worker.stealth = false;
            worker.owner = player;
            worker.army = player.army;
            worker.engine = this;
            worker.x = 5700;
            worker.y = 5000;
            worker.health = 10;
            worker.maxhealth = 10;
            worker.track = tracktype.full;
            //make worker able to attack
            worker.shot_time_ms = 30;
            worker.reloadtime_ms = 20;
            worker.power = 5;
            worker.maxbullets = 1;
            worker.rangeofattack = 150;
            worker.silver = 100;
            worker.imagesofanimations.Add(0, "h2.png");
            worker.imagesofanimations.Add(1, "h3.png");
            return worker;
        }
        piece createrbg(player player, piece bullet)
        {
            piece worker = new piece();
            worker.generaltype = generaltype.infantry;
            worker.targettype = generaltype.vehicle;
            worker.type = type.worker;
            worker.salary = 2;
            worker.basespeed = 7;
            worker.image = "rpg.png";
            worker.width = 30;
            worker.height = 30;
            worker.buildtime = 50;
            worker.buildtime_ms = 50;
            worker.stealth = false;
            worker.owner = player;
            worker.army = player.army;
            worker.engine = this;
            worker.x = 5700;
            worker.y = 5000;
            worker.health = 15;
            worker.maxhealth = 15;
            worker.track = tracktype.full;
            //make worker able to attack
            worker.shot_time_ms = 5;
            worker.reloadtime_ms = 20;
            worker.power = 12;
            worker.maxbullets = 4;
            worker.rangeofattack = 250;
            worker.silver = 250;
            worker.workersrequired = 1;
            worker.type = type.soldier;
            //  worker.imagesofanimations.Add(0, "h2.png");
            //  worker.imagesofanimations.Add(1, "h3.png");
            worker.change = 0.9f;
            return worker;
        }
        piece createsniper(player player,piece bullet)
        {
            piece worker = new piece();
            worker.patience = 100;
            worker.generaltype = generaltype.infantry;
            worker.type = type.worker;
            worker.targettype = generaltype.infantry;
            worker.onlyattacktarget = true;
            worker.salary = 3;
            worker.basespeed = 5;
            worker.image = "sniper.png";
            worker.width = 30;
            worker.height = 30;
            worker.buildtime = 50;
            worker.buildtime_ms = 50;
            worker.stealth = true;
            worker.owner = player;
            worker.army = player.army;
            worker.engine = this;
            worker.x = 5700;
            worker.y = 5000;
            worker.health = 10;
            worker.maxhealth = 10;
            worker.track = tracktype.full;
            //make worker able to attack
            worker.shot_time_ms = 10;
            worker.reloadtime_ms = 10;
            worker.power = 11;
            worker.maxbullets = 1;
            worker.rangeofattack = 1050;
            worker.silver = 700;
            worker.basicdirection = 270;
            worker.armedbullet = bullet;
            worker.workersrequired = 1;
            worker.type = type.soldier;
            worker.change = 0.9f;
            //worker.imagesofanimations.Add(0, "h2.png");
            //worker.imagesofanimations.Add(1, "h3.png");
            return worker;
        }
        piece createsniperbullet(piece thinbullet)
        {
            var p = (piece)thinbullet;
            p.onlyattacktarget = true;
            p.basespeed = 70;
            p.width =3;
            p.height=9;
            return p;
        }
        building createwarfactory(player player)
        {
            building warfactory = new building();
            warfactory.generaltype = generaltype.building;
            warfactory.width = 300;
            warfactory.height = 300;
            warfactory.owner = player;
            warfactory.engine = this;
            warfactory.health = 1500;
            warfactory.stealth = false;
            warfactory.maxhealth = 1500;
            warfactory.type = type.building;
            warfactory.available = true;
            warfactory.army = player.army;
            warfactory.image = "war factory.png";
            warfactory.buildtime_ms = 500;
            warfactory.buildtime = 500;
            warfactory.posx = 150;
            warfactory.posy = 70;
            warfactory.silver = 3000;
            warfactory.workersrequired = 5;
            warfactory.salary = warfactory.workersrequired+2;
            return warfactory;
        }
        building createbarracks(player player)
        {
            building warfactory = new building();
            warfactory.generaltype = generaltype.building;
            warfactory.width = 200;
            warfactory.height = 200;
            warfactory.owner = player;
            warfactory.engine = this;
            warfactory.health = 1500;
            warfactory.stealth = false;
            warfactory.maxhealth = 1500;
            warfactory.type = type.building;
            warfactory.available = true;
            warfactory.army = player.army;
            warfactory.image = "barracks.png";
            warfactory.buildtime_ms = 250;
            warfactory.buildtime = 250;
            warfactory.posx = 150;
            warfactory.posy = 70;
            warfactory.silver = 3000;
            warfactory.workersrequired = 3;
            warfactory.salary = warfactory.workersrequired + 2;
            return warfactory;
        }
        building createhouse(player player,piece worker)
        {
            building warfactory = new building();
            warfactory.name = "house";
            warfactory.type = type.building;
            warfactory.generaltype= generaltype.building;
            warfactory.width = 100;
            warfactory.height = 100;
            warfactory.owner = player;
            warfactory.engine = this;
            warfactory.health = 60;
            warfactory.stealth = false;
            warfactory.maxhealth = 60;
            warfactory.type = type.building;
            warfactory.available = true;
            warfactory.army = player.army;
            warfactory.image = "house.png";
            warfactory.buildtime_ms = 150;
            warfactory.buildtime = 150;
            warfactory.posx = 50;
            warfactory.posy = 70;
            warfactory.silver = 700;
            warfactory.workersrequired = 2;
            warfactory.piecesallowed.Add(worker);
            warfactory.autobuildms = 300;
            warfactory.salary = 4;
            return warfactory;
        }
        building createGoldmine(player player)
        {
            building warfactory = new building();
            warfactory.generaltype = generaltype.building;
            warfactory.width = 150;
            warfactory.height = 150;
            warfactory.owner = player;
            warfactory.engine = this;
            warfactory.health = 80;
            warfactory.stealth = false;
            warfactory.maxhealth = 80;
            warfactory.type = type.building;
            warfactory.available = true;
            warfactory.army = player.army;
            warfactory.image = "gold mine.png";
            warfactory.buildtime_ms = 200;
            warfactory.buildtime = 200;
            warfactory.posx = 85;
            warfactory.posy = 30;
            warfactory.silver = 1500;
            warfactory.producesmoney = 5;
            warfactory.owner = player;  
            warfactory.workersrequired = 10;
            warfactory.salary = 15;
            return warfactory;
        }
        public void init(int widthresolution,int heightresolution,int realwidth,int realheight,Panel panel3)
        {
            map mp = new map("desert2", 10000, 10000, "desert.jpg", widthresolution, heightresolution);
            mp.engine = this;
            gm = new game();
            mp.gm = gm;
            player player = new player();
            player.silver = 90000;
            player.army = army.create_usa_army(this);
            player.army.owner = player;
            var worker = createworker(player);
            //
         
            //
            building building = new building();
            building.generaltype = generaltype.building;
            building.producesmoney = 0;
            building.width = 400;
            building.height = 400;
            building.owner = player;
            building.x = 5000;
            building.y = 5000;
         //   building.stealth = true;
            building.engine = this;
            building.stealth = false;
            //building.y = 100;
            building.health = 8000;
            building.maxhealth = 8000;
            building.type = type.building;
            building.available = true;
            building.image = "cmd center.png";
            building.type=type.building;
            building.army = player.army;
            building.posx = 190;
            building.posy = 150;
            var pc = createTank(player);
           var bullet=createbullet(player);
            player.name = "medo";
            player.engine = this;
            player.settings.mousespeed = 1;
            var humvee = createhumvee(player);
            gm.map = mp;
            gm.players.Add(player);
            var warfactory=createwarfactory(player);
            var godlmine = createGoldmine(player);
            var thinbullet=createthinbullet(player);
            var automachine = createautomachine(player,thinbullet);
            var barraks=createbarracks(player);
            var sniperbullet = createsniperbullet(thinbullet);
            var sniper = createsniper(player,sniperbullet);
            var rbg = createrbg(player, bullet);
            barraks.piecesallowed.Add(rbg);
            barraks.piecesallowed.Add(sniper);
            mp.asstes.Add(bullet);
            mp.asstes.Add(pc);
            mp.asstes.Add(building);
            mp.asstes.Add(warfactory);
            mp.asstes.Add(worker);
            mp.asstes.Add(humvee);
            mp.asstes.Add(godlmine);
            mp.asstes.Add(automachine);
            mp.asstes.Add(barraks);
            mp.asstes.Add(sniper);
            mp.asstes.Add(sniperbullet);
            mp.asstes.Add(rbg);
            building house = createhouse(player, worker);
            mp.asstes.Add(house);
            var xp = pc;
            warfactory.piecesallowed.Add(automachine);
            warfactory.piecesallowed.Add(humvee);
            warfactory.piecesallowed.Add(xp);
            building.buildingsallowed.Add(house);
            building.buildingsallowed.Add(godlmine);
            building.buildingsallowed.Add(barraks);
            building.buildingsallowed.Add(warfactory);
           // building.piecesallowed.Add(worker);
            mp.load_resources(realwidth, realheight);
            var ppc = pc.clone();
            ppc.army = army.create_usa_army(this);
            ppc.army.teamid = 0;

            //mp.items.Add(ppc);
            mp.items.Add(building.clone());
            loadassets(panel3);
            var workere =worker.clone();
            workere.army = ppc.army;
            ppc.addworker(workere);
            workere = worker.clone();
            workere.army = ppc.army;
            ppc.addworker(workere);
            float x = worker.x+worker.width*3;
            float y= worker.y+worker.height*3;
            for (int cc = 0; cc < 4; cc++)
            {
                for (int i = 0; i < 5; i++)
                {
                    var w = worker.clone();
                    w.x = x;
                    w.y = y;
                    mp.items.Add(w);
                    x += worker.width * 3;
                    // y += worker.height;

                }
                y += worker.height * 3;
                x = worker.x + worker.width * 3;
            }
            //mp.asstes.Add(xp);

            gm.start();
            player.x = (int)building.x-100;
            player.y = (int)building.y-100;
        }
        Bitmap stop;
        Bitmap protect;
        Bitmap aggressive;
        Bitmap passive;
        void loadassets(Panel panel3)
        {
            stop = new Bitmap("assets/stop.png");
            passive = new Bitmap("assets/passive.png");
            protect = new Bitmap("assets/protect.png");
            aggressive = new Bitmap("assets/aggressive.png");
            Size sz=new Size(panel3.Width/3-5,panel3.Height/3-5);
            stop = resource.ResizeImage(stop, sz);
            passive = resource.ResizeImage(passive, sz);
            aggressive = resource.ResizeImage(aggressive, sz);
            protect = resource.ResizeImage(protect, sz);
        }
        public void requestchangeposition(item it)
        {
            //  return;
            var hh = it.timeaway;
            var hh2 = it.pathfindingorder;
            var newx = it.x + it.speedx*speedfactor;
            var newy = it.y + it.speedy*speedfactor;
            var newz = it.z + it.speedz * speedfactor;
            it.canceledx = true;
            it.canceledy = true;
            int dv = 1;
            if (it.basespeed > gm.map.mod) {
                dv = 10;
            }
            for (int i = 0; i < dv; i++)
            {
                 newx = it.x + it.speedx * speedfactor/dv;
                 newy = it.y + it.speedy * speedfactor/dv;
                 newz = it.z + it.speedz * speedfactor / dv;
                int newxx = (int)Math.Round((it.x) / gm.map.mod);
                int newyy = (int)Math.Round((it.y) /gm.map.mod);
                
                if (gm.map.accept(it, newx, newy, newz, true))
                {
                    it.canceledx = false;
                    it.canceledy = false;
                    it.x = newx;
                    it.y = newy;
                }
                else if (gm.map.accept(it, newx, it.y, newz, true))
                {
                    it.canceledy = true;
                    it.x = newx;
                    //it.speedy = 0;

                }
                else if (gm.map.accept(it, it.x, newy, newz, true))
                {
                    it.canceledx = true;
                    it.y = newy;
                    //it.speedx = 0;

                }
                it.firehit(newxx, newyy);
                if (it.canceledx && it.speedx > 0 == it.newspeedx > 0)
                {
                    it.newspeedx *= -1;
                }

                if (it.canceledy && it.speedy > 0 == it.newspeedy > 0)
                {
                    it.newspeedy *= -1;
                }
                it.z = newz;
            }
        }
        public float speedfactor = 1f;
        void changemode(List<item> selectedones,piecemode piecemode,player pl)
        {
            foreach(var item in selectedones)
            {
                if (item.army.owner != pl)
                {
                    continue;
                }
                if (piecemode != piecemode.stop)
                {
                    item.mode = piecemode;
                }
                else
                {
                    stopwalk(item);
                }
            }
        }
        public void loadselection(item it, Panel panel3,player pl)
        {
            building b = null;
           
            try
            {
                b = (building)it;
                if (it == null||pl!=b.owner||!b.selected)
                {
                    throw new Exception("not building");
                }
            }
            catch { 
                List<item> selectedones = new List<item>();
                int protected1=0;
                int passive1 = 0;
                int agressive1 = 0;
                for(int i=0;i<gm.map.items.Count;i++)
                {
                    var item = gm.map.items[i];
                    if(item.selected&&item.army.owner==pl)
                    {
                        if(item.mode == piecemode.protect) { protected1++; }
                        if(item.mode == piecemode.passive) { passive1++; }
                        if(item.mode == piecemode.agressive) { agressive1++; }
                        selectedones.Add(item);
                    }
                }
                if(selectedones.Count==0) {
                    return;
                }
                int x = 7;
                int y = 7;
                panel3.Controls.Clear();
                for (int i = 0; i < 4; i++)
                {
                    
                    PictureBox picture = new PictureBox();
                    //a.prepareresourcebitmap(gm);
                    Bitmap btmp =protect;
                    piecemode piecemode=piecemode.protect;
                    Color l = Color.Transparent;

                    switch (i)
                    {
                        case 0:
                            btmp = stop;
                            piecemode = piecemode.stop;
                            
                            break;
                        case 1:
                            btmp=protect; 
                            piecemode=piecemode.protect;
                            if (protected1 > 0)
                            {
                                l = Color.Aqua;
                            }
                            break;
                            case 2:
                            piecemode=piecemode.passive;
                            if (passive1 > 0)
                            {
                                l=Color.Aqua;
                            }
                            btmp=passive; break;
                            case 3:
                            piecemode=piecemode.agressive;
                            if(agressive1 > 0)
                            {
                                l=Color.Aqua;
                            }
                            btmp = aggressive;
                            break;
                    }
                    var g = Graphics.FromImage(btmp);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    //     picture.Image=;
                    picture.Height = panel3.Height / 3 - 7;
                    picture.Left = x;
                    picture.Top = y;
                    picture.Width = panel3.Width / 3 - 7;
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                    picture.Image = btmp;
                    panel3.Controls.Add(picture);
                    picture.BackColor = l;
                    x += picture.Width + 5;
                    //   picture.BackColor = Color.Red;
                    if (x >= panel3.Width / 3 * 3 - 7 * 3)
                    {
                        x = 7;
                        y += panel3.Height / 3 - 5;
                    }
                    picture.Click += delegate
                    {
                        changemode(selectedones, piecemode,pl);
                    };
                }
                return;
            }
            while (b.selected)
            {

                panel3.Controls.Clear();
                //let's fill panel3
                int x = 7;
                int y = 7;

                for (int i = 0; i < b.pieces_tobuild.Count; i++)
                {
                    var ass= b.pieces_tobuild[i];
                    var a = (piece)b.pieces_tobuild[i].clone();
                    a.z = 0;
                    PictureBox picture = new PictureBox();
                    //a.prepareresourcebitmap(gm);
                    var btmp = (Bitmap)a.load(gm, true).Clone();
                    var g = Graphics.FromImage(btmp);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    //     picture.Image=;
                    picture.Height = panel3.Height / 3 - 7;
                    picture.Left = x;
                    picture.Top = y;
                    picture.Width = panel3.Width / 3 - 7;
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                    drawhealth( 1 - (float)a.buildtime_ms / a.buildtime, picture.Width / 2 - 25, picture.Height / 2, 0, (int)(50*(a.width/80)),g);
                    picture.Image = btmp;
                    panel3.Controls.Add(picture);
                    x += picture.Width + 5;
                 //   picture.BackColor = Color.Red;
                    if (x >= panel3.Width / 3 * 3 - 7 * 3)
                    {
                        x = 7;
                        y += panel3.Height / 3 - 5;
                    }
                    picture.Click += delegate
                    {
                        b.removepiece(ass);
                    };
                }
                break;
                Thread.Sleep(100);
            }

        }
       public  building tobuild;
        public void fill_selection(Panel panel, item it, player pl)
        {
            //try if it is building
            try
            {
                Panel panel2 = null;
                Panel panel3 = null;
                foreach (Control control in panel.Controls)
                {
                    if (control.Name == "panel4")
                    {
                        panel2 = (Panel)control;
                        // break;
                    }
                    else if (control.Name == "panel3") { panel3 = (Panel)control; }
                }
                var b = (building)it;
                if (!b.available)
                {
                    return;
                }
                int left = panel2.Left + panel2.Width + 80;
                foreach (var a in b.piecesallowed)
                {
                    PictureBox pic = new PictureBox();
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                   // a.prepareresourcebitmap(gm);
                    pic.Image = (Bitmap)a.load(gm, true).Clone();
                    pic.Height = panel.Height;
                    pic.Left = left;
                    left += pic.Width + 10;
                  //  pic.BackColor = Color.Red;
                    pic.Click += delegate
                    {
                        b.adddpiece((piece)a.clone());
                        //    this.additem(a.clone());
                    };
                    panel.Controls.Add(pic);
                }
                foreach (var a in b.buildingsallowed)
                {
                    PictureBox pic = new PictureBox();
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                   // a.prepareresourcebitmap(gm);
                    pic.Image = (Bitmap)a.load(gm, true).Clone();
                    pic.Height = panel.Height;
                    pic.Left = left;
                    left += pic.Width + 10;
                  //  pic.BackColor = Color.Red;
                    pic.Click += delegate
                    {
                        tobuild = (building)a;
                        //    this.additem(a.clone());
                    };
                    panel.Controls.Add(pic);
                }
                //  Task.Run(() => {

                //}//);
            }
            catch {
                List<Control> controls = new List<Control>();
                foreach (Control c in panel.Controls)
                {
                    if (!c.Name.Contains("panel"))
                    {
                        controls.Add(c);
                    }
                }
                foreach (Control c in controls)
                {
                    panel.Controls.Remove(c);
                }

            }
        }
        List<item> selecteditems = new List<item>();
        public List<item> SelectedItemsGet()
        {
            var x = (new List<item>());//
            x.AddRange(selecteditems);
            return x;
        }
        public List<item> selectitems(player player, int fx, int fy, int mousex, int mousey)
        {
            int newx = Math.Max(fx + player.x, mousex + player.x);
            int newy = Math.Max(fy + player.y, mousey + player.y);
            int absx = Math.Min(fx + player.x, mousex + player.x);
            int absy = Math.Min(fy + player.y, mousey + player.y);
            var lista = new List<item>();
            for (int i = 0; i < gm.map.items.Count; i++)
            {
                var a = gm.map.items[i];
                if (a.type!=type.building&&a.type!=type.bullet&&a.x + a.width / 2 > absx && a.x + a.width / 2 < newx && a.y + a.height / 2 > absy && a.y + a.height / 2 < newy)
                {
                    a.selected = true;
                    lista.Add(a);
                }
                else
                {
                    a.selected = false;
                }
            }
            return lista;
        }
        public item match(int x, int y, player pl)
        {
            selecteditems.Clear();
            x += pl.x; y += pl.y;
            item xt = null;
            for (int i = 0; i < gm.map.items.Count; i++)
            {
                var it = gm.map.items[i];
                if (it.x <= x && it.y <= y && x < it.x + it.width && y < it.y + it.height)
                {
                    it.selected = true;
                    selecteditems.Add(it);
                    xt = it;
                    // return it;
                }
                else
                {
                    //  it.selected=false;
                }
            }
            return xt;
        }
        public void update(player pl)
        {
            gm.tick(pl);
           // gm.drawmap();
        }
        public System.Drawing.Bitmap todraw=null;
        long framenum = 0;
        DateTime startmilisecs;
        DateTime lastmillisecs;
        public  GameEngineManager GameEngineManager ;
        public void fill( player pl,ref string message,int resizingframe)
        {
            var bitmap = (Bitmap)gm.map.image.Bitmap.Clone();
            lastmillisecs =DateTime.Now;
            if(framenum == 0)
            {
                startmilisecs=DateTime.Now;
            }
           
            int width = gm.map.width_resolution;
            int height = gm.map.height_resolution;
            
            pl.width = width;
            pl.height = height;
            float dt = 1;// width / (double)gm.map.image.Bitmap.Width;
            var ld = gm.map.itemsclonned;
            
            float factow = gm.map.factorw;
            float factoh = gm.map.factorh;
            System.Drawing.Rectangle main = new Rectangle(0,0,width,height);
            var g = Graphics.FromImage(bitmap);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int shows=0;
            for (int i = 0; i < ld.Count; i++)
            {


                var a = ld[i];
                if (a.health < 0)
                {
                    continue;
                }
                float xx = (a.x * dt  - pl.x ) * factow-a.width*1*factow*0;// + a.width * dt * factow;
                float yy = (a.y * dt  - pl.y ) * factoh-a.height*1*factoh*0;// + a.height * dt * factow;
                System.Drawing.Rectangle rect2=new Rectangle((int)(xx),(int)(yy),(int)(a.width*factow),(int)(a.height*factoh));
                
                if (rect2.IntersectsWith(main)&&!(a.stealth&&a.army.teamid!=pl.army.teamid&&true))//hide stealth units
                {
                    a.loadframe.loadframes = (int)framenum;
                    var btmp= a.load(gm);
                    xx -= (btmp.Width - a.width) / 2;
                    yy -= (btmp.Height - a.height) / 2;
                    int negx = (int)(a.width * a.z / 20 / 2 * 3*factow);//بيغير موضع الرسمة علشان الطيران
                    int negy = (int)(a.height * a.z / 20 / 2 * 3*factoh);
                    if (a.type != type.building)
                    {
                        xx -= 5;
                        yy -= 5;
                    }
               //     g.TranslateTransform(xx+a.width/2 - negx, yy+a.height/2 - negy);
               //     g.TranslateTransform(-a.width/2,-a.height/2);
                    draw( btmp, (int)(xx +a.width* Math.Tan(a.direction)*0) , (int)(yy+a.height*Math.Tan(a.direction)*0) , (int)a.z,g);
                    //    g.ResetTransform();
                    //  sp.Stop();
                    if (!a.available&&!a.selected)
                    {
                        drawhealth(1-(a.buildtime_ms+0.0 )/ a.buildtime, (int)((a.x - 15 - pl.x + (int)a.width / 2) * factow), (int)((a.y - pl.y + (int)a.height / 2 - 15) * factoh), 0, (int)(50 * factow * a.width / 150), g,true);

                    }
                    if (a.selected)
                    {
                        drawhealth( a.health / a.maxhealth, (int)((a.x - 15 - pl.x + (int)a.width / 2)*factow), (int)((a.y - pl.y + (int)a.height / 2 - 15)*factoh), 0, (int)(50* factow*a.width/150),g);
                    }
                    shows++;
                    
                }
                else
                {

                }
               
               
            }
            
            //sp.Stop();
            //explosions and fire
            for(int i=0;i<gm.squaresofinterest.Count;i++)
            {
                try
                {
                    var sqrc = gm.squaresofinterest[i];
                    float fact = (float)(sqrc.Rockettail / 50.0);
                    int sz = (int)(fact * 10);
                    // sqrc.Rockettail++;
                    if (sz > 0&&fact<.9f)
                    {
                        for (int u = 0; u < 1; u++)
                        {
                            Rectangle rect = new Rectangle((int)(sqrc.x+1+u) * gm.map.mod - pl.x - sz / 2+ gm.map.mod/2*0, (int)(sqrc.y + 1 + u * 0) * gm.map.mod - pl.y - sz / 2 + gm.map.mod / 2*0, sz, sz);
                            g.FillEllipse(Brushes.DarkRed, rect);
                        }
                    }
                    fact = (float)(sqrc.Explosion / 150.0);
                    sz = (int)((1 - fact) * 50);
                    if (sz > 0 && sqrc.Explosion > 0)
                    {

                        Brush b = new SolidBrush(Color.FromArgb(120-(int)(sz/50.0*100), 250, 50, 0));
                        
                        Rectangle rect = new Rectangle((int)(sqrc.x+0) * gm.map.mod+ gm.map.mod/2 - pl.x - sz / 2, (int)(sqrc.y+0) * gm.map.mod - pl.y - sz / 2+ gm.map.mod/2, sz, sz);
                        g.FillEllipse(b, rect);
                    }
                    if (sqrc.thinpasses > 1)
                    {
                        Brush b = new SolidBrush(Color.White);
                        Rectangle rect = new Rectangle((int)(sqrc.realxx + 0)  - pl.x , (int)(sqrc.realyy + 0)  - pl.y  , 3, 3);
                        //g.FillEllipse(b, rect);

                    }
                }
                catch (Exception ex){ }
            }
            for (int i = 0; i < ld.Count; i++)
            {
                var a = ld[i];
                double xx = (a.targetx * dt - pl.x)*factow ;
                double yy = (a.targety * dt - pl.y)*factoh ;
                if (a.image == "tank.png")
                {
                    //   pic.Image = a.load(gm);
                }
                if (xx > -0 && xx < width && yy > -0 && yy < height)
                {
                    
                    if (resizingframe % 2 == 0 && a.selected && a.army.owner == pl)
                    {
                        if (a.targetx != -1 && a.target == null && a.walk)
                        {
                          //  gm.drawsquare(bitmap, (int)a.targetx - pl.x, (int)a.targety - pl.y, Color.LightBlue, 20, true);
                          //  gm.drawsquare(bitmap, (int)a.targetx - pl.x+5, (int)a.targety - pl.y+5, Color.LightGreen, 10, true);

                            gm.drawstring(bitmap, "⛯", (int)((a.targetx - pl.x-15)*factow), (int)((a.targety - pl.y-15)*factoh), Color.LightGreen, 15);
                        }
                         if (a.target != null)
                        {
                            gm.drawstring(bitmap, "⛯", (int)((a.targetx - pl.x - 15) * factow), (int)((a.targety - pl.y - 15) * factoh), Color.Red, 15);

                            //⚔️
                     //       gm.drawstring(bitmap, "⛯󠁧󠁢󠁥󠁮󠁧󠁿", (int)((a.target.x + a.target.width / 2 - pl.x) * factow), (int)((a.target.y + a.target.height / 2 - pl.y) * factoh), Color.Red, 30);

                           // gm.drawsquare(bitmap, (int)((a.target.x + a.target.width / 2 - pl.x)*factow), (int)((a.target.y + a.target.height / 2 - pl.y)*factoh), Color.Red, 10, true);

                        }
                    }
                }
                if (a.selected && a.walk && framenum % 1 == 0&&a.army.owner==pl)//01010420095//
                {
                    //draw path squares
                    for (int ii = 0; ii < a.pathsquares.Count; ii++)
                    {
                        System.Drawing.Color cl = Color.FromArgb(100, 0, 0, 255);
                        var sqrc = a.pathsquares[ii];
                        if (ii == a.pointer + 1)
                        {
                            cl = Color.FromArgb(100, 255, 0, 0);
                            //  cl = Color.Red;
                        }
                        var xxx = (int)sqrc.x * gm.map.mod + 0f;// - pl.x;
                        var yyy = (int)sqrc.y * gm.map.mod + 0f;// - pl.y;
                        xxx = (xxx * dt - pl.x) * factow;// + a.width * dt * factow;
                        yyy = (yyy * dt - pl.y) * factoh;// + a.height * dt * factow;

                        if (xxx > -0 && xxx < width && yyy > -0 && yyy < height)
                        {
                            gm.drawsquare(bitmap, (int)xxx, (int)yyy, cl, 10, true);
                            gm.drawsquare(bitmap, (int)(xxx + 2 * factow), (int)(yyy + 2 * factoh), Color.Yellow, 5, true);
                        }

                    }
                }

            }
            if (tobuild != null)
            {
                
                var b=tobuild.clone();
                b.stealth = true;
                b.loadframe.opacity = 0.5f;
                var btmp = b.load(gm);
                draw(btmp, (int)(GameEngineManager.mousex-b.width/2), (int)(GameEngineManager.mousey-b.height/2), (int)b.z, g);
            }
            // drawnight(g, bitmap, 0);
            if ((lastmillisecs - onesecdate).TotalMilliseconds >= 1000)
            {
                onesecdate = lastmillisecs;
                shotframe = framenum-oldframe;
                oldframe = framenum;
            }
            //night and light effects
            //   makenormallight(g, 400, 200, 400, 400);
            //   drawnight(g, bitmap, 1.9f);
            gm.drawstring(bitmap, $"{Math.Round(shotframe+0.0, 2)} fps objects on screen : {shows} out of {ld.Count}", 0, 0, Color.Aqua, 10);
            gm.drawstring(bitmap, $"silver : {pl.silver}", 400, 0, Color.LightGreen, 10);
            gm.drawstring(bitmap, $"gold : {pl.gold}", 750, 0, Color.Gold, 10);
            if (message != ""&& fullmessage < 6)
            {
                gm.drawstring(bitmap, $"{message}", 0, 20, Color.Red, 10);
            }
            if (message != ""&&framenum%50==0)
            {
                fullmessage++;
            }
            if (fullmessage > 6)
            {
             //   fullmessage = 0;
               // message = "";
            }
           
            // gm.drawstring(bitmap, $"{Math.Round(shotframe+0.0, 2)} fps", 0, 0, Color.Aqua, 10);
            if (pl.fx != -1)
            {
                gm.drawmousselction(bitmap, (int)(pl.fx*factow), (int)(pl.fy*factoh), (int)(pl.mousex*factow), (int)(pl.mousey*factoh));
            }
            
             g = null;
            
            todraw = bitmap;
            framenum++;
        }
        void makenormallight(Graphics g,int x,int y,int w,int h)
        {
            GraphicsPath ellipsePath = new GraphicsPath();
            ellipsePath.AddEllipse(x, y, w, h);
            g.SetClip(ellipsePath, CombineMode.Exclude);//light
        }
        void drawnight(Graphics g,Bitmap bitmap,float nightfactor)
        {
            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Color ccl = Color.FromArgb((int)(125*nightfactor), 0, 0, 0);
        //    ccl = Color.FromArgb((int)(200), 250, 250, 150);//bomb effect
            g.FillRectangle(new SolidBrush(ccl), rectangle);
        }
        public long shotframe;
        public int fullmessage;
        long oldframe;
        DateTime onesecdate;
        public void drawhealth( double ratio, int x, int y, int z, int size,Graphics g,bool buildin=false)
        {
            //  
            //    var area = new Rectangle(0, 0, bitmp2.Width / 2, bitmp2.Height / 2);
            //  g.FillRectangle(new LinearGradientBrush(area, Color.PaleGoldenrod, Color.OrangeRed, 45), area);
            ratio = Math.Min(0.995, ratio);

            //  var width2 = ratio * 100; 
            Color cl=Color.Red;//=  Color.FromArgb(170, (byte)(255 * (1 - width / 100)), (byte)(155 * (width / 100)), 0);
            if (buildin)
            {
                cl = Color.FromArgb(255,205,195,0);
                //ratio=Math.Max(0.2, ratio);
            }
            var width = ratio * 100;
            if (!buildin)
            {
                cl = Color.FromArgb(170, (byte)(255 * (1 - width / 100)), (byte)(155 * (width / 100)), 0);
            }
            g.DrawRectangle(Pens.White, x - size / 4, y, (int)size, (float)size / 50 * 8 + 1);

            var b = new SolidBrush(cl);
            g.FillRectangle(b, x + 1-size/4, y + 1, (int)(width* ((float)size/50) ) / 2-1 , (float)size / 50 * 8);

        }
        public void draw( Bitmap bitmp2, int x, int y, int z,Graphics g)
        {
            //  int width = 800, height = 600;

            // var g = Graphics.FromImage(bitmp1);

            //  g.SmoothingMode = SmoothingMode.AntiAlias;
            //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //  var area = new Rectangle(0, 0, bitmp2.Width/2, bitmp2.Height/2);
            //  g.FillRectangle(new LinearGradientBrush(area, Color.PaleGoldenrod, Color.OrangeRed, 45), area);
          //  bitmp2 = resource.SetOpacity(bitmp2, 0.8f);
            g.DrawImage(bitmp2, new Point(x, y));
       //     g.Dispose();
        }
        public void makeorder(int orderid, double ordervalue, int tag)
        {

        }
        public void stopwalk(item item)
        {
            makeorder(0, 0, 1);
            item.walk = false;
            item.targetx = -1;
            item.targety = -1;
            item.target = null;
           // item.mode = piecemode.passive;
            item.pathsquares.Clear();
            item.orderid = item.orderid/2;
        }
        List<item> workers = new List<item>();
        int countworkers(army army)
        {
            workers.Clear();
            int ans = 0;
            for(int i = 0; i < gm.map.items.Count; i++)
            {
                var item = gm.map.items[i];
                
                if (item.army == army && item.type == type.worker)
                {
                    workers.Add(item);
                    ans++;
                }
            }
            return ans;
        }
        Random rd = new Random();
        public bool additem(item item)
        {
            //   item.x += rd.Next(-30, 30);
            //   item.y += rd.Next(-30, 30);
            makeorder(0, 0, 0);
            item.orderid=gm.map.items.Count+1;
            item.id=gm.map.items.Count+1;
              var i=countworkers(item.army);
            if (i >= item.workersrequired)
            {
                for(int j = 0; j < item.workersrequired&&item.workersrequired>0; j++)
                {
                    item.addworker(workers[j]);
                    workers[j].health = -1;
                }

                gm.map.items.Add(item);
                return true;
            }
            else
            {

                GameEngineManager.message = "workers needed !";
                return false;
            }
           
        }

        public void change_direction(item item, int x, int y, player pl)
        {
            item.resettarget = false;
            
            item.targetx = x; item.targety = y;
            item.minitargetx = x; item.minitargety = y;
            var xx = item.x + item.width / 2;
            var yy = item.y + item.height / 2;
            var xy = item.basespeed;// MathF.Sqrt(item.speedx *item.speedx + item.speedy * item.speedy);
            var xy2 = MathF.Sqrt((x - xx) * (x - xx) + (y - yy) * (y - yy));
            item.newspeedx = xy * (x - xx) / xy2;
            item.newspeedy = xy * (y - yy) / xy2;
        }
        public void change_direction_direct(item item, int x, int y)
        {

            var xx = item.x + item.width / 2;
            var yy = item.y + item.height / 2;
            var xy = item.basespeed;// MathF.Sqrt(item.speedx *item.speedx + item.speedy * item.speedy);
            var xy2 = MathF.Sqrt((x - xx) * (x - xx) + (y - yy) * (y - yy));
            xy2 = MathF.Max(0.0001f, xy2);
            item.newspeedx = xy * (x - xx) / xy2;
            item.newspeedy = xy * (y - yy) / xy2;
        }
    }
 

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
                        GameEngine.gm.drawmap(enginespeed,player);
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
        bool imsleepy = false;
        string msg;
        public string message {
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

                    GameEngine.fill(player, ref msg, resizingframe);
                    imsleepy = true;

                    //  Thread.Sleep(1);
                    if (i % 3 == 0)
                    {
                        Thread.Sleep(1);
                    }
                    if (false)
                    {

                        Thread.Sleep(50);
                    }

                    if (i % restnum == 0)
                    {


                        Thread.Sleep(20);
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
                    GameEngine.update(player);

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
                notcollecting= true;
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
                        Task.Run(()=>collect());
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
                            Thread.Sleep(10);
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
        public item selected;
        public building selected_b;
        item a;
        public void cancelselection()
        {
            foreach (var it in GameEngine.gm.map.items)
            {
                it.selected = false;
            }
        }
        square getnextsquare(int x, int y, item it, int orderid, int plusorminusx,int plusorminusy)
        {
            square square = null;// = new square();
            int modx = ((int)it.squarewidth) / GameEngine.gm.map.mod + 2;
            x = x / GameEngine.gm.map.mod;
            y = y / GameEngine.gm.map.mod;
            var xlen = GameEngine.gm.map.xlen;
            var ylen = GameEngine.gm.map.ylen;
            //for (  i += modx * plusorminusx)
            {
                for (int j = 0, i = 0; j + y < ylen && j + y >= 0&& i + x < xlen && x + i >= 0; j += modx * plusorminusy, i += modx * plusorminusx)
                {
                    var square2 = GameEngine.gm.map.squares[x + i, y + j];
                    if (square2.issitastarget != orderid&&square2.isavailable(it,null)&&it.checksurroundings(square2,x+i,y+j,null,null))
                    {
                        if(square2.piecethere!=null && square2.piecethere.type == type.building)
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
            public comparesquares(int mousex,int mousey,int mod)
            {
                targetx=mousex/mod;
                targety=mousey/mod;
            }
            public int Compare(square x, square y)
            {
                float dist1=(float)Math.Abs(Math.Abs(x.x - targetx)+Math.Abs(x.y-targety));
                float dist2=(float)Math.Abs(Math.Abs(y.x - targetx) + Math.Abs(y.y - targety));
                return -dist1.CompareTo(dist2);
            }
        }

        public void sendorder()
        {
            
            order order = new order();
          //  GameEngine.selectitems(player, fx, fy, mousex, mousey);
            item fv = GameEngine.match(mousex, mousey, player);
            if (selected == null)
            {
                //a = GameEngine.match(mousex, mousey, player);
            }
            int ise = 0;
            //x += pl.x; y += pl.y;
            int nextx = mousex+player.x;
            int nexty = mousey+player.y;
            int mx=mousex+player.x;
            int my=mousey+player.y;
            if (GameEngine.tobuild != null)
            {
                var xxg = player.silver - GameEngine.tobuild.silver;
                if (xxg < 0)
                {
                    message = "Insuffcient funds ";
                    return;
                }

                var xb=GameEngine.tobuild.clone();
                xb.available = false;
                xb.x = mx - xb.width / 2;
                xb.y = my - xb.height / 2;
                xb.health = 1;
                int w = 0;
                while (engineworking)
                {
                    if (w % 50==0) 
                    {
                        Thread.Sleep(1);
                    }
                    w++;
                }
                xb.canceledx = false;
                xb.canceledy = false;
                GameEngine.requestchangeposition(xb);
                if (xb.canceledx||xb.canceledy)
                {
                    message = "Inavailable place to build";
                    return;
                }
                
               var b=  GameEngine.additem(xb);
                if (b)
                {
                    player.silver = xxg;
                    GameEngine.tobuild = null;
                }
            }
            int plusorminusx = 1;
            int plusorminusy = 1;
            int time=0;
            void getnext()
            {
                switch (time)
                {
                    case 0:
                        plusorminusx=1;
                        plusorminusy=1;
                        break;
                    case 1:
                        plusorminusx=1;
                        plusorminusy=-1;
                        break;
                    case 2:
                        plusorminusx=-1;
                        plusorminusy=1;
                        break;
                    case 3:
                        plusorminusx=1;
                        plusorminusy=0;
                        break;
                    case 4:
                        plusorminusx=0;
                        plusorminusy=1;
                        break;
                    case 5:
                        plusorminusx=-1;
                        plusorminusy=-1;
                        break;
                    case 6:
                        plusorminusx=-1;
                        plusorminusy=0;
                        break;
                    case 7:
                        plusorminusx=0;
                        plusorminusy=-1;
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
            int cx = mx;int cy= my;
            void getnextsq()
            {
                if(selectedsquares.Count > iss) {
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
                if (it.selected&&it.army.owner==player&&it!=fv&&it.type!=type.building)
                {
                    if (fv == null)
                    {

                        square s = null;// getnextsquare(mx, my, it, order.orderid,plusorminus);
                        int counts = 0;
                        
                        while (counts<8)
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

                                s = getnextsquare(cx, cy, it, order.orderid, plusorminusx,plusorminusy);
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
            for(int i = 0; i < selecteditems.Count; i++)
            {
                centerx += selecteditems[i].x;
                centery += selecteditems[i].y;
            }
            centerx/=selecteditems.Count;
            centery/=selecteditems.Count;
            comparesquares comp=new comparesquares((int)centerx, (int)centery,GameEngine.gm.map.mod);
            selectedsquares.Sort(comp);
            int pointt = 0;
            
            while (selecteditems.Count > 0)
            {
                item topcloses = null;
                float topdist=float.MaxValue;
                int dx = (int)selectedsquares[pointt].x*GameEngine.gm.map.mod;
                int dy = (int)selectedsquares[pointt].y*GameEngine.gm.map.mod;
                for(int i=0;i<selecteditems.Count;i++)
                {
                    var dist = selecteditems[i].getdistance(dx,dy);
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
                    selected.selected = false;
                    selected = null;
                }
            }
            fx = -1;
            fy = -1;
            orders.Add(order);
            if (selected!=null&& selected.type == type.building)
            {
                selected_b = (building)selected;
               // selected = null;
            }
            else
            {
                selected_b=null;
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
