﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rise
{
    public partial class Rise : Form
    {
        public Rise()
        {
            InitializeComponent();
        }
        GameEngine GameEngine=new GameEngine();
        GameEngineManager GameEngineManager;
        player player;
        PictureBox pic = new PictureBox();
        bool imdone=false;
        Bitmap bitmapll;
        System.Windows.Forms.Label label1;
        private async void Form1_Load(object sender, EventArgs e)
        {
            particleSystem = new ParticleSystem();
            bitmapll = new Bitmap("loading.png");
            label1 =new Label();

            Task.Run(() => {
                GameEngine.init(pictureBox1.Width, pictureBox1.Height, pictureBox1.Width, pictureBox1.Height, panel3);
             player = GameEngine.gm.players[0];
                GameEngineManager = new GameEngineManager(GameEngine, player, pictureBox1.Width, pictureBox1.Height, pictureBox2.Width, pictureBox2.Height);
                GameEngineManager.runorclose(true);
               imdone = true;
            });
            
            pic.Image = (Bitmap)bitmapll.Clone();
            pic.BackColor = Color.Black;
            pic.SizeMode=PictureBoxSizeMode.Zoom;
            pic.Dock = DockStyle.Fill;
            Controls.Add(pic);
            Controls.Add(label1);
            pic.BringToFront();
            //label1.BringToFront();
            label1.Hide();
        }
        int timx = 3;
        int timy = 3;
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.BringToFront();
            if (!imdone)
            {
                var btmp = bitmapll;
                var bitmap2 = new Bitmap(btmp.Width, btmp.Height);
                using (Graphics g = Graphics.FromImage(bitmap2))
                {
                    g.DrawImage(btmp, 0, 0);
                  //  g.FillRectangle(Brushes.Red, 48, 148, 204, 14);
                    g.FillRectangle(Brushes.LightGreen, 35, 150, (int)(GameEngine.progress* 200/200), 2);
                    
                }
                pic.Image = bitmap2;
                
            }
            label1.Text = GameEngine.progress + "";
            if (imdone)
            {
                pic.Hide();
            }
            
            // await Task.Run(() =>
       //     Width;
       //     Height;
            {
                IntPtr foregroundWindowHandle = GetForegroundWindow();

                // Get the process ID of the foreground window
                GetWindowThreadProcessId(foregroundWindowHandle, out int processId);

                // Get the current process ID
                int currentProcessId = System.Diagnostics.Process.GetCurrentProcess().Id;

                // Check if the current process has focus
                bool isAppInFocus = (processId == currentProcessId);

                
                try
                {
                    if (!isAppInFocus)
                    {
                        goto hh;
                    }
                    label2.Text = $"{GameEngineManager.mousex} : {GameEngineManager.mousey}";
                    // var bb = resource.ResizeImage((Bitmap)GameEngine.todraw.Clone(), new Size(1920, 1080));
                   // if (xv < 100)
                    {
                     }
                  
                    //xv++;
                    if (GameEngineManager.mousemode == 0)
                    {
                        GameEngineManager.plusx = 0;
                        GameEngineManager.plusy = 0;
                        GameEngineManager.plusz = 0;
                        GameEngineManager.mousex = Cursor.Position.X;
                        GameEngineManager.mousey = Cursor.Position.Y;
                        if (GameEngineManager.mousex >= Width * 99 / 100)
                        {
                            GameEngineManager.plusx = (int)(10 / GameEngine.gm.map.factorw*timx);
                        }
                        if (GameEngineManager.mousey >= Height * 99 / 100)
                        {
                            GameEngineManager.plusy = (int)(10 / GameEngine.gm.map.factorh*timy);
                        }
                        if (GameEngineManager.mousex <= Width * 1 / 100)
                        {
                            GameEngineManager.plusx = -(int)(10 / GameEngine.gm.map.factorw*timx);
                        }
                        if (GameEngineManager.mousey <= Height * 1 / 100)
                        {
                            GameEngineManager.plusy = -(int)(10 / GameEngine.gm.map.factorh*timy);
                        }
                    }
                    Task.Run(() =>
                    {
                        if(GameEngineManager.plusx==0&& GameEngineManager.plusy==0&& GameEngineManager.plusz == 0)
                        {
                            return;
                        }
                            while (!GameEngineManager.imsleepy)
                        {

                        }
                        try
                        {
                            player.x += Convert.ToInt32(GameEngineManager.plusx * player.settings.mousespeed);
                            player.y += Convert.ToInt32(GameEngineManager.plusy * player.settings.mousespeed);
                            player.z += Convert.ToInt32(GameEngineManager.plusz * player.settings.mousespeed);
                            if (player.x > GameEngine.gm.map.width)
                            {
                                player.x = (int)GameEngine.gm.map.width;
                            }
                            if (player.y > GameEngine.gm.map.height - pictureBox1.Height / 2 - 150)
                            {
                                player.y = (int)GameEngine.gm.map.gm.map.height - pictureBox1.Height / 2 - 150;
                            }
                        }catch { }
                        //  label1.Text = $"{player.x}:{player.y}:{player.z}";
                        //  pictureBox2.Image = (Bitmap)GameEngine.gm.mappic.Clone();
                    });
                   hh:
                    pictureBox2.Image = (Bitmap)GameEngine.gm.mappic.Clone();
                    pictureBox2.Image = (Bitmap)GameEngineManager.mappic.Clone();
                    pictureBox1.Image = GameEngineManager.imagetoshowready;// (Bitmap)GameEngine.todraw.Clone();
                                                                           //   xv = 0
                }
                catch { };

            }//);
        }

        private void Rise_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(GameEngineManager!=null)
            GameEngineManager.shutdown();
        }
        //control staff
        bool rightclick = false;
        int fx = 0;
        int fy = 0;
        List<item> lista = new List<item>();
        piece selcted = null;
        item selected_b = null;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Math.Abs(fx - GameEngineManager.mousex) > 5)
            {
                return;
            }
            //  MessageBox.Show(GameEngine.gm.map.squares[mousex/1,mousey/1].piecethere+"");
            panel3.Controls.Clear();
            if (rightclick)
            {
                lista.Clear();
                selcted = null;
                selected_b = GameEngine.gm.map.items[0];
               
               // selected_b.selected= true;
                
                GameEngine.tobuild = null;
                GameEngine.selectitems(player, -1, -1, -1, -1);
                GameEngineManager.selected = (building)selected_b;
                GameEngine.fill_selection(panel2, GameEngineManager.selected, player);
                return;
            }
            if (rightclick)
            {

                return;
            }
            item a = null;

         //   item fv = GameEngine.match(GameEngineManager.mousex, GameEngineManager.mousey, player);
           
            try
            {
                //var gg = lista;
                //for (int i = 0; i < gg.Count; i++)
                //{
                //    var aselect = gg[i];
                //    if (fv == null)
                //    {
                //        if (aselect.target != null)
                //        {
                //            aselect.target.selected = false;
                //        }
                //    }
                //    aselect.target = fv;
                //    if (fv == null)
                //    {
                //        aselect.walk = true;
                //    }
                //    GameEngine.change_direction(aselect, GameEngineManager.mousex, GameEngineManager.mousey, player);
                //    // return;
                //}

                //if (a == null && selcted != null)
                //{
                //    if (fv != selcted.target)
                //    {
                //        selcted.walk = true;
                //    }
                //    selcted.target = fv;

                //    selcted.selected = true;

                //    GameEngine.change_direction(selcted,GameEngineManager.mousex, GameEngineManager.mousey, player);

                //    return;
                //}
                //if (lista.Count != 0)
                //{
                //    return;
                //}
                //selcted = (piece)a;
                //selected_b = null;
                GameEngineManager.sendorder();
                GameEngine.fill_selection(panel2, GameEngineManager.selected, player);
            }
            catch
            {
                selected_b = (building)a;
                selcted = null;
                GameEngine.fill_selection(panel2, a, player);
            }
        }

        private void Rise_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!imdone)
            {
                return;
            }
            if (e.KeyChar == 't')
            {
                GameEngineManager.message = $"Engine took time : {GameEngineManager.enginetime} ms";
            }
            if (e.KeyChar == 'd')
            {
                GameEngine.showsquares = !GameEngine.showsquares;
            }
            if (e.KeyChar == 'h')
            {
                GameEngine.showshadow = !GameEngine.showshadow;
            }
            if (e.KeyChar == 'z')
            {
                GameEngine.reverse *=-1;
            }
            if (e.KeyChar == 's')
            {
                try
                {

                    foreach (var item in lista)
                    {
                        GameEngine.stopwalk(item);
                    }
                    GameEngine.stopwalk(selcted);
                }
                catch
                {

                }
            } 
            if (e.KeyChar == 'e')
            {
                GameEngine.fullmessage = 0;
            }
            if (e.KeyChar == 'q')
            {
                try
                {
                    //foreach(var a in lista)
                    //{
                    //    a.selected = false;
                    //}
                    lista.Clear();
                    foreach (var item in GameEngine.gm.map.items)
                    {
                        if (item.type != type.bullet && item.type != type.building)
                        {
                            item.selected = true;
                            lista.Add(item);
                        }
                    }
                    //  GameEngine.stopwalk(selcted);
                }
                catch
                {

                }
            }
            if(e.KeyChar == 'f')
            {
                GameEngineManager.setenginespeednext();
            }
        }
        private ParticleSystem particleSystem;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointF center = e.Location;
                float radius = 10;
                int particleCount = 100;
                int particleLife = 70;

              //  particleSystem.CreateSmoke(center, radius, particleCount, particleLife);
            }
            fx = e.X; fy = e.Y;

            if (e.Button == MouseButtons.Right)
            {

                rightclick = true;
            }
            else
            {
                player.fx = fx; player.fy = fy;
                rightclick = false;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (player.fx == -1)
            {
                return;
            }

            player.fx = -1;
            player.fy = -1;
            if (Math.Abs(fx - GameEngineManager.mousex) <= 5)
            {
                return;
            }
            selcted = null;
            selected_b = null;
            GameEngineManager.fx = fx;
            GameEngineManager.fy = fy;
            lista = GameEngine.selectitems(player, fx, fy, GameEngineManager.mousex, GameEngineManager.mousey);
        }
        int tttx = 0;
        private void selectionloader_Tick(object sender, EventArgs e)
        {
           
            try
            {
               //if (fx == -1)
                {
                    var xt = GameEngine.lastitem;
                    if (GameEngine.lastitem== null&&tttx==0)
                    {
                        xt = GameEngineManager.selected;
                    }
                    GameEngine.fill_selection(panel2, xt, player);
                }
                GameEngine.loadselection(GameEngineManager.selected_b, panel3,player);
                tttx++;
            }
            catch { }
           
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            particleSystem.UpdateParticles();
            particleSystem.DrawParticles(e.Graphics);

            Invalidate();
        }
    }
}
