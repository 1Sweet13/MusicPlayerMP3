﻿using MusicPlayer.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO; // Добавление библиотеки taglib.cs
using System.Linq;
using System.Windows.Forms;
using WMPLib;

namespace MusicPlayer
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            StartVolume();
            p_bar.ForeColor = Color.Orange;
            

        }

        List<string> paths = new List<string>();
        List<string> files = new List<string>();


        private void track_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            label1.Visible = true;
            player.URL = paths[track_list.SelectedIndex];
            label1.Text = files[track_list.SelectedIndex]; // Вывод названия при клике            

            try
            {
                var file = TagLib.File.Create(paths[track_list.SelectedIndex]);
                var bin = file.Tag.Pictures[0].Data.Data;
                pic_art.Image = Image.FromStream(new MemoryStream(bin));


            }
            catch
            {
                pic_art.Image = Resources.Plastink;
            }
           
         

        }


        private void btn_pause_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();

        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {

                     
            if (player.playState == WMPPlayState.wmppsPlaying)
                p_bar.Maximum = (int)player.Ctlcontrols.currentItem.duration;
                p_bar.Value = (int)player.Ctlcontrols.currentPosition;
            

            try
            {
                lbl_track_start.Text = player.Ctlcontrols.currentPositionString.ToString();
                lbl_track_end.Text = player.Ctlcontrols.currentItem.durationString.ToString();
            }
            catch
            {

            }

        }

        private void p_bar_MouseDown(object sender, MouseEventArgs e)
        {
            if (player.playState == WMPPlayState.wmppsPlaying || player.playState == WMPPlayState.wmppsPaused)
                player.Ctlcontrols.currentPosition = player.currentMedia.duration * e.X / p_bar.Width;

        }

        private void track_volume_Scroll(object sender, EventArgs e)
        {
            player.settings.volume = track_volume.Value;
            lbl_volume.Text = track_volume.Value.ToString() + "%";
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog(); // Отображает диалоговое окно

            ofd.Multiselect = true; // Позволяет выбрать несколько файлов


            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var file = ofd.SafeFileNames;

                files.AddRange(ofd.SafeFileNames.ToList());
                paths.AddRange(ofd.FileNames.ToList());

                label1.Text = ofd.SafeFileName; // Возвращает имя файла

                for (int x = 0; x < file.Length; x++)
                {
                    track_list.Items.Add(file[x]);

                }

            }

        }

        public void StartVolume()
        {
            track_volume.Value = 10;
            lbl_volume.Text = "10%";

        }
        public void NextClick()
        {
            if (track_list.SelectedIndex < track_list.Items.Count - 1)
            {
                track_list.SelectedIndex = track_list.SelectedIndex + 1;
            }
        }
        public void PreviewClick()
        {
            if (track_list.SelectedIndex > 0)
            {
                track_list.SelectedIndex = track_list.SelectedIndex - 1;
            }
        }
        public void PlayClick()
        {
            player.Ctlcontrols.play();
            label1.Visible = true;
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            NextClick();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PreviewClick();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.play();      
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
            label1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (track_list.SelectedIndex != -1)
                    track_list.Items.RemoveAt(track_list.SelectedIndex);                   
                else
                {
                    MessageBox.Show("выберите элемент");
                }
                
            }
            catch
            
            {             
                    
            }
           
        }
    }
}
