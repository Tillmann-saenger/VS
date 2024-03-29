﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace passwortVerdampfer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName.Equals("")|| saveFileDialog1.FileName.Equals(""))
            {
                MessageBox.Show("Dateipfade wählen");
                return;
            }
            string filepath = openFileDialog1.FileName;
            Stream str = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(str);
            var arlist = new System.Collections.ArrayList();

            int i = 0;
            int j = 0;
            while (!sr.EndOfStream)
            {
                i++;
                String temp = sr.ReadLine();
                try
                {
                    if ((!temp.Contains(" ")) &&
                        (temp.Length > 9) && (
                            (temp.Contains(",")) ||
                            (temp.Contains(".")) ||
                            (temp.Contains("-")) ||
                            (temp.Contains("#")) ||
                            (temp.Contains("+")) ||
                            (temp.Contains("_")) ||
                            (temp.Contains(":")) ||
                            (temp.Contains("{")) ||
                            (temp.Contains("|")) ||
                            (temp.Contains("}")) ||
                            (temp.Contains("=")) ||
                            (temp.Contains("$")) ||
                            (temp.Contains("(")) ||
                            (temp.Contains(")")))
                        && (temp.Any(char.IsUpper))
                        && (temp.Any(char.IsLower))
                        && (temp.Any(char.IsDigit)))
                    {
                        arlist.Add(temp);
                        j++;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Fehler beim einlesen der Datei");
                }
            }
            sr.Close();
            label3.Text = i.ToString();
            label4.Text = j.ToString();

            using (TextWriter writer = File.CreateText(saveFileDialog1.FileName))
            {
                foreach (string actor in arlist)
                {
                    writer.WriteLine(actor);
                }
            }
            MessageBox.Show("Fertig");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
    }
}
