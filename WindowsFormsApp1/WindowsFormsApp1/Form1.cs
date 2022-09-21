using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string[] arrAllImages;
        int imageIndex = 0;
        string keepPath, seepPath;

        public Form1()
        {
            InitializeComponent();
        }

        private void saveFile(KeepOrSeep keepOrSeep,string file)
        {
            switch (keepOrSeep)
            {
                case KeepOrSeep.keep:
                    {
                        try { File.Copy(file, keepPath + @"\" + getFilename(file), true); }
                        catch (Exception e) {}
                        break;
                    }
                case KeepOrSeep.seep:
                    {
                        try { File.Copy(file, seepPath + @"\" + getFilename(file), true); }
                        catch (Exception e) { }
                        break;
                    }
            } 
        }

        private string getFilename(string file)
        {
            if (file == null) return"";
            int index = file.LastIndexOf("\\");
            string filename = file.Remove(0, index + 1);
            return filename;

        }

        private void displayImage(Direction direction)
        {
            if (arrAllImages == null) return;
            switch (direction)
            {
                case Direction.forwards:
                    {
                        imageIndex++;
                        try { pictureBox1.Image = Image.FromFile(arrAllImages[imageIndex]); }
                        catch (Exception e) {
                            imageIndex--;
                            //MessageBox.Show("keep failed - Path error");
                        }
                        break;
                    }
                case Direction.backwards:
                    {
                        imageIndex--;
                        try { pictureBox1.Image = Image.FromFile(arrAllImages[imageIndex]); }
                        catch (Exception e) { 
                            imageIndex++;
                            //MessageBox.Show("seep failed - Path error");
                        }
                        break;
                    }
                case Direction.stay:
                    {
                        try { pictureBox1.Image = Image.FromFile(arrAllImages[imageIndex]); }
                        catch (Exception e) { }
                        break;
                    }
                default: break;
            }
            resizeImageBox(arrAllImages[imageIndex]);
            //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void resizeImageBox(string file)
        {
            //  int height = pictureBox1.Height;
            //  int widht = pictureBox1.Width;

            Bitmap image = new Bitmap(file);

            // MessageBox.Show(image1.Width.ToString());
            /*
            pictureBox1.Height = image.Height;
            pictureBox1.Width = image.Width;


            if ((pictureBox1.Height > this.Height)&& (pictureBox1.Width < this.Width)) {

                pictureBox1.Height = (pictureBox1.Height / (image.Height/ this.Height)) - 150;
                pictureBox1.Width = (pictureBox1.Width / (image.Height / this.Height)) - 150;
            }

            if ((pictureBox1.Height > this.Width)&&(pictureBox1.Height < this.Height)){

                pictureBox1.Width = (pictureBox1.Width / (image.Height / this.Height)) - 150;
                pictureBox1.Height = (pictureBox1.Height / (image.Width / this.Width)) - 150;
            }

            */

        }

        enum Direction
        {
            forwards,
            backwards,
            stay
        }

        GlobalKeyboardHook gHook;

        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (arrAllImages == null) return;
            
            switch (((char)e.KeyValue).ToString())
            {
                case "'":
                    displayImage(Direction.forwards);
                    break;
                case "%":
                    displayImage(Direction.backwards);
                    break;
                case "(":
                    saveFile(KeepOrSeep.seep, arrAllImages[imageIndex]);
                    displayImage(Direction.forwards);
                    break;
                case "&":
                    saveFile(KeepOrSeep.keep, arrAllImages[imageIndex]);
                    displayImage(Direction.forwards);
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook(); // Create a new GlobalKeyboardHook
                                              // Declare a KeyDown Event
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            // Add the keys you want to hook to the HookedKeys list
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            gHook.hook();
        }

        private void keepPfadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            keepPath = folderBrowserDialog1.SelectedPath.ToString();
            toolStripTextBox1.Text = keepPath;
        }

        private void seepPfadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            seepPath = folderBrowserDialog2.SelectedPath.ToString();
            toolStripTextBox2.Text = seepPath;
        }

        private void bilderÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;                // guard to prevent fail of FileDialog
            arrAllImages = openFileDialog1.FileNames;                                   // read all Files into array
            displayImage(Direction.stay);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            displayImage(Direction.forwards);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            displayImage(Direction.backwards);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (arrAllImages == null) return;
            saveFile(KeepOrSeep.keep, arrAllImages[imageIndex]);
            displayImage(Direction.forwards);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (arrAllImages == null) return;
            saveFile(KeepOrSeep.seep, arrAllImages[imageIndex]);
            displayImage(Direction.forwards);
        }

        enum KeepOrSeep
        {
            keep,
            seep
        }
    }
}
