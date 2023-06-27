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
            pc.buildtime_ms = 170;
            pc.buildtime = 170;
            pc.army = player.army;
            pc.image = "tank.png";
            pc.name = "tank";
            pc.width = 50;
            pc.height = 80;
            pc.speedx = 0f;
            pc.newspeedx = 0.0f;
            pc.speedy = -0.01f;
            pc.basespeed = 3.5f;
            pc.emergencyspeed = 14f;
            pc.newspeedy = 0.0001f;
            pc.power = 20;
            pc.maxpower = 3;
            pc.basicdirection = -1;
            pc.engine = this;
            pc.shot_time_ms = 30;
            pc.reloadtime_ms = 100;
            pc.maxbullets = 3;
            pc.health = 120;
            pc.maxhealth = 120;
            pc.x = 5600;
            pc.y = 5200;
            pc.silver = 650f;
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
            pc.targettype = generaltype.infantry;
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
            pc.power = 5.5f;
            pc.maxpower = 2.5f;
            pc.basicdirection = -1;
            pc.engine = this;
            pc.shot_time_ms = 1;
            pc.reloadtime_ms = 5;
            pc.maxbullets = 10;
            pc.health = 20;
            pc.maxhealth = 20;
            pc.x = 5600;
            pc.y = 5200;
            pc.silver = 400;
            //    pc.z = 20;
            pc.type = type.vehicle;
            pc.track = tracktype.full;
            pc.type = type.vehicle;
            pc.rangeofattack = 300f;
            pc.targettype = generaltype.infantry;
            pc.change = 0.3f;
            //pc.onlyattacktarget = true;
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
            pc.silver = 400;
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
            bullet.basespeed = 15;
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
          //  bullet.onlyattacktarget = true;
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
            worker.comment = "worker";
            worker.name = "worker";
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
            worker.x = 1700;
            worker.y = 500;
            worker.health = 10;
            worker.maxhealth = 10;
            worker.track = tracktype.full;
            //make worker able to attack
            worker.shot_time_ms = 30;
            worker.reloadtime_ms = 20;
            worker.power = 0;
            worker.maxbullets = 0;
            worker.rangeofattack = 0;
            worker.silver = 100;
            worker.imagesofanimations.Add(0, "animations/h2.png");
            worker.imagesofanimations.Add(1, "animations/h3.png");
            worker.imagesofanimations.Add(2, "animations/dead1.png");
            worker.imagesofanimations.Add(3, "animations/dead2.png");
            worker.imagesofanimations.Add(4, "animations/dead3.png");
            return worker;
        }
        piece createhelicopter(player player)
        {
            piece worker = new piece();
            worker.type = type.air;
            worker.comment = "helicopter";
            worker.name = "helicopter";
            worker.patience = 140;
            worker.generaltype = generaltype.plane;
           // worker.type = type.worker;
            worker.salary = 6;
            worker.basespeed = 16;
            worker.image = "helicopterx2.png";
            worker.width = 100;
            worker.height = 150;
            worker.walklengthanimation = 17;
            worker.buildtime = 200;
            worker.buildtime_ms = 200;
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
            worker.shot_time_ms = 5;
            worker.reloadtime_ms = 50;
            worker.power = 20;
            worker.maxbullets = 3;
            worker.rangeofattack = 350;
            worker.silver = 900;
            worker.workersrequired = 3;
            worker.alwayswalk = true;
            worker.targettype = generaltype.vehicle;
            worker.walkanimationnums = 2;
         //   worker.change = 0.95f;
            worker.imagesofanimations.Add(0, "animations/helicopterx1.png");
            worker.imagesofanimations.Add(1, "animations/helicopterx2.png");
            // worker.imagesofanimations.Add(2, "animations/helicopter3.png");
            // worker.imagesofanimations.Add(3, "animations/helicopter4.png");
            //  worker.imagesofanimations.Add(4, "animations/helicopter5.png");
            //  worker.imagesofanimations.Add(5, "animations/helicopter6.png");
            //  worker.imagesofanimations.Add(6, "animations/helicopter7.png");
            //  worker.imagesofanimations.Add(7, "animations/helicopter8.png");
            //   worker.imagesofanimations.Add(2, "animations/helicopter3.png");
            //  worker.imagesofanimations.Add(2, "animations/dead1.png");
            //  worker.imagesofanimations.Add(3, "animations/dead2.png");
            //  worker.imagesofanimations.Add(4, "animations/dead3.png");
            worker.change = 0.25f;
            worker.poswidth = 5;
            worker.posheight = 10;
            return worker;
        } 
        piece createhelicopter2(player player)
        {
            piece worker = new piece();
            worker.type = type.air;
            worker.comment = "helicopter";
            worker.name = "sting helicopter";
            worker.patience = 140;
            worker.generaltype = generaltype.plane;
           // worker.type = type.worker;
            worker.salary = 6;
            worker.basespeed = 24;
            worker.image = "helicopter1.png";
            worker.width = 200;
            worker.height = 200;
            worker.walklengthanimation = 3;
            worker.buildtime = 150;
            worker.buildtime_ms = 150;
            worker.stealth = false;
            worker.owner = player;
            worker.army = player.army;
            worker.engine = this;
            worker.x = 5700;
            worker.y = 5000;
            worker.health = 13;
            worker.maxhealth = 13;
            worker.track = tracktype.full;
            //make worker able to attack
            worker.shot_time_ms = 5;
            worker.reloadtime_ms = 50;
            worker.power = 26;
            worker.maxbullets = 3;
            worker.rangeofattack = 350;
            worker.silver = 1100;
            worker.workersrequired = 3;
            worker.alwayswalk = true;
            worker.targettype = generaltype.vehicle;
            worker.walkanimationnums = 8;
         //   worker.change = 0.95f;
            worker.imagesofanimations.Add(0, "animations/helicopter1.png");
            worker.imagesofanimations.Add(1, "animations/helicopter2.png");
            worker.imagesofanimations.Add(2, "animations/helicopter3.png");
            worker.imagesofanimations.Add(3, "animations/helicopter4.png");
            worker.imagesofanimations.Add(4, "animations/helicopter5.png");
            worker.imagesofanimations.Add(5, "animations/helicopter6.png");
            worker.imagesofanimations.Add(6, "animations/helicopter7.png");
            worker.imagesofanimations.Add(7, "animations/helicopter8.png");
            //worker.imagesofanimations.Add(2, "animations/helicopter3.png");
        //    worker.imagesofanimations.Add(2, "animations/dead1.png");
          //  worker.imagesofanimations.Add(3, "animations/dead2.png");
          //  worker.imagesofanimations.Add(4, "animations/dead3.png");
            worker.change = 0.45f;
            worker.poswidth = 5;
            worker.posheight = 10;
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
            worker.name = "rbg man";
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
            worker.imagesofanimations.Add(0, "rpg.png");
            worker.imagesofanimations.Add(1, "rpg.png");
            worker.imagesofanimations.Add(2, "animations/dead1.png");
            worker.imagesofanimations.Add(3, "animations/dead2.png");
            worker.imagesofanimations.Add(4, "animations/dead3.png");
            worker.change = 0.9f;
            worker.onlyattacktarget = true;
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
            worker.power = 20;
            worker.maxbullets = 1;
            worker.rangeofattack = 750;
            worker.silver = 700;
            worker.basicdirection = 270;
            worker.armedbullet = bullet;
            worker.workersrequired = 1;
            worker.type = type.soldier;
            worker.change = 0.9f;
            worker.name = "sniper";
            //worker.imagesofanimations.Add(0, "h2.png");
            //worker.imagesofanimations.Add(1, "h3.png");
            worker.imagesofanimations.Add(0, "sniper.png");
            worker.imagesofanimations.Add(1, "sniper.png");
            worker.imagesofanimations.Add(2, "animations/dead1.png");
            worker.imagesofanimations.Add(3, "animations/dead2.png");
            worker.imagesofanimations.Add(4, "animations/dead3.png");
            return worker;
        }
        piece createsniperbullet(piece thinbullet)
        {
            var p = (piece)thinbullet;
            p.comment = "sniper bullet";
            p.onlyattacktarget = true;
            p.basespeed = 150f;
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
            warfactory.name = "war factory";
            warfactory.buildtime_ms = 500*5;
            warfactory.buildtime = 500*5;
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
            warfactory.name = "barracks";
            warfactory.buildtime_ms = 250 * 3;
            warfactory.buildtime = 250 * 3;
            warfactory.posx = 70;
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
            warfactory.width = 150;
            warfactory.height = 150;
            warfactory.owner = player;
            warfactory.engine = this;
            warfactory.health = 60;
            warfactory.stealth = false;
            warfactory.maxhealth = 60;
            warfactory.type = type.building;
            warfactory.available = true;
            warfactory.army = player.army;
            warfactory.image = "house.png";
            warfactory.buildtime_ms = 90*2;
            warfactory.buildtime = 90*2;
            warfactory.posx = 50;
            warfactory.posy = 70;
            warfactory.silver = 200;
            warfactory.workersrequired = 2;
            warfactory.piecesallowed.Add(worker);
            warfactory.autobuildms = 300;
            warfactory.salary = 4;
            return warfactory;
        }
        building createstoragehouse(player player)
        {
            building warfactory = new building();
            warfactory.name = "storage house";
            warfactory.type = type.building;
            warfactory.generaltype= generaltype.building;
            warfactory.width = 160;
            warfactory.height = 160;
            warfactory.owner = player;
            warfactory.engine = this;
            warfactory.health = 60;
            warfactory.stealth = false;
            warfactory.maxhealth = 60;
            warfactory.type = type.building;
            warfactory.available = true;
            warfactory.army = player.army;
            warfactory.image = "storage.png";
            warfactory.buildtime_ms = 250*3;
            warfactory.buildtime = 250*3;
            warfactory.posx = 70;
            warfactory.posy = 30;
            warfactory.silver = 700;
            warfactory.workersrequired = 3;
            warfactory.autobuildms = 300;
            warfactory.salary = 4;
            return warfactory;
        }
        building createfarm(player player)
        {
            building warfactory = new building();//grass
            warfactory.name = "farm";
            warfactory.comment = "farm";
            warfactory.requiredzonename = "grass";
            warfactory.type = type.building;
            warfactory.generaltype = generaltype.building;
            warfactory.width = 290;
            warfactory.height = 290;
            warfactory.owner = player;
            warfactory.engine = this;
            warfactory.health = 150;
            warfactory.stealth = false;
            warfactory.maxhealth =150;
            warfactory.type = type.building;
            warfactory.available = true;
            warfactory.army = player.army;
            warfactory.image = "farm2.png";
            warfactory.buildtime_ms = 125*5;
            warfactory.buildtime = 125*5;
            warfactory.posx = (int)(147*290/190.0);
            warfactory.posy = (int)(60*290/190.0);
            warfactory.silver = 350;
            warfactory.workersrequired = 5;
            warfactory.autobuildms = 300;
            warfactory.salary = 6;
            warfactory.foodproduction = 1000;
            return warfactory;
        }
        building createGoldmine(player player)
        {
            building warfactory = new building();
            warfactory.requiredzonename = "gold";
            warfactory.name = "gold mine";
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
            warfactory.buildtime_ms = 2000;
            warfactory.buildtime = 2000;
            warfactory.posx = 85;
            warfactory.posy = 30;
            warfactory.silver = 1500;
            warfactory.producesmoney = 25;
            warfactory.owner = player;  
            warfactory.workersrequired = 10;
            warfactory.salary = 15;
            return warfactory;
        }
        mapzone creategoldzone()
        {
            mapzone mp = new mapzone("gold", "gold.png",gm.map.mod *4, gm.map.mod *4,this);
            mp.x = 1500; mp.y = 1000;
            mp.refusemessage = "Gold mine must be built on gold zone";
            return mp;
        }
        mapzone creategrasszone()
        {
            mapzone mp = new mapzone("grass", "grass.png",gm.map.mod*4,gm.map.mod*4,this);
            mp.x = 2500; mp.y = 1000;
            mp.refusemessage = "farm must be built on grass zone";
            mp.imagesofanimations.Add(1, "grass_short.png");
            mp.imagesofanimations.Add(2, "grass_full.png");
            return mp;
        }
        void fillzone(map mp,mapzone goldzone)
        {
            var gz = goldzone.width/gm.map.mod;
            for (int i = 0; i < 60; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    var g2 = (mapzone)goldzone.clone();
                    g2.x = goldzone.x + i * mp.mod;
                    g2.y = goldzone.y + j * mp.mod;
                    if (i%gz == 0 && j %gz== 0)
                    {
                        mp.addmapzone(g2);
                    }
                }
            }
        }
        public float progress;
        public void init(int widthresolution,int heightresolution,int realwidth,int realheight,Panel panel3)
        {
            map mp = new map("desert2", 15000, 15000, "desert.jpg", widthresolution, heightresolution,this);
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
            building.x = 500;
            building.y = 500;
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
            building.comment = "cmd";
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
            var farm = createfarm(player);
            var sniperbullet = createsniperbullet(thinbullet);
            var sniper = createsniper(player,sniperbullet);
            var rbg = createrbg(player, bullet);
            var storagehouse = createstoragehouse(player);
            var goldzone = creategoldzone();
            var grasszone = creategrasszone();
            var helicopterstealth=createhelicopter(player);
            helicopterstealth.silver = 1300;
            helicopterstealth.name = "Stealth helicopter";
            var helicopter=createhelicopter(player);
            var helicopter2=createhelicopter2(player);
            helicopter.stealth = false;
            helicopter.buildtime_ms = 150;
            helicopter.buildtime = 150;
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
            mp.asstes.Add(storagehouse);
            mp.asstes.Add(goldzone);
            mp.asstes.Add(grasszone);
            mp.asstes.Add(farm);
            mp.asstes.Add(helicopter);
            mp.asstes.Add(helicopter2);
            mp.asstes.Add(helicopterstealth);
            godlmine.favoritemapzone = goldzone;
            farm.favoritemapzone = grasszone;
            building house = createhouse(player, worker);
            mp.asstes.Add(house);
            var xp = pc;
            warfactory.piecesallowed.Add(automachine);
            warfactory.piecesallowed.Add(humvee);
            warfactory.piecesallowed.Add(xp);
            warfactory.piecesallowed.Add(helicopter);
            warfactory.piecesallowed.Add(helicopterstealth);
            warfactory.piecesallowed.Add(helicopter2);
            building.buildingsallowed.Add(house);
            building.buildingsallowed.Add(storagehouse);
            building.buildingsallowed.Add(farm);
            building.buildingsallowed.Add(godlmine);
            building.buildingsallowed.Add(barraks);
            building.buildingsallowed.Add(warfactory);
           // building.piecesallowed.Add(worker);
            mp.load_resources(realwidth, realheight);
            fillzone(mp, goldzone);
            fillzone(mp, grasszone);
            
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
            progress += 1f;
            passive = new Bitmap("assets/passive.png");
            progress += 1f;
            protect = new Bitmap("assets/protect.png");
            progress += 1f;
            aggressive = new Bitmap("assets/aggressive.png");
            progress += 1f;
            Size sz=new Size(panel3.Width/3-5,panel3.Height/3-5);
            stop = resource.ResizeImage(stop, sz);
            progress += 1f;
            passive = resource.ResizeImage(passive, sz);
            progress += 1f;
            aggressive = resource.ResizeImage(aggressive, sz);
            progress += 1f;
            protect = resource.ResizeImage(protect, sz);
            progress += 1f;
            progress += 2f;
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
                if (it.health > 0)
                {
                    it.firehit(newxx, newyy);
                }
                
                if (it.health < 0)
                {
                    return;
                }
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
        public Random random=new Random();
        public void loadselection(item it, Panel panel3,player pl)
        {
            building b = null;
           
            try
            {
                b = (building)it;
                if (it == null||pl!=b.owner||it.army!=pl.army)
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
                panel3.Controls.Clear();
                if (selectedones.Count==0) {
                    return;
                }
                int x = 7;
                int y = 7;
               
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
                    picture.BorderStyle= BorderStyle.FixedSingle;
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

                
                //let's fill panel3
                int x = 7;
                int y = 7;
                List<Control> cls = new List<Control>();
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
                    picture.BorderStyle= BorderStyle.FixedSingle;
                    picture.SizeMode = PictureBoxSizeMode.Zoom;
                    drawhealth( 1 - (float)a.buildtime_ms / a.buildtime, picture.Width / 2 - 25, picture.Height / 2, 0, (int)(50*(172.0/80)),g);
                    picture.Image = btmp;
                    
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
                    cls.Add(picture);
                }
                panel3.Controls.Clear();
                panel3.Controls.AddRange(cls.ToArray());
                break;
                Thread.Sleep(100);
            }

        }
       public  building tobuild;
        public Panel ppanel;
        public item lastitem;
        void clear(player pl)
        {
            var panel = ppanel;
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
            fill_selection(panel, null,pl );
        }
        public void fill_selection(Panel panel, item it, player pl)
        {
            if (it == null) 
            {
                return;
            }
            if (it == lastitem)
            {
                return;
            }
            lastitem = it;
            //try if it is building
            ppanel = panel;
            try
            {
               
                if ((!it.selected&&it.comment!="cmd") || it.dead || it.army != pl.army)
                {
                    return;
                }
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
                int left = panel2.Left + panel2.Width + 30;
                //panel.Controls.Clear();
                //panel.Controls.Clear();
                List<Control> cls = new List<Control>();
                foreach (var a in b.piecesallowed)
                {
                    PictureBox pic = new PictureBox();
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                   // a.prepareresourcebitmap(gm);
                    var btmp = (Bitmap)a.load(gm, true).Clone();

                    pic.Width = 150;
                    pic.Top = 7;
                    pic.Height = panel.Height - 15;
                    pic.Left = left;
                    left += pic.Width + 10;
                    pic.BackColor = Color.Wheat;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                   
             
                    pic.Click += delegate
                    {
                        b.adddpiece((piece)a.clone());
                        //    this.additem(a.clone());
                    };
                    btmp = a.load2(btmp, pic.Width, pic.Height);
                    pic.Image = btmp;
                    cls.Add(pic);
                }
                foreach (var a in b.buildingsallowed)
                {
                    PictureBox pic = new PictureBox();
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    // a.prepareresourcebitmap(gm);
                    var btmp = (Bitmap)a.load(gm, true).Clone();

                    pic.Width = 150;
                    pic.Top = 7;
                    pic.Height = panel.Height-15;
                    pic.Left = left;
                    left += pic.Width+10 ;
                    pic.BackColor = Color.Wheat;
                    pic.BorderStyle = BorderStyle.FixedSingle;
                    //  pic.BackColor = Color.Red;
                    pic.Click += delegate
                    {
                        tobuild = (building)a;
                        //    this.additem(a.clone());
                    };
                    btmp = a.load2(btmp, pic.Width, pic.Height);
                    pic.Image = btmp;
                    cls.Add(pic);
                }
                if (it != null)
                {
                    clear(pl);

                }
                panel.Controls.AddRange(cls.ToArray());
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
            item tt=null;
            for (int i = 0; i < gm.map.items.Count; i++)
            {
                var a = gm.map.items[i];
                if (a.type!=type.building&&a.type!=type.bullet&&a.x + a.width / 2 > absx && a.x + a.width / 2 < newx && a.y + a.height / 2 > absy && a.y + a.height / 2 < newy)
                {
                    a.selected = true;
                    tt = a;
                    lista.Add(a);
                }
                else
                {
                    a.selected = false;
                }
            }
            
            if (tt == null)
            {
                tt = lastitem;
            }
            else
            {
                GameEngineManager.selected_b = null;
            }
          //  GameEngineManager.selected = null;
           // GameEngineManager.selected_b = null;
            if (ppanel != null)
            {
                if (fx != -1)
                {
             //       clear(player);
                }
               
                fill_selection(ppanel, tt, player);
                if (fx != -1)
                {
                 //   lastitem = null;
                }
            }
            return lista;
        }
        public item match(int x, int y, player pl)
        {
            selecteditems.Clear();
            x += pl.x; y += pl.y;
            int xp = x / gm.map.mod;
            int yp = y / gm.map.mod;
            return gm.map.squares[xp, yp].piecethere;
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
        public  GameEngineManager GameEngineManager;
        public bool showsquares;
        public bool showshadow=true;
        public int reverse=-1;
        public void fill( player pl,ref string message,int resizingframe,int lastms)
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
            Color clr=Color.FromArgb(170,255,255,255);
            Color clr2=Color.FromArgb(170,0,0,0);
            Brush b1=new SolidBrush(clr);
            Brush b2=new SolidBrush(clr2);
           
            //g.SmoothingMode = SmoothingMode.AntiAlias;
          //  g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int shows=0;
            for (int i = 0; i < gm.map.mapzones.Count; i++)
            {
                var a = gm.map.mapzones[i];
                float xx = (a.x * dt - pl.x) * factow - a.width * 0 * factow * 0;// + a.width * dt * factow;
                float yy = (a.y * dt - pl.y) * factoh - a.height * 0 * factoh * 0;// + a.height * dt * factow;
                System.Drawing.Rectangle rect2 = new Rectangle((int)(xx)-90, (int)(yy)-90, (int)(a.width * factow)+90, (int)(a.height * factoh)+90);

                if (rect2.IntersectsWith(main) && !(a.stealth && a.army.teamid != pl.army.teamid && true))//hide stealth units
                {
                    a.loadframe.loadframes = (int)framenum;
                    var btmp = a.load(gm);
                    xx -= (btmp.Width - a.width) / 2;
                    yy -= (btmp.Height - a.height) / 2;
                    draw(btmp, (int)(xx + a.width * Math.Tan(a.direction) * 0), (int)(yy + a.height * Math.Tan(a.direction) * 0), (int)a.z, g);
                    shows++;

                }
            }
            GFGBuildings GFGBuildings = new GFGBuildings();
            ld.Sort(GFGBuildings);
            for (int i = 0; i < ld.Count; i++)
            {
                var a = ld[i];
                float xx = (a.x * dt  - pl.x ) * factow-a.width*0*factow*0;// + a.width * dt * factow;
                float yy = (a.y * dt  - pl.y ) * factoh-a.height*0*factoh*0;// + a.height * dt * factow;
                System.Drawing.Rectangle rect2=new Rectangle((int)(xx),(int)(yy),(int)(a.width*factow),(int)(a.height*factoh));
                if (rect2.IntersectsWith(main)&&!(a.stealth&&a.army.teamid!=pl.army.teamid&&true))//hide stealth units
                {
                    a.loadframe.loadframes = (int)framenum;
                    var btmp= a.load(gm,false,lastms);
                    xx -= (btmp.Width - a.width) / 2;
                    yy -= (btmp.Height - a.height) / 2;
                    if (a.type != type.building)
                    {
                     //   xx -= 5;
                     //   yy -= 5;
                    }
                    if (showshadow)
                    {
                        var btmpshadow = a.shadowbitmap;// resource.GenerateShadowImage(btmp);
                        if(btmpshadow == null)
                        {

                        }
                        int psx = -120;
                        int psy = -120;
                        if (a.type != type.air)
                        {
                            psx = -(int)(a.width/10.0);
                            psy =  -(int)(a.height / 10.0);
                            psx=Math.Max(-30, psx);
                            psy=Math.Max(-30, psy);
                        }
                        if (a.type == type.building)
                        {
                            psx = -5;
                            psy = -5;
                        }
                        if (!a.stealth||a.type==type.air)
                        {
                            draw(btmpshadow, (int)(xx + a.width * Math.Tan(a.direction) * 0 + psx*reverse), (int)(yy + a.height * Math.Tan(a.direction) * 0 + psy*reverse), (int)a.z, g);
                        }
                    }
                   
                    int negx = (int)(a.width * a.z / 20 / 2 * 3*factow);//بيغير موضع الرسمة علشان الطيران
                    int negy = (int)(a.height * a.z / 20 / 2 * 3*factoh);
                  
               //     g.TranslateTransform(xx+a.width/2 - negx, yy+a.height/2 - negy);
               //     g.TranslateTransform(-a.width/2,-a.height/2);
                    draw( btmp, (int)(xx +a.width* Math.Tan(a.direction)*0) , (int)(yy+a.height*Math.Tan(a.direction)*0) , (int)a.z,g);
                    //    g.ResetTransform();
                    //  sp.Stop();
                    if (!a.available&&!a.selected)
                    {
                        drawhealth(1-(a.buildtime_ms+0.0 )/ a.buildtime, (int)((a.x - 15 - pl.x + (int)a.width / 2) * factow), (int)((a.y - pl.y + (int)a.height / 2 - 15) * factoh), 0, (int)(50 * factow * a.width / 150), g,true);

                    }
                    if (a.selected&&!a.dead)
                    {
                        drawhealth( a.health / a.maxhealth, (int)((a.x - 15 - pl.x + (int)a.width / 2)*factow), (int)((a.y - pl.y + (int)a.height / 2 - 15)*factoh), 0, (int)(50* factow*a.width/150),g);
                    }
                    shows++;
                    
                }
            }
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
                        
                        Rectangle rect = new Rectangle((int)(sqrc.realxx+0)+ gm.map.mod/2 - pl.x - sz / 2, (int)(sqrc.realyy+0)  - pl.y - sz / 2+ gm.map.mod/2, sz, sz);
                        g.FillEllipse(b, rect);
                    }
                    if (sqrc.Thinpasses > 1)
                    {
                        Brush b = new SolidBrush(Color.White);
                        Rectangle rect = new Rectangle((int)(sqrc.realxx + 0)  - pl.x , (int)(sqrc.realyy + 0)  - pl.y  , 3, 3);
                        //g.FillEllipse(b, rect);

                    }
                }
                catch (Exception ex){ }
            }
            if (showsquares)
            {
                for (int i = (pl.x / gm.map.mod); i <= bitmap.Width / gm.map.mod + (pl.x / gm.map.mod); i++)
                {
                    for (int j = (pl.y / gm.map.mod); j <= bitmap.Height / gm.map.mod + (pl.y / gm.map.mod); j++)
                    {
                        var b = b1;
                        if (gm.map.squares[i, j].piecethere != null)
                        {
                            b = b2;
                        }
                        g.FillRectangle(b, i * gm.map.mod - pl.x, j * gm.map.mod - pl.y, gm.map.mod, gm.map.mod);

                    }
                }
            }
            for (int i = 0; i < ld.Count; i++)
            {
                var a = ld[i];
                double xx = (a.targetx * dt - pl.x)*factow ;
                double yy = (a.targety * dt - pl.y)*factoh ;
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
          //   drawnight(g, bitmap, 1f);
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
            gm.drawstring(bitmap, $"Silver : {pl.silver,6}        Gold :{pl.gold,6}        Food :{pl.food,6} ", 400, 0, Color.LightGreen, 15);
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
            Color ccl = Color.FromArgb((int)(225*nightfactor), 0, 0, 0);
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
               g.DrawImage(bitmp2, new Point(x, y));
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
            item.timeaway = -1;
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
            int workersadded = 0;
            GFGworkers gFGworkers = new GFGworkers();
            gFGworkers.xi = item.centerx;
            gFGworkers.yi = item.centery;
            workers.Sort(gFGworkers);
            if (i >= item.workersrequired)
            {
                for(int j = 0; j < workers.Count && item.workersrequired>0; j++)
                {
                    if (workers[j].health >= 0 && !workers[j].working)
                    {
                        item.addworker(workers[j]);
                      //  workers[j].health = -1;
                      //  workers[j].deathcount = -1;
                        workersadded++;
                    }
                    if (workersadded >= item.workersrequired)
                    {
                        break;
                    }
                }
                if (workersadded < item.workersrequired)
                {
                    GameEngineManager.message = "workers needed !";
                    return false;
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
            var xminusxx = x - xx;
            var yminusyy = y - yy;
            if (Math.Abs(xminusxx) < 1)
            {
                if (xminusxx >= 0)
                {
                    xminusxx = 1;
                }
                else
                {
                    xminusxx = -1;
                }
            }
            if (Math.Abs(yminusyy) < 0.1)
            {
                if (yminusyy >= 0)
                {
                    yminusyy = 1;
                }
                else
                {
                    yminusyy = -1;
                }
            }
            
            var xy2 = MathF.Sqrt(xminusxx * xminusxx + yminusyy * yminusyy);
            xy2 = MathF.Max(0.0001f, xy2);
            item.newspeedx = xy * xminusxx / xy2;
            item.newspeedy = xy * yminusyy / xy2;
        }
    }
 

}
