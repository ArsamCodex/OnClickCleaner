using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using AngleSharp.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace OnClickCleaner
{
    public partial class Form1 : Form

    {
        string path = @"C:\Users\Armin\AppData\Local\Temp";
        string Prefetchpath = @"C:\Windows\Prefetch";
        string apiUrl = "https://api.coingecko.com/api/v3/coins/bitcoin/market_chart?vs_currency=usd&days=100";
        Color colorIncrease = Color.Green;
        Color colorDecrease = Color.Red;
        decimal previousPrice ;     // C:\Users\Armin\AppData\Local\Temp
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
        private async Task DownloadAudioFromUrl(string videoUrl, string destinationPath)
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
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Please enter a value in textBox3.");
            }
            else
            {
                errorProvider1.Clear();

                richTextBox1.Text = "Please Wait Until U get Done";
                await DownloadAudioFromUrl(textBox3.Text, textBox4.Text);
                richTextBox1.AppendText(Environment.NewLine + "File Downloaded Successfully. Operation Done!");
                richTextBox1.AppendText(Environment.NewLine + "File Downloaded Here: " + textBox4.Text);
            }


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

        private Timer timer;
        private async void tabPage5_Click(object sender, EventArgs e)
        {
            try
            {
                await UpdatePrices(); // Call the method to update prices initially

                decimal movingAverage = await CalculateMovingAverage(apiUrl);
                string formattedMovingAverage = movingAverage.ToString("0.00");
                label14.Text = formattedMovingAverage;






                timer = new Timer(); // Initialize the timer
                timer.Interval = 15000; // Set the interval to 3 seconds
                timer.Tick += async (s, args) => await UpdatePrices(); // Assign the update method to the tick event
                timer.Start(); // Start the timer


            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the API request or timer setup
                //MessageBox.Show("An error occurred: " + ex.Message);
                richTextBox1.AppendText(ex.Message);
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private async void button5_Click(object sender, EventArgs e)
        {


        }
        public async Task<string> GetBinancePrice(string symbol)
        {
            try
            {
                string apiUrl = $"https://www.binance.com/api/v3/ticker/price?symbol={symbol}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Parse the JSON response and extract the price
                    JObject data = JObject.Parse(responseBody);
                    string price = (string)data["price"];

                    return price;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the API request
                richTextBox1.AppendText("No Internet Connetcio Valid"+ex.Message);
                return null;

            }
        }
        private async Task UpdatePrices()
        {
           
            try
            {
                label13.Text = await GetBinancePrice("BTCUSDT");
                label12.Text = await GetBinancePrice("ETHUSDT");
                label21.Text = await GetBinancePrice("LTCUSDT");
                label22.Text = await GetBinancePrice("FTMUSDT");
                label23.Text = await GetBinancePrice("BNBUSDT");

                 previousPrice = decimal.Parse(await GetBinancePrice("BTCUSDT"));
               // previousPrice = decimal.Parse(label13.Text);
                


                // labelPrice.Text = label13.Text;

                // Compare the current price with the previous price and set the label color accordingly
                if (previousPrice < decimal.Parse(label13.Text))
                {
                    richTextBox1.AppendText( "Down Down Down");
                    label24.BackColor = colorDecrease;
                }
                else if (previousPrice > decimal.Parse(label13.Text))
                {
                    label24.BackColor = colorIncrease;
                    richTextBox1.AppendText("UP UP UP");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the API request
                // MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private void label12_Click(object sender, EventArgs e)
        {

        }



        private void label14_Click(object sender, EventArgs e)
        {

        }
        static async Task<decimal> CalculateMovingAverage(string apiUrl)
        {

            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(apiUrl);
                CoinGeckoResponse data = JsonConvert.DeserializeObject<CoinGeckoResponse>(response);

                decimal[] prices = data.Prices.Select(p => p[1]).ToArray();
                decimal sum = prices.Sum();
                decimal movingAverage = sum / prices.Length;

                return movingAverage;
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            string jpgFilePath = textBox5.Text;
            string icoFilePath = textBox6.Text;
            ConvertJpgToIcon(jpgFilePath, icoFilePath);
            richTextBox1.AppendText("ICO Extention Done");
        }
        static void ConvertJpgToIcon(string jpgFilePath, string icoFilePath)
        {
            using (Bitmap bitmap = new Bitmap(jpgFilePath))
            {
                Icon icon = Icon.FromHandle(bitmap.GetHicon());

                using (System.IO.FileStream stream = new System.IO.FileStream(icoFilePath, System.IO.FileMode.Create))
                {
                    icon.Save(stream);
                }
            }
        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }
    }

    public class CoinGeckoResponse
    {
        [JsonProperty("prices")]
        public decimal[][] Prices { get; set; }
    }
}






