using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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
        private void Form1_Load(object sender, EventArgs e)
        {
            GameEngine.init(pictureBox1.Width, pictureBox1.Height, pictureBox1.Width, pictureBox1.Height);
            player = GameEngine.gm.players[0];
            GameEngineManager = new GameEngineManager(GameEngine,player,pictureBox1.Width,pictureBox1.Height);
            GameEngineManager.runorclose(true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            // await Task.Run(() =>
       //     Width;
       //     Height;
            {
                try
                {
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
                            GameEngineManager.plusx = (int)(10 / GameEngine.gm.map.factorw);
                        }
                        if (GameEngineManager.mousey >= Height * 99 / 100)
                        {
                            GameEngineManager.plusy = (int)(10 / GameEngine.gm.map.factorh);
                        }
                        if (GameEngineManager.mousex <= Width * 1 / 100)
                        {
                            GameEngineManager.plusx = -(int)(10 / GameEngine.gm.map.factorw);
                        }
                        if (GameEngineManager.mousey <= Height * 1 / 100)
                        {
                            GameEngineManager.plusy = -(int)(10 / GameEngine.gm.map.factorh);
                        }
                    }
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
                    //  label1.Text = $"{player.x}:{player.y}:{player.z}";
                    //  pictureBox2.Image = (Bitmap)GameEngine.gm.mappic.Clone();
                    pictureBox2.Image = (Bitmap)GameEngine.gm.mappic.Clone();
                    pictureBox1.Image = GameEngineManager.imagetoshowready;// (Bitmap)GameEngine.todraw.Clone();
                                                                           //   xv = 0
                }
                catch { };

            }//);
        }

        private void Rise_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                selected_b = null;
                GameEngine.fill_selection(panel2, selcted, player);
                GameEngine.selectitems(player, -1, -1, -1, -1);
                return;
            }
            if (rightclick)
            {

                return;
            }
            item a = null;

            item fv = GameEngine.match(GameEngineManager.mousex, GameEngineManager.mousey, player);
            if (selcted == null)
            {
                a = GameEngine.match(GameEngineManager.mousex, GameEngineManager.mousey, player);
            }
            try
            {
                var gg = lista;
                for (int i = 0; i < gg.Count; i++)
                {
                    var aselect = gg[i];
                    if (fv == null)
                    {
                        if (aselect.target != null)
                        {
                            aselect.target.selected = false;
                        }
                    }
                    aselect.target = fv;
                    if (fv == null)
                    {
                        aselect.walk = true;
                    }
                    GameEngine.change_direction(aselect, GameEngineManager.mousex, GameEngineManager.mousey, player);
                    // return;
                }

                if (a == null && selcted != null)
                {
                    if (fv != selcted.target)
                    {
                        selcted.walk = true;
                    }
                    selcted.target = fv;

                    selcted.selected = true;

                    GameEngine.change_direction(selcted,GameEngineManager.mousex, GameEngineManager.mousey, player);

                    return;
                }
                if (lista.Count != 0)
                {
                    return;
                }
                selcted = (piece)a;
                selected_b = null;
                GameEngine.fill_selection(panel2, a, player);
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
                        item.selected = true;
                        lista.Add(item);
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

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

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
            lista = GameEngine.selectitems(player, fx, fy, GameEngineManager.mousex, GameEngineManager.mousey);
        }

        private void selectionloader_Tick(object sender, EventArgs e)
        {
            try
            {
                GameEngine.loadselection(selected_b, panel3);
            }
            catch { }
        }
    }
}
