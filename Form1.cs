using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopAssistant
{
    public partial class Form1 : Form
    {
        public volatile PictureBox obrazok;
        public Size plocha;
        public bool ready = false;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Random rng = new Random();
            plocha = new Size(Screen.FromControl(this).Bounds.Width, Screen.FromControl(this).Bounds.Height);
            this.TabStop = false;
            this.AllowTransparency = true;
            this.Size = plocha;
            pictureBox1.Image = DesktopAssistant.Properties.Resources.tucan0;
            this.FormBorderStyle = FormBorderStyle.None;
            obrazok = pictureBox1;
            int xpos2 = rng.Next(0, Screen.FromControl(this).Bounds.Width);
            int ypos2 = rng.Next(0, Screen.FromControl(this).Bounds.Height);
            Cursor.Position = new Point(xpos2, ypos2);
            this.TopMost = true;
            this.ready = true;
        }
    }
}
