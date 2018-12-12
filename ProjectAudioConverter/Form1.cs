using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace ProjectAudioConverter
{
    public partial class Form1 : Form
    {
        OpenFileDialog open = new OpenFileDialog();
        SaveFileDialog save = new SaveFileDialog();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            open.Filter = "MP3 File (*.mp3)|*.mp3";
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open.FileName;
                label1.Text = "MP3 File Loaded";
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            save.Filter = "WAV File (*.wav)|*.wav";
            if (save.ShowDialog() != DialogResult.OK) return;

            textBox2.Text = save.FileName;

            try
            {
                using (Mp3FileReader mp3 = new Mp3FileReader(open.FileName))
                {
                    using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                    {
                        WaveFileWriter.CreateWaveFile(save.FileName, pcm);
                        progressBar1.Value = 100;
                        label1.Text = "Mp3 File Successfully Converted to Wav File";
                    }
                }
            }
            catch
            {
                progressBar1.Value = 0;
            }
        }

      

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
