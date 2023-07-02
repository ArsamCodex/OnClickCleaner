using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace OnClickCleaner
{
    public partial class Form1 : Form

    {
        string path = @"C:\Users\Armin\AppData\Local\Temp";
        string Prefetchpath = @"C:\Windows\Prefetch";
        // C:\Users\Armin\AppData\Local\Temp
        //C:\Windows\Prefetch
        //C:\Windows\Temp
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


        }


        public void RemoveAllFilesInDirectory(string path)

        {
            // List<string> FailedfILEToDelete = new List<string>();

            try
            {
                // Get the list of files in the specified directory
                // string[] files = Directory.GetFiles(path);

                foreach (string file in Directory.GetFiles(path))
                {
                    try
                    {
                        File.Delete(file);
                        richTextBox1.Text = file;
                    }
                    catch (IOException)
                    {

                    }
                }

                foreach (string subDirectory in Directory.GetDirectories(path))
                {
                    try
                    {
                        RemoveAllFilesInDirectory(subDirectory); // Recursively remove subdirectory contents
                        Directory.Delete(subDirectory); // Remove the empty directory
                    }
                    catch (IOException)
                    {

                    }
                }
            }
            catch (Exception)
            {
                // Console.WriteLine($"An error occurred while removing files: {ex.Message}");

            }
        }
        public void CleanUpPrefetch(string path)

        {
            // List<string> FailedfILEToDelete = new List<string>();

            try
            {
                // Get the list of files in the specified directory
                // string[] files = Directory.GetFiles(path);

                foreach (string file in Directory.GetFiles(path))
                {
                    try
                    {
                        File.Delete(file);
                        richTextBox1.Text = file;
                    }
                    catch (IOException)
                    {

                    }
                }

                foreach (string subDirectory in Directory.GetDirectories(path))
                {
                    try
                    {
                        RemoveAllFilesInDirectory(subDirectory); // Recursively remove subdirectory contents
                        Directory.Delete(subDirectory); // Remove the empty directory
                    }
                    catch (IOException)
                    {

                    }
                }
            }
            catch (Exception)
            {
                // Console.WriteLine($"An error occurred while removing files: {ex.Message}");

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                RemoveAllFilesInDirectory(path);
            }
            if (checkBox2.Checked)
            {
                CleanUpPrefetch(Prefetchpath);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public void DiskPartManager(string DiskPart, string PartiotionNumber)
        {
            string script = $"select disk {DiskPart}\n" +
                       $"select partition {PartiotionNumber}\n" +
                       $"delete partition override\n" +
                       $"exit";

            // Create a ProcessStartInfo instance for Diskpart
            ProcessStartInfo psi = new ProcessStartInfo("diskpart.exe")
            {
                UseShellExecute = false,
                RedirectStandardInput = true,
                CreateNoWindow = true
            };

            // Start the Diskpart process
            Process process = Process.Start(psi);

            // Pass the script to Diskpart's standard input
            process.StandardInput.WriteLine(script);
            process.StandardInput.Close();

            // Wait for the process to exit
            process.WaitForExit();

            Console.WriteLine("Recovery partition removed successfully.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DiskPartManager(textBox1.Text, textBox2.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {


        }
        public static string[] GetInstalledPrograms()
        {
            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                string[] programNames = rk.GetSubKeyNames();
                string[] installedPrograms = new string[programNames.Length];

                for (int i = 0; i < programNames.Length; i++)
                {
                    using (RegistryKey subKey = rk.OpenSubKey(programNames[i]))
                    {
                        installedPrograms[i] = subKey.GetValue("DisplayName") as string;
                    }
                }

                return installedPrograms;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            List<string> lstInstalledPrograms = new List<string>();
            string[] programs = GetInstalledPrograms();

            foreach (string program in programs)
            {
                lstInstalledPrograms.Add(program);

            }

            label6.Text = lstInstalledPrograms.Count().ToString();
            PartitionFreeSpaceReporter();




        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }




        private void richTextBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        public void PartitionFreeSpaceReporter()
        {
            //  List<DriveInfo> PrtinToUser = new List<DriveInfo>();
            DriveInfo[] drives = DriveInfo.GetDrives();

            // Iterate through each drive and store the drive information
            foreach (DriveInfo drive in drives)
            {
                try
                {
                    richTextBox3.AppendText("Drive: " + drive.Name + Environment.NewLine);
                    richTextBox3.AppendText("Free space: " + drive.AvailableFreeSpace + " bytes" + Environment.NewLine);
                    richTextBox3.AppendText("------------------------------" + Environment.NewLine);
                }
                catch (Exception)
                {
                    //HAndle exception
                }
            }





        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
        private  async Task DownloadAudioFromUrl(string videoUrl, string destinationPath)
        {
            try
            {
                var youtube = new YoutubeClient();
                var video = await youtube.Videos.GetAsync(videoUrl);

                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                var audioStreams = streamManifest.GetAudioStreams().Where(s => s.Container == YoutubeExplode.Videos.Streams.Container.Mp4);
                var streamInfo = audioStreams.OrderByDescending(s => s.Bitrate).FirstOrDefault();

                if (streamInfo != null)
                {
                    await youtube.Videos.Streams.DownloadAsync(streamInfo, destinationPath);
                }
                else
                {
                   //  Console.WriteLine("No audio stream found for the given video.");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("An error occurred while downloading the audio: " + ex.Message);
            }
        }

        private async void button4_Click_1(object sender, EventArgs e)
        {
           await DownloadAudioFromUrl(textBox3.Text, textBox4.Text);
            label9.Text = "Done";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}