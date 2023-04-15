using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;
using System.IO; // Добавление библиотеки taglib.cs
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using System.Drawing.Drawing2D;

namespace MusicPlayer
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            
            InitializeComponent();
            track_volume.Value = 20;
            lbl_volume.Text = "20%";

        }



        string[] paths, files;

        private void track_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            player.URL = paths[track_list.SelectedIndex];
            label1.Text = files[track_list.SelectedIndex]; // Вывод названия при клике 
            player.Ctlcontrols.play();
                    
            try
            {
                var file = TagLib.File.Create(paths[track_list.SelectedIndex]);              
                var bin = (byte[])(file.Tag.Pictures[0].Data.Data);             
                pic_art.Image = Image.FromStream(new MemoryStream(bin));
                

            }
            catch { }


        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.play();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (track_list.SelectedIndex < track_list.Items.Count - 1)
            {
                track_list.SelectedIndex = track_list.SelectedIndex + 1;
            }
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            if (track_list.SelectedIndex > 0)
            {
                track_list.SelectedIndex = track_list.SelectedIndex - 1;
            }
        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            p_bar.ForeColor = Color.Orange;
            p_bar.BackColor = Color.Black;
            if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                p_bar.Maximum = (int)player.Ctlcontrols.currentItem.duration;
                p_bar.Value = (int)player.Ctlcontrols.currentPosition;
            }
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

                files = ofd.SafeFileNames;
                paths = ofd.FileNames;

                label1.Text = ofd.SafeFileName; // Возвращает имя файла

                for (int x = 0; x < files.Length; x++)
                {
                    track_list.Items.Add(files[x]);

                }
               
            }
            
        }

    }
}
