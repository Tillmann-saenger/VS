﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp23
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        GlobalKeyboardHook gHook;

       string ausgabe;

        private void Form1_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook(); // Create a new GlobalKeyboardHook
                                              // Declare a KeyDown Event
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            // Add the keys you want to hook to the HookedKeys list
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            gHook.HookedKeys.Add(key);
        }

        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            textBox1.Text += ((char)e.KeyValue).ToString();
            ausgabe = ((char)e.KeyValue).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gHook.hook();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gHook.unhook();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gHook.unhook();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //------------------------------------------------

            MessageBox.Show(ausgabe);



            //------------------------------------------------
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
