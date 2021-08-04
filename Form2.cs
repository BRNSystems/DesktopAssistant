using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DesktopAssistant
{
    public partial class Form2 : Form
    {
        public Image[] images = { Properties.Resources.Meme1, Properties.Resources.Meme2, Properties.Resources.Meme3, Properties.Resources.Meme4, Properties.Resources.Meme5, Properties.Resources.Meme6, Properties.Resources.Meme7, Properties.Resources.Meme8, Properties.Resources.Meme9, Properties.Resources.Meme10, Properties.Resources.Meme11, Properties.Resources.Meme12, Properties.Resources.Meme13, Properties.Resources.Meme14, Properties.Resources.Meme15, Properties.Resources.Meme16, Properties.Resources.Meme17, Properties.Resources.Meme18};
        
        public Form2()
        {
            InitializeComponent();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Random rng = new Random();
            base.OnFormClosing(e);
            SoundPlayer sound = new SoundPlayer();
            sound.Stream = Properties.Resources.image_close;
            sound.Play();
            int xpos2 = rng.Next(0, Screen.FromControl(this).Bounds.Width);
            int ypos2 = rng.Next(0, Screen.FromControl(this).Bounds.Height);
            Cursor.Position = new Point(xpos2, ypos2);
            Thread.Sleep(2000);
            xpos2 = rng.Next(0, Screen.FromControl(this).Bounds.Width);
            ypos2 = rng.Next(0, Screen.FromControl(this).Bounds.Height);
            Cursor.Position = new Point(xpos2, ypos2);
            SendKeys.Send("%{Tab}");
            Thread.Sleep(10);
            SendKeys.Send("%{Tab}");
            Thread.Sleep(10);
            SendKeys.Send("%{Tab}");
            Thread.Sleep(10);
            SendKeys.Send("%{Tab}");
            Program.closed = true;
            Program.form2 = new Form2();
            Program.thread3 = new Thread(Program.form2x);
        }
            private void Form2_Load(object sender, EventArgs e)
        {
            Random rng = new Random();
            this.pictureBox1.Image = images[rng.Next(0, 17)];
            int xpos2 = rng.Next(0, Screen.FromControl(this).Bounds.Width);
            int ypos2 = rng.Next(0, Screen.FromControl(this).Bounds.Height);
            Cursor.Position = new Point(xpos2, ypos2);
            this.TopMost = true;
        }
    }
}
