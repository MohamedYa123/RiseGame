using Microsoft.Win32.SafeHandles;
using rises_dotnet.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Effects;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace Rise
{

    public enum type {  vehicle, builder, building, grabber, soldier,worker, air, sea, bullet }
    public enum piecemode {  protect,passive, agressive,stop }
    public enum tracktype { naive, simple, full }
    /*
     naive  : just follows targetx and targety by direction 
     simple : targetx and targety are updated by target co-ordinates then follows by direction
     full   : targetx and targety are updated then follows by pathfinding
     */
    public class piece : item
    {
        public string picture = "";
        
        public bool viewed;
        public bool detcted;

        public float power;
        public float maxpower;
        public int bullets;
        public int maxbullets;
        public int shot_time_ms;
        public int reloadtime_ms;
        public double recoveramount;
        public int score;
        public double upgrade_factor;
        public int star;
        //     public int speed;
        public int version;
        public int pricesilver;
        public int pricegold;
        
        public int liftime;
        public int minlifetime;
        public int maxlifetime;
        public string name;

        public bool justcreated = true;
        int last;
        int ticks;
        public float healthdecrease;

        piece mother;
        bool once = true;
        int plus;
        public override void load_resouces(game gm, float fw,float fh)
        {
            if (sound != "")
            {
                sound = $"resources/{army.name}/pieces/sound/{sound}";
            }
            resourceid = gm.map.load_resource($"resources/{army.name}/pieces/{image}", $"{army.name} - " + name, sound, this,fw,fh);
            foreach(var a in imagesofanimations)
            {
                resourcesofanimation.Add(gm.map.load_resource($"resources/{army.name}/pieces/{a.Value}", $"{army.name} - " + name, sound, this, fw, fh));
            }
        }
        //  bool previouswalk;
        float oldtargetx = -1;
        float oldtargety = -1;
        int numofhits;
        
        int zz;
        int frames=-1;
        float plusopacity = -1;
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);
        item gettarget(float dist)
        {
            int xstart = (int)(x / engine.gm.map.mod);
            int ystart = (int)(y / engine.gm.map.mod);
            int spx = (int)(dist / engine.gm.map.mod);
            for (int i = xstart - spx; i < xstart + spx; i++)
            {
                for(int j = ystart - spx; j < ystart + spx; j++)
                {
                    if (i < 0 || j < 0 || i >= engine.gm.map.xlen || j >= engine.gm.map.ylen)
                    {
                        continue;
                    }
                    var pc = engine.gm.map.squares[i, j].piecethere;
                    if (pc != null&&pc!=this&&pc.army!=army)
                    {
                        return pc;
                    }
                }
            }
            return null;
        }
        public override void read()
        {
            frames++;
            if (target == null && targetx <0)
            {
                //auto attack
                if (mode == piecemode.protect)
                {
                    target = gettarget(rangeofattack);
                }
                else if (mode == piecemode.agressive)
                {
                    target=gettarget(rangeofattack*3);
                }
            }
            if(type==type.bullet)
            {
                //   change = 0.03f;
                var xx = (int)(x / engine.gm.map.mod);//Math.Round
                var yy = (int)(y / engine.gm.map.mod);//Math.Round
                engine.gm.map.squares[xx, yy].Rockettail = 50;

            }
            else
            {

            }
            if (x < 0 || y < 0 || x > engine.gm.map.width || y > engine.gm.map.height)
            {
                health = -10;
                return;
            }
            if (track != tracktype.naive&&timeaway<=0)
            {
                speedx = newspeedx * change + speedx * (1 - change);
                speedy = newspeedy * change + speedy * (1-change);
            }
            if (timeaway > 0)
            {
                walk = true;
                speedx = newspeedxtimed * change + speedx * (1 - change);
                speedy = newspeedytimed * change + speedy * (1 - change);
                goto g;
            }
            if (track == tracktype.simple)
            {
                if (target != null)
                {
                    targetx=target.x+target.width/2; targety=target.y+target.height/2;
                    engine.change_direction_direct(this, (int)targetx, (int)targety);

                }
            }
            if (justcreated)
            {
                if (sound != "")
                {
                    //SoundPlayer sdp = new SoundPlayer();
                    //sdp.SoundLocation = sound;
                    //  Task.Run(() => { sdp.Play(); });
                //    Task.Run(() =>
                    {
                        //  var c = new MediaPlayer();
                        //  c.Open(new System.Uri(@"G:\projects\C# Projects\rises_dotnet\rises_dotnet\bin\Debug\resources\USA\pieces\sound\bullet.wav"));
                        //  c.Play();
                        // mciSendString(@"open G:\projects\C# Projects\rises_dotnet\rises_dotnet\bin\Debug\resources\USA\pieces\sound\bullet.wav type waveaudio alias applause", null, 0, IntPtr.Zero);
                        // mciSendString(@"play applause", null, 0, IntPtr.Zero);
                        //Player pl = new Player();
                        //byte[] arr = File.ReadAllBytes(@"in.wav");
                        //pl.Play(arr);
                        //Player pl2 = new Player();
                        //pl2.FileName = "123.mp3";
                        //pl2.Play();
                        //SoundEffectInstance Sound = SoundEffect.FromStream(Application.GetResourceStream(new Uri("Assets/Sounds/wav/sound.wav", UriKind.Relative)).Stream).CreateInstance();
                        //Sound.IsLooped = true;
                        //Sound.Play();
                        // windowsm
                        //    c.Close();
                        effect = new SoundPlayer(sound);
                        BackgroundWorker effectPlayer;
                        effectPlayer = new BackgroundWorker();
                        effectPlayer.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
                        //effectPlayer.RunWorkerAsync();

                        // axWindowsMediaPlayer1.pl
                    }//);
                }

            }
            else if (track==tracktype.full)
            {
                //pathfinding
                if (target != null)
                {
                    targetx = target.x + target.width / 2;
                    targety = target.y + target.height / 2;

                }
                if (target == null)
                {
                    var xx = x + width / 2;
                    var yy = y + height / 2;
                    var dist = MathF.Sqrt((targetx - xx) * (targetx - xx) + (targety - yy) * (targety - yy));
                    if (dist < 20)
                    {
                        pathsquares.Clear();
                        targetx = -1;
                        targety = -1;
                        walk = false;
                    }
                    else if(targetx!=-1)
                    {
                        walk = true;
                    }
                }
                if (targetx != -1 && walk)
                {

                    float timerx = 1;
                    float timery = 1;
                    if (canceledx)
                    {
                        //          timerx = xx/targetx;

                    }
                    if (canceledy)
                    {
                        //        timery= yy/targety;
                    }
                    if (true)
                    {
                        if (oldtargetx != targetx && oldtargety != targety||(pathsquares.Count<0))
                        {
                            var xt = (int)targetx / engine.gm.map.mod;
                            var yt = (int)targety / engine.gm.map.mod;
                            var xtnow = (int)(x + width / 2) / engine.gm.map.mod;
                            var ytnow = (int)(y + height / 2) / engine.gm.map.mod;
                            if(xt>=engine.gm.map.xlen|| yt >= engine.gm.map.ylen)
                            {
                                goto jump;
                            }
                            var end = engine.gm.map.squares[xt, yt];
                            var start = engine.gm.map.squares[xtnow, ytnow];
                            
                            // end.x = targetx/engine.gm.map.mod;
                            // end.y=targety / engine.gm.map.mod;

                            var xx = x + width / 2;
                            var yy = y + height / 2;
                            var p = pathfinding(start, end, xx, yy);
                            minus = 0;
                            if (p.Count == 0)
                            {
                                basicdoublingfactor = 1;
                            }
                            else
                            {
                                basicdoublingfactor = 1.1f;
                            }
                            if (p.Count == 0&&resettarget&&pathsquares.Count!=0&&false)
                            {
                                resettarget = false;  
                           //     minus = -1;
                            }
                            else
                            {
                                pathsquares = p;
                                pointer = pathsquares.Count - 1;
                                resettarget = false;
                                //   minus = 0;
                            }
                          //  minus = -1;
                           
                            if (target != null && target.walk)
                            {
                                pointer -= 2;
                            }
                            //     minitargetx = sqrc.x * engine.gm.map.mod;
                            //     minitargety = sqrc.y * engine.gm.map.mod;
                            oldtargetx = targetx;
                            oldtargety = targety;
                        }
                        jump:
                        if (pointer >= 0 && pathsquares.Count > 1)
                        {
                            var sqrc = pathsquares[pointer];
                            int aa = 0;
                            for(int i = pointer; i >=0 && aa < 8; i--, aa++)
                            {
                                var sqrc2 = pathsquares[pointer];
                                sqrc2.makesweetswap(engine.gm.map, pathfindingorder,orderid,this);

                            }
                            minitargetx = sqrc.x * engine.gm.map.mod;
                            minitargety = sqrc.y * engine.gm.map.mod;
                            var dist = MathF.Sqrt(MathF.Pow(minitargetx - x-width/2, 2) + MathF.Pow(minitargety - y-height/2, 2));
                            if (dist <= (float)engine.gm.map.mod*4f)
                            {
                                pointer--;
                                numofhits = 0;
                            }

                        }
                        if (pointer <= 1)
                        {
                            minitargetx = targetx; minitargety = targety;
                        }
                        engine.change_direction_direct(this, (int)(minitargetx * timerx), (int)(minitargety * timery));
                        if (pointer + plus >= 0)
                        {
                            pointer += plus;
                            
                        }
                       // oldtargetx = targetx;
                       // oldtargety = targety;
                        //  plus = 0;

                    }
                    
                    if (canceledx && !canceledy)
                    {
                        //  newspeedx /= 10;
                        int ix = 1;
                        if (newspeedy < 0)
                        {
                            ix = -1;
                        }
                        newspeedy = basespeed * ix;

                        //      once= false;
                    }
                    else if (canceledy && !canceledx)
                    {
                        int ix = 1;
                        if (newspeedx < 0)
                        {
                            ix = -1;
                        }
                        newspeedx = basespeed * ix;

                        // newspeedy/= 10;
                        //    once = true;
                    }
                }


            }
            
           
            if (type==type.bullet)
            {
                var xx = x + width / 2;
                var yy = y + height / 2;
                var dist = MathF.Sqrt((targetx - xx) * (targetx - xx) + (targety - yy) * (targety - yy));
                //Rectangle rc=new Rectangle((int)x,(int)y,(int)width,(int)height);
                //foreach(var a in engine.gm.map.items)
                //{
                //    if (a == this||a==mother)
                //    {
                //        continue;
                //    }
                //    Rectangle rc2 = new Rectangle((int)a.x, (int)a.y, (int)a.width, (int)a.height);
                //    if (rc.IntersectsWith(rc2))
                //    {
                //        health = -10;
                //        a.health -= power;
                //    }
                //}
                if (target != null)
                {
                    Rectangle rect = new Rectangle((int)target.x, (int)target.y, (int)target.width, (int)target.height);
                    Rectangle rect2 = new Rectangle((int)x, (int)y, (int)width, (int)height);
                    if (rect.IntersectsWith(rect2))
                    {
                        z = 0;
                    }
                }
                if (target==null|| dist <Math.Min( 200,target.squarewidth*3))
                {
                    z = 0;
                }
                if (z < 3)
                {
                    int newx = (int)Math.Round((x) / engine.gm.map.mod);
                    int newy = (int)Math.Round((y) / engine.gm.map.mod);
                    
                    var a = engine.gm.map.squares[newx, newy].piecethere;
                   
                    if (!(a == this || a == mother || a == null))
                    {
                        if (a != null)
                        {
                            int ix = (int)a.x / engine.gm.map.mod;
                            int iy = (int)a.y / engine.gm.map.mod;
                            int nx = (int)(a.x + a.width) / engine.gm.map.mod;
                            int ny = (int)(a.y + a.height) / engine.gm.map.mod;
                            bool done = true;
                            if (Math.Abs(newx - nx) <= 1&&!done)
                            {
                                newx = nx;
                                done= true;
                            }
                            if (Math.Abs(newx - ix) <= 1&&!done)
                            {
                                newx = ix;
                                done= true;
                            }
                            if (Math.Abs(newy - ny) <= 1&&!done)
                            {
                                newy = ny;
                                done= true;
                            }
                            if (Math.Abs(newy - iy) <= 1&&!done)
                            {
                                newy = iy;
                                done= true;
                            }
                        }
                        health = -10;
                        engine.gm.map.squares[newx, newy].Explosion = 150;
                        
                      //  engine.gm.map.squares[newx, newy].Rockettail = 80;
                      //  engine.gm.map.squares[newx, newy].dx = 8;
                        a.health -= power;
                    }
                    health *= healthdecrease;
                    if (health < 0.01)
                    {
                        health = -1;
                    }

                }
            }
            if (type!=type.air)
            {
                z *= 0.95f;
                //  x -= width / 20 * z*0.03f;
                //  y -= height / 20 * z*0.03f;
            }
            justcreated = false;
            if (target != null && target.health < 0)
            {
                target = null;
                targetx = (int)(x + width / 2);
                targety = (int)(y + height / 2);
                walk = true;
            }
            if (type!=type.bullet && target != null && timeaway <= 0)
            {
                var xz = target.x + target.width / 2;
                var yz = target.y + target.height / 2;

                var dist = MathF.Sqrt((xz - x) * (xz - x) + (yz - y) * (yz - y));
                var olddirectx = newspeedx;
                var olddirecty = newspeedy;
                engine.change_direction_direct(this, (int)xz, (int)yz);
                bool fire = false;
                if (1 - MathF.Abs((speedx - newspeedx) / newspeedx) > 0.3 && 1 - MathF.Abs((speedy - newspeedy) / newspeedy) > 0.3)
                {
                    fire = true;
                }
                if (dist-target.squarewidth/2 < rangeofattack)
                {
                    targetx = -1;
                    targety=-1;
                    pathsquares.Clear();
                    pointer = -1;
                    walk = false;

                }
                else
                {
                    newspeedx = olddirectx;
                    newspeedy = olddirecty;
                    walk = true;
                }

                if (fire && bullets > 0 && walk == false && (ticks - last == 0 || ticks - last > shot_time_ms))
                {
                    var a = (piece)engine.gm.map.asstes[0].clone();
                    a.justcreated = true;
                    a.x = x + width / 2;
                    a.y = y + height / 2;
                    a.z = 20;
                    a.target = target;
                    a.targetx = target.x + target.width / 2; a.targety = target.y + target.height / 2;
                 //   a.x += (width / 2 + 20) * speedx / speed;
                //    a.y += (height / 2 + 10) * speedy / speed;
                    a.healthdecrease = 0.9f;
                    a.minitargetx = a.targetx;
                    a.minitargety = a.targety;
                    a.power = power;
                    // engine.gm.map.items.Add(a);
                    var xy = a.basespeed;// MathF.Sqrt(a.speedx * a.speedx + a.speedy * a.speedy);
                    //var xy2=MathF.Sqrt(speedx *speedx + speedy*speedy);
               //     a.speedx = xy * speedx / basespeed;
                    a.speedy=(float)Math.Sqrt((xy*xy)/(1+Math.Pow(speedx/speedy,2)));
                    a.speedx=(float)(a.speedy*speedx/speedy);
                    if (speedy < 0)
                    {
                        a.speedy *= -1;
                    }
                    if(speedx < 0 != a.speedx < 0)
                    {
                        a.speedx *= -1;
                    }
                    //a.speedy = xy * speedy / basespeed;
                    
                    a.mother = this;
                    engine.additem(a);
                    bullets--;
                    last = ticks;

                }
                else
                {

                }
            }
            if (type != type.bullet && bullets == 0)
            {
                if (ticks - last > reloadtime_ms)
                {
                    last = ticks;
                    bullets = maxbullets;
                }
            }
            g:
            ticks++;
            if (targetx == -1 && timeaway < 0)
            {
                walk = false;
            }
            if (walk)
            {
                if (type != type.bullet)
                {
                    engine.requestchangeposition(this);
                    if ((canceledx || canceledy)&&false)
                    {
                        if (pointer >= 0 && pathsquares.Count > 1 && false)
                        {
                            var sqrc = pathsquares[pointer];

                            var xx = sqrc.x * engine.gm.map.mod;
                            var yy = sqrc.y * engine.gm.map.mod;
                            var dist = MathF.Sqrt(MathF.Pow(minitargetx - x - speedx, 2) + MathF.Pow(minitargety - y - speedy, 2));
                            numofhits++;
                            if (dist < squarewidth)
                            {
                                numofhits = 0;
                                pointer--;
                            }

                        }
                        else if(track==tracktype.full&&false)
                        {
                         //   minus=0;
                            oldtargetx = -1;
                            oldtargety = -1;
                            resettarget = true;
                            // plus  =-num;
                            if (zz + 1 > (originalnum) * 2)
                            {
                                zz = 0;
                            }
                            else
                            {
                                minus = zz;
                                zz++;
                            }
                            
                            //zz++;
                            
                            if (Math.Abs(plus) > pathsquares.Count-1)
                            {
                                plus = 0;
                            }
                        }

                    }
                }
                else
                {
                    zz = 0;
                   // minus = -1;
                    x += speedx*engine.speedfactor;
                    y += speedy*engine.speedfactor;
                    z += speedz*engine.speedfactor;
                    plus = 0;
                }
            }
            
            timeaway--;
            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            if (z < 0)
            {
                z = 0;

            }

        }
        SoundPlayer effect;
        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            effect.PlaySync();
        }
    }

}
