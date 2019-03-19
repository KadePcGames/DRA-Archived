﻿using DesktopRemoteAdmin_UI.Libs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopRemoteAdmin_UI
{
    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                NetworkStream s = Tcp.Connect(txt_IP.Text);

                Tcp.SendData("login|" + txt_Pass.Text,s);

                string[] data = Tcp.Recieve(s);

                if (data[0] != "true")
                    MessageBox.Show("Incorrect password!", "Desktop Remote Admin Login");
                else
                {
                    MessageBox.Show("Welcome!", "Desktop Remote Admin Login");
                    Variables.CachePassword = txt_Pass.Text;
                    new Main(txt_IP.Text).Show();
                    Hide();
                }
            }
            catch
            {
                MessageBox.Show("Failed to connect to the server!", "Desktop Remote Admin Login");
            }

        }

        private void InnerRim_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}