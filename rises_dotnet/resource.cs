using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rise
{
    public class resource
    {
        public int id = 0;
         Bitmap bitmap;
        public  Bitmap bitmap2;
       // public Bitmap Bitmap { get { return bitmap; }set { bitmap = value; } }
        public Bitmap Bitmap { get { /*bitmap = (Bitmap)bitmap.Clone();*/ return (Bitmap)bitmap.Clone(); } set { bitmap = value; } }
        public string name = "";
        public string sound;
         secondresource[,,] secondresources;
        public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            try
            {
                Bitmap b = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage((Image)b))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                    g.Dispose();
                }
                return b;
            }
            catch
            {
                Console.WriteLine("Bitmap could not be resized");
                return imgToResize;
            }
        }
        public resource(string path, string sound, string name, item item,float fw,float fh,int resourceoption1, int resourceoption2, int resourceoption3)
        {
            this.sound = sound;
            Bitmap btmp = new Bitmap(path);
            if (item != null)
            { btmp = ResizeImage(btmp, new Size((int)(item.width*fw), (int)(item.height*fh))); }
            bitmap = btmp;
            if (name == "mappicture")
            {
                bitmap2 = (Bitmap)btmp.Clone();
            }
            this.name = name;
            secondresources=new secondresource[resourceoption1, resourceoption2,resourceoption3];
        }
        public secondresource GetSecondresource(int resourceoption1, int resourceoption2, int resourceoption3)
        {
            try
            {
                var sr = secondresources[resourceoption1, resourceoption2, resourceoption3];
                return sr;
            }
            catch
            {
                return null;    
            }
        }
        public void setsecondresource(Bitmap btmp,int resourceoption1, int resourceoption2, int resourceoption3)
        {
            try
            {
                secondresource sr = new secondresource(btmp);
                secondresources[resourceoption1, resourceoption2, resourceoption3] = sr;
            }
            catch
            {

            }
        }
    }
    public class secondresource
    {
        public Bitmap bitmap;
        public secondresource(Bitmap bitmap)
        {
            this.bitmap = (Bitmap)bitmap.Clone();
        }
    }
}
