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

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;                // guard to prevent fail of FileDialog
            arrAllImages = openFileDialog1.FileNames;                                   // read all Files into array
            displayImage(Direction.stay);
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            displayImage(Direction.forwards);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            displayImage(Direction.backwards);
        }

        private void keepButton_Click(object sender, EventArgs e)
        {
            saveFile(KeepOrSeep.keep, arrAllImages[imageIndex]);
            displayImage(Direction.forwards); 
        }

        private void seepButton_Click(object sender, EventArgs e)
        {
            saveFile(KeepOrSeep.seep, arrAllImages[imageIndex]);
            displayImage(Direction.forwards);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            keepPath = folderBrowserDialog1.SelectedPath.ToString();
            textBox2.Text = keepPath;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            seepPath = folderBrowserDialog2.SelectedPath.ToString();
            textBox3.Text = seepPath;
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
            int index = file.LastIndexOf("\\");
            string filename = file.Remove(0,index+1);
            return filename;
        }

        private void displayImage(Direction direction)
        {

            switch (direction)
            {
                case Direction.forwards:
                    {
                        imageIndex++;
                        try { pictureBox1.Image = Image.FromFile(arrAllImages[imageIndex]); }
                        catch (Exception e) {
                            imageIndex--;
                            MessageBox.Show("keep failed - Path error");
                        }
                        break;
                    }
                case Direction.backwards:
                    {
                        imageIndex--;
                        try { pictureBox1.Image = Image.FromFile(arrAllImages[imageIndex]); }
                        catch (Exception e) { 
                            imageIndex++;
                            MessageBox.Show("seep failed - Path error");
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
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void resizeImageBox(string file)
        {
          //  int height = pictureBox1.Height;
          //  int widht = pictureBox1.Width;

           // Bitmap image1 = new Bitmap(file);

            // MessageBox.Show(image1.Width.ToString());

        }

        enum Direction
        {
            forwards,
            backwards,
            stay
        }

        enum KeepOrSeep
        {
            keep,
            seep
        }
    }
}
