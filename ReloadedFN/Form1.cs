using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReloadedFN
{
    public partial class Form1 : Form
    {

        [DllImport("user32.DLL")]
        private static extern void ReleaseCapture();

        // Token: 0x06000452 RID: 1106
        [DllImport("user32.DLL")]
        private static extern void SendMessage(IntPtr one, int two, int three, int four);

        // Token: 0x06000453 RID: 1107
        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public Form1()
        {
            InitializeComponent();

            Findinstall();
            Console.ReadLine();

            Process[] processesByName = Process.GetProcessesByName("Fortnitelauncher");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            processesByName = Process.GetProcessesByName("FortniteClient-Win64-Shipping_EAC");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            processesByName = Process.GetProcessesByName("FortniteClient-Win64-Shipping");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            processesByName = Process.GetProcessesByName("FortniteClient-Win64-Shipping_BE");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
        }

        public static string GetDirectory()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            ///Finds Epics Program Data Directory
            if (Directory.Exists(@"C:\ProgramData\Epic"))
            {
                return @"C:\ProgramData\Epic";
            }
            if (Directory.Exists(@"D:\ProgramData\Epic"))
            {
                return @"D:\ProgramData\Epic";
            }
            if (Directory.Exists(@"E:\ProgramData\Epic"))
            {
                return @"E:\ProgramData\Epic";
            }
            if (Directory.Exists(@"F:\ProgramData\Epic"))
            {
                return @"F:\ProgramData\Epic";
            }
            return "error"; ///Returns Error If It Can't Find it
        }
        public static void Findinstall()///Reads Epicdata Json And Checks If Its Fortnite Install Path
        {
            string EpicData = GetDirectory();
            if (File.Exists(EpicData + @"\UnrealEngineLauncher\LauncherInstalled.dat"))
            {
                var jsonObject = JObject.Parse(File.ReadAllText(EpicData + @"\UnrealEngineLauncher\LauncherInstalled.dat")); ///Formats The Json
                foreach (var Installs in jsonObject["InstallationList"]) ///Go Throughs Every Json Value
                {
                    if (Directory.Exists(Installs["InstallLocation"] + @"\FortniteGame")) ///Checks If Path Contains The FortniteGame Folder
                    {
                        Settings1.Default.InstallPath = Installs["InstallLocation"] + @"\FortniteGame\Binaries\Win64";
                        Settings1.Default.Save();
                    }
                }
            }
            else
            {
                Console.WriteLine("Couldent Find LauncherInstalled.dat Make Sure You Have Epic Games Installed!"); ///Writes This Error If It Can't Find The File Called LauncherInstalled.dat
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Form1.ReleaseCapture();
            Form1.SendMessage(base.Handle, 274, 61458, 0);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Path.Text = Settings1.Default.InstallPath;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.InitialDirectory = "C:\\";
                dialog.IsFolderPicker = true;

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    Settings1.Default.InstallPath = dialog.FileName;
                    Settings1.Default.Save();
                    Path.Text = Settings1.Default.InstallPath;
                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if(!File.Exists(Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_BE.exe.reloadedfn"))
            {
                File.Copy(Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_BE.exe", Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_BE.exe.reloadedfn");
                File.Copy(Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_EAC.exe", Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_EAC.exe.reloadedfn");
                File.Delete(Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_BE.exe");
                File.Delete(Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_EAC.exe");
            }
            else
            {

            }
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://cdn.discordapp.com/attachments/863436114584797185/868884682501128232/FortniteClient-Win64-Shipping_BE.exe", Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_BE.exe");
            webClient.DownloadFile("https://cdn.discordapp.com/attachments/863436114584797185/868884683583275088/FortniteClient-Win64-Shipping_EAC.exe", Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_EAC.exe");
            if (!File.Exists(Settings1.Default.InstallPath + "\\ReloadedFN.dll"))
            {
                webClient.DownloadFile("https://cdn.discordapp.com/attachments/863436114584797185/868873096554246204/ReloadedFN.dll", Settings1.Default.InstallPath + "\\ReloadedFN.dll");
            }
            MessageBox.Show("Successfully Installed ReloadedFN", "Reloaded Hybrid");
            notifyIcon1.ShowBalloonTip(1000, "Reloaded Hybrid", "Successfully Installed ReloadedFN!", ToolTipIcon.None);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Process.Start("com.epicgames.launcher://apps/Fortnite?action=launch");
            MessageBox.Show("Successfully Started Fortnite!", "Reloaded Hybrid");
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_BE.exe.reloadedfn"))
            {
                File.Delete(Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_BE.exe");
                File.Move(Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_BE.exe.reloadedfn", Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_BE.exe");
                File.Delete(Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_EAC.exe");
                File.Move(Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_EAC.exe.reloadedfn", Settings1.Default.InstallPath + "\\FortniteClient-Win64-Shipping_EAC.exe");
            } else
            {

            }
            WebClient unwebClient = new WebClient();
            if (File.Exists(Settings1.Default.InstallPath + "\\ReloadedFN.dll"))
            {
                File.Delete(Settings1.Default.InstallPath + "\\ReloadedFN.dll");
            }
            MessageBox.Show("Successfully Uninstalled ReloadedFN!", "Reloaded Hybrid");
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Process[] processesByName = Process.GetProcessesByName("Fortnitelauncher");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            processesByName = Process.GetProcessesByName("FortniteClient-Win64-Shipping_EAC");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            processesByName = Process.GetProcessesByName("FortniteClient-Win64-Shipping");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            processesByName = Process.GetProcessesByName("FortniteClient-Win64-Shipping_BE");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            MessageBox.Show("You have Successfully killed Fortnite!", "Reloaded Hybrid");
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            Process[] processesByName = Process.GetProcessesByName("EpicGamesLauncher");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            processesByName = Process.GetProcessesByName("EpicWebHelper.exe");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            processesByName = Process.GetProcessesByName("EpicWebHelper.exe");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            processesByName = Process.GetProcessesByName("EpicWebHelper.exe");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            processesByName = Process.GetProcessesByName("EpicWebHelper.exe");
            for (int i = 0; i < processesByName.Length; i++)
            {
                processesByName[i].Kill();
            }
            MessageBox.Show("You have Successfully killed Epic Games!", "Reloaded Hybrid");
        }

            private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Process.Start("https://dsc.gg/reloaded");
            MessageBox.Show("Thank u for Joining our Discord Server! c:", "Reloaded Hybrid");
        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.InitialDirectory = "C:\\";
                dialog.IsFolderPicker = true;

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    Settings1.Default.InstallPath = dialog.FileName;
                    Settings1.Default.Save();
                    Path.Text = Settings1.Default.InstallPath;
                }
            }
        }
    }
}
