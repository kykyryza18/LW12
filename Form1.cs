using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LB12
{
    public partial class Form1 : Form
    {
        private Graphics g;
        private Pen blackpen = new Pen(Color.Black, 4);
        private Bitmap bmp;
        private Point Priviouspoint, point;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Priviouspoint.X = e.X;
            Priviouspoint.Y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                point.X = e.X;
                point.Y = e.Y;
                g.DrawLine(blackpen, Priviouspoint, point);
                Priviouspoint.X = point.X;
                Priviouspoint.Y = point.Y;
                pictureBox1.Invalidate();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.PNG)|*.bmp;*.jpg;*.gif;*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(dialog.FileName);
                int width = image.Width;
                int haight = image.Height;
                pictureBox1.Width = width;
                pictureBox1.Height = haight;
                bmp = new Bitmap(image, width, haight);
                pictureBox1.Image = bmp;
                g = Graphics.FromImage(pictureBox1.Image);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < bmp.Width; i ++)
                for (int j = 0; j < bmp.Height; j+=2)
                {
                    int Grey = (bmp.GetPixel(i, j).R + bmp.GetPixel(i, j).G + bmp.GetPixel(i, j).B) / 3;
                    Color p = Color.FromArgb(255, Grey, Grey, Grey);
                    bmp.SetPixel(i, j, p);
                }
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            savedialog.Title = "Сохранить картинку как ...";
            savedialog.OverwritePrompt = true;
            savedialog.CheckPathExists = true;
            savedialog.Filter = "Bitmap File(*.bmp|*.bmp| GIF File(*.gif)|*.gif| JPEG File(*.jpg)|*.jpg| PNG File(*.png)|*.png";
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                string filename = savedialog.FileName;
                string filextn = filename.Remove(0, filename.Length - 3);
                switch (filextn)
                {
                    case "bmp":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case "jpg":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case "tif":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Tiff);
                        break;
                    case "png":
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    default:
                        break;
                }
            }
        } 
    }
}
