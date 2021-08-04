using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopAssistant
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        /// 

        public static volatile Form1 form;
        public static volatile Form2 form2;
        public static Thread thread1;
        public static Thread thread2;
        public static Thread thread3;
        public static Thread thread4;
        public static Thread thread5;
        public static Thread thread6;
        public static bool playing = false;
        public static bool wasplaying = false;
        public static volatile bool closed = false;

        public const int smoothingx = 10;

        static void form1(object data)
        {

            Application.Run((Form1) data);
            
        }

        public static void form2x(object data)
        {

            Application.Run((Form2)data);
        }

        static void playaudio1(object data)
        {
            SoundPlayer sound = new SoundPlayer();
            sound.Stream = (Stream)data;
            sound.Play();
            playing = false;
            thread5 = new Thread(playaudio1);
            return;
        }
        static void playaudio2(object data)
        {
            SoundPlayer sound = new SoundPlayer();
            sound.Stream = (Stream)data;
            sound.Play();
            playing = false;
            thread6 = new Thread(playaudio2);
            return;
        }

        static void moveimage(object data)
        {
            Form2 imgview = (Form2)data;
            Random rng = new Random();
            int xdif;
            int ydif;
            int xpos2 = rng.Next(0, Screen.FromControl(form2).Bounds.Width - 341);
            int ypos2 = rng.Next(0, Screen.FromControl(form2).Bounds.Height - 364);
            int xpos = 0;
            int ypos = 0;
            int steps = 1;
            xdif = imgview.Location.X - xpos2;
            ydif = imgview.Location.Y - ypos2;
            while (xdif != 0 || ydif != 0)
            {
                xdif = imgview.Location.X - xpos2;
                ydif = imgview.Location.Y - ypos2;

                if (xdif > 0)
                {
                    xpos = imgview.Location.X - steps;

                }
                else if (xdif < 0)
                {
                    xpos = imgview.Location.X + steps;


                }
                if (ydif > 0)
                {
                    ypos = imgview.Location.Y - steps;
                }
                else if (ydif < 0)
                {
                    ypos = imgview.Location.Y + steps;
                }
                imgview.Location = new Point(xpos, ypos);
                Thread.Sleep(1);
            }
            thread4 = new Thread(moveimage);
        }

        static void movechar(object data)
        {
            Random rng = new Random();
            Form1 formx = (Form1)data;
            int xdif;
            int ydif;
            int xpos = 0;
            int ypos = 0;
            int steps = 1;
            int ytemp = 0;
            bool window = false;
            bool firstrun = true;
            bool canopen = false;
            while (true)
            {
                canopen = closed || firstrun;
                if (rng.Next(0, 1000) == 0 && canopen)
                {
                    firstrun = false;
                    window = true;
                    closed = false;
                    ytemp = Cursor.Position.Y;
                }
                xdif = formx.obrazok.Location.X - Cursor.Position.X;
                ydif = formx.obrazok.Location.Y - Cursor.Position.Y;
                if (window)
                {
                    xdif = formx.obrazok.Location.X;
                    ydif = formx.obrazok.Location.Y - ytemp;
                    if (xdif == 5)
                    {
                        window = false;
                        form2.Location = new Point(0, ytemp);
                        thread3.Start(form2);
                        form2.Location = new Point(0, ytemp);
                        thread4.Start(form2);
                        if (!playing)
                        {
                            playing = true;
                            thread6.Start(Properties.Resources.image_open);
                        }
                    }
                }

                if (xdif == 0)
                {
                    formx.obrazok.Image = Properties.Resources.tucan1;
                }

                if (xdif > 5)
                {
                    xpos = formx.obrazok.Location.X - steps;
                    formx.obrazok.Image = Properties.Resources.tucan1;

                }
                else if (xdif < -5)
                {
                    xpos = formx.obrazok.Location.X + steps;
                    formx.obrazok.Image = Properties.Resources.tucan0;


                }
                if (ydif > 5)
                {
                    ypos = formx.obrazok.Location.Y - steps;
                }
                else if (ydif < -5)
                {
                    ypos = formx.obrazok.Location.Y + steps;
                }
                formx.obrazok.Location = new Point(xpos, ypos);
                Thread.Sleep(1);
            }

        }

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Control.CheckForIllegalCrossThreadCalls = false; // daj sa vypchat c#
            form = new Form1();
            thread1 = new Thread(form1);
            thread1.Start(form);
           
            while (!form.ready) {
                Thread.Sleep(10);
            }
            form2 = new Form2();
            thread2 = new Thread(movechar);
            thread2.Start(form);
            thread3 = new Thread(form2x);
            thread4 = new Thread(moveimage);
            thread5 = new Thread(playaudio1);
            thread6 = new Thread(playaudio2);
            playing = true;
            thread5.Start(Properties.Resources.start);


        }
    }
}
