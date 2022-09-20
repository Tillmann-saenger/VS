using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;                // guard to prevent fail of FileDialog
            arrAllImages = openFileDialog1.FileNames;                          // read all Files into array
            displayImage(Direction.stay);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void displayImage(Direction direction)
        {

            switch (direction)
            {
                case Direction.forwards:
                    {
                        imageIndex++;
                        try { pictureBox1.Image = Image.FromFile(arrAllImages[imageIndex]); }
                        catch (Exception e) { imageIndex--; }
                        break;
                    }
                case Direction.backwards:
                    {
                        imageIndex--;
                        try { pictureBox1.Image = Image.FromFile(arrAllImages[imageIndex]); }
                        catch (Exception e) { imageIndex++; }
                        break;
                    }
                case Direction.stay:
                    {
                        try{pictureBox1.Image = Image.FromFile(arrAllImages[imageIndex]);}
                        catch (Exception e) { }
                        break;
                    }
                default:
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            displayImage(Direction.forwards);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            displayImage(Direction.backwards);
        }

        enum Direction
        {
            forwards,
            backwards,
            stay
        }
    }
}
