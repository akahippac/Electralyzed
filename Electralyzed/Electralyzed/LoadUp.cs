﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinSCP;

namespace Electralyzed
{
    public partial class LoadUp : Form
    {
        public LoadUp()
        {
            InitializeComponent();
        }

        //GLOBAL VARIABLES
        string ip, username, password;

        private void LoadUp_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Application.StartupPath + @"\7Z\7z.exe"))
            {
                MessageBox.Show(@"7Zip.exe is not in \7Z", "ERROR!", MessageBoxButtons.OK);
                Application.Exit();
            }
            if (!File.Exists(Application.StartupPath + @"\7Z\7z.dll"))
            {
                MessageBox.Show(@"7Zip.dll is not in \7Z", "ERROR!", MessageBoxButtons.OK);
                Application.Exit();
            }
            Auto_U();
            Ver_Label.Text += Properties.Settings.Default.versionNum;
        }

        private void Exit_Label_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Connect_Button_Click(object sender, EventArgs e)
        {
            ip = IP_TextBox.Text;;
            password = Password_TextBox.Text;

            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = ip,
                UserName = "root",
                Password = password,
                GiveUpSecurityAndAcceptAnySshHostKey = true
            };

            C_Label.Text = "Attempting to connect...";

            using (Session session = new Session())
            {
                session.Open(sessionOptions);
                C_Label.Text = "Success!";
                session.Close();
                this.Hide();
                Electralyzed EM = new Electralyzed(IP_TextBox.Text, Password_TextBox.Text);
                EM.ShowDialog();
            }
        }

        private void G_Label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/6ilent/Electralyzed");
        }

        private void Auto_U()
        {
            WebClient a_fileD = new WebClient();
            if (File.Exists(Application.StartupPath + @"\a_version.txt"))
            {
                File.Delete(Application.StartupPath + @"\a_version.txt");
            }
            a_fileD.DownloadFile("https://raw.githubusercontent.com/6ilent/Electralyzed/master/Electralyzed/Other%20Files/version.txt", Application.StartupPath + @"\a_version.txt");
            string e_version = Properties.Settings.Default.e_versionNum;
            string a_version = File.ReadAllText(Application.StartupPath + @"\a_version.txt");

            if (e_version != a_version)
            {
                DialogResult n_version = MessageBox.Show("You are using an outdated version of Electralyzed! Would you like me to take you take you to GitHub to download the latest version?", "Please Update!", MessageBoxButtons.YesNo);
                if (n_version == DialogResult.Yes)
                {
                    Process.Start("https://github.com/6ilent/Electralyzed/releases");
                    Application.Exit();
                }
            }
        }
    }
}
