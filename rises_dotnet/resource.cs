﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
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
        public  Bitmap shadowbitmap;
       // public Bitmap Bitmap { get { return bitmap; }set { bitmap = value; } }
        public Bitmap Bitmap { get { /*bitmap = (Bitmap)bitmap.Clone();*/ return (Bitmap)bitmap.Clone(); } set { bitmap = value; } }
        public string name = "";
        public string sound;
       //  secondresource[,,,] secondresources;
        Dictionary<string, secondresource> secondresources=new Dictionary<string, secondresource>();   
        public static Bitmap ResizeImage(Bitmap imgToResize, Size size)
        {
            try
            {
                if(imgToResize.Size == size)
                {
                    //return imgToResize;
                    return (Bitmap)imgToResize.Clone();
                }
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
        public static Bitmap cropImage(Bitmap img, Rectangle cropArea)
        {
            try { 
            Bitmap bmpImage = new Bitmap(img);
            return (Bitmap)bmpImage.Clone(cropArea, bmpImage.PixelFormat);
            }
            catch
            {
                return null;
            }
        }
        public static Bitmap fillwitharmycolor(Image image,Color amrycolor)
        {
            Bitmap bitmap = (Bitmap)image.Clone(); //new Bitmap(image.Width, image.Height);
            const int refre = 250;
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (pixelColor.A==255&&pixelColor.R>=refre&&pixelColor.G>=refre&&pixelColor.B>=refre)
                    {

                        bitmap.SetPixel(x, y, amrycolor);
                    }
                }
            }


            //   ApplyShadowEffect(shadowImage, shadowSize);

            return bitmap;
        }
        public static Bitmap GenerateShadowImage(Image image,float opacity)
        {
            //  int shadowSize = 10; // Adjust the shadow size as needed
            Bitmap bitmap = (Bitmap)image.Clone(); //new Bitmap(image.Width, image.Height);
            if (opacity >=1.0f)
            {
                opacity = 2.0f;
            }
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    if (pixelColor.A > 0)
                    {
                     
                        bitmap.SetPixel(x, y, Color.FromArgb((int)(50*opacity), 0, 0, 0));
                    }
                }
            }


            //   ApplyShadowEffect(shadowImage, shadowSize);

            return bitmap;
        }

        public static Bitmap RotateAndCropBitmap(Bitmap bitmap, float angle,type type)
    {
            if (type == type.building)
            {
                return bitmap;
            }
        // Convert the angle to radians
        float angleInRadians = (float)(45 * Math.PI / 180);

        // Calculate the rotation matrix
        float sin = (float)Math.Abs(Math.Sin(angleInRadians));
        float cos = (float)Math.Abs(Math.Cos(angleInRadians));
        float newWidth = cos * bitmap.Width + sin * bitmap.Height+1;
        float newHeight = sin * bitmap.Width + cos * bitmap.Height+1;
        
        
        // Create a new bitmap with the calculated size
        Bitmap rotatedBitmap = new Bitmap((int)newWidth, (int)newHeight);

        using (Graphics g = Graphics.FromImage(rotatedBitmap))
        {
            // Set the interpolation mode to achieve better quality during rotation
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            // Calculate the rotation point as the center of the bitmap
            PointF rotationPoint = new PointF(bitmap.Width / 2f, bitmap.Height / 2f);

            // Translate and rotate the graphics object around the rotation point
            g.TranslateTransform(newWidth / 2f, newHeight / 2f);
                try
                {
                    g.RotateTransform(angle);
                }
                catch
                {

                }
            g.TranslateTransform(-rotationPoint.X, -rotationPoint.Y);

            // Draw the original bitmap onto the rotated bitmap
            g.DrawImage(bitmap, new PointF(0, 0));
        }

        // Calculate the cropping rectangle
       // int cropX = (int)((newWidth - bitmap.Width) / 2f);
       // int cropY = (int)((newHeight - bitmap.Height) / 2f);
        //Rectangle cropRect = new Rectangle(cropX, cropY, (int)(bitmap.Width), (int)(bitmap.Height ));

        // Crop the rotated bitmap
     //   Bitmap croppedBitmap = rotatedBitmap.Clone(cropRect, bitmap.PixelFormat);

        // Dispose the intermediate rotated bitmap
      //  rotatedBitmap.Dispose();

        return rotatedBitmap;
    }

    public static Bitmap RotateBitmapWithoutCropping(Bitmap bitmap, float angle)
        {
            // Calculate the size of the new bitmap to hold the rotated image
            int newWidth = (int)(bitmap.Width * Math.Abs(Math.Cos(angle*item.pi/180)) + bitmap.Height * Math.Abs(Math.Sin(angle*item.pi/180)));
            int newHeight = (int)(bitmap.Height * Math.Abs(Math.Cos(angle*item.pi/180)) + bitmap.Width * Math.Abs(Math.Sin(angle*item.pi/180)));

            // Create a new bitmap with the calculated size
            Bitmap rotatedBitmap = new Bitmap(newWidth, newHeight);

            // Set the rotation point at the center of the new bitmap
            PointF rotationPoint = new PointF(newWidth / 2f, newHeight / 2f);

            using (Graphics g = Graphics.FromImage(rotatedBitmap))
            {
                // Clear the background with a transparent color
                g.Clear(Color.Transparent);

                // Set the interpolation mode to achieve better quality during rotation
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // Rotate the graphics object around the rotation point
                g.TranslateTransform(rotationPoint.X, rotationPoint.Y);
                g.RotateTransform(angle);
                g.TranslateTransform(-rotationPoint.X, -rotationPoint.Y);

                // Draw the original bitmap onto the rotated bitmap
                g.DrawImage(bitmap, new PointF((newWidth - bitmap.Width) / 2f, (newHeight - bitmap.Height) / 2f));
            }

            return rotatedBitmap;
        }
        public Bitmap minibitmap;
        public resource(string path, string sound, string name, item item,float fw,float fh,int resourceoption1, int resourceoption2, int resourceoption3,int resourceoption4)
        {
            this.sound = sound;
            Bitmap btmp = new Bitmap(path);
            if (item != null)
            {
                try
                {
                    if(item.army!=null) 
                    btmp = fillwitharmycolor(btmp, item.army.armycolor);
                }
                catch(Exception ex)
                { 
                
                }
            }
            Bitmap btmp2 = (Bitmap)btmp.Clone();// new Bitmap(path);
            if (item!=null&&item.type == type.building)
            {
                btmp.RotateFlip(RotateFlipType.Rotate180FlipY);
            }
            if (item != null)
            { 
                btmp = ResizeImage(btmp, new Size((int)(item.width*fw), (int)(item.height*fh)));
                minibitmap = ResizeImage(btmp, new Size((int)(item.width*fw/item.engine.gm.map.mod), (int)(item.height*fh/ item.engine.gm.map.mod)));
            
            }
            bitmap = btmp;
            if (item!=null)
            {
                shadowbitmap = GenerateShadowImage(bitmap, 0.5f);
            }
            if (name == "mappicture" )
            {
                bitmap2 = (Bitmap)btmp.Clone();
            }
            else
            {
                bitmap2 = ResizeImage(btmp2, new Size((int)(100), (int)(100)));
            
            }
         
            this.name = name;
            //secondresources=new secondresource[resourceoption1, resourceoption2,resourceoption3,resourceoption4];
        }

        public secondresource GetSecondresource(int resourceoption1, int resourceoption2, int resourceoption3,int resourceoption4,string plus)
        {
            try
            {
                string key = $"{resourceoption1.ToString("000")}{resourceoption2.ToString("000")}{resourceoption3.ToString("000")}{resourceoption4.ToString("000")}{plus}";
               // int[] key = { resourceoption1, resourceoption2, resourceoption3, resourceoption4 };
                var sr = secondresources[key] ;// secondresources[resourceoption1, resourceoption2, resourceoption3, resourceoption4];
                return sr;
            }
            catch
            {
                return null;    
            }
        }
        public void setsecondresource(Bitmap btmp,Bitmap shadow,int resourceoption1, int resourceoption2, int resourceoption3,int resourceoption4, string plus)
        {
            try
            {
                secondresource sr = new secondresource(btmp,shadow);
                string key = $"{resourceoption1.ToString("000")}{resourceoption2.ToString("000")}{resourceoption3.ToString("000")}{resourceoption4.ToString("000")}{plus}";
                if (secondresources.ContainsKey(key))
                {
                    secondresources[key] = sr;
                }
                else
                {
                    secondresources.Add(key, sr);
                }
                //secondresources[resourceoption1, resourceoption2, resourceoption3, resourceoption4] = sr;
            }
            catch(Exception e) 
            {

            }
        }
        public static Bitmap SetOpacity(Bitmap image, float opacity)
        {
            var colorMatrix = new ColorMatrix();
            colorMatrix.Matrix33 = opacity;
            var imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);
            var output = new Bitmap(image.Width, image.Height);
            using (var gfx = Graphics.FromImage(output))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.DrawImage(
                    image,
                    new Rectangle(0, 0, image.Width, image.Height),
                    0,
                    0,
                    image.Width,
                    image.Height,
                    GraphicsUnit.Pixel,
                    imageAttributes);
            }
            return output;
        }
    }
    public class secondresource
    {
        public Bitmap bitmap;
        public Bitmap shadowbitmap;
        public secondresource(Bitmap bitmap, Bitmap shadowbitmap)
        {
            this.bitmap = (Bitmap)bitmap.Clone();
            this.shadowbitmap = shadowbitmap;
        }
    }
}
