using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using AdonisUI;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Window = System.Windows.Window;

namespace BDownloaderRe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileDirectory = System.IO.Directory.GetCurrentDirectory();

        public MainWindow()
        {
            InitializeComponent();
            Path.Text = ConfigurationManager.AppSettings["path"];
            Cookies.Text = ConfigurationManager.AppSettings["cookies"];
            if (ConfigurationManager.AppSettings["check"] == "1")
            {
                DefaultChooser.IsChecked = true;
            }
            else
            {
                DefaultChooser.IsChecked = false;
            }
        }

        private void Path_GotFocus(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择保存路径";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string folderPath = dialog.SelectedPath;
                Path.Text = folderPath;
            }
            if (Path.Text == "请在此选择保存路径")
            {
                Path.Text = "";
            }
        }

        private void Path_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Path.Text))
            {
                Path.Text = "请在此选择保存路径";
            }
        }

        private void URL_GotFocus(object sender, RoutedEventArgs e)
        {

            if (URL.Text == "请在此输入网址")
            {
                URL.Text = "";
            }

        }

        private void URL_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(URL.Text))
            {
                URL.Text = "请在此输入网址";
            }
        }

        private void Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Cookies_GotFocus(object sender, RoutedEventArgs e)
        {
            string file;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "请选择Cookies文件";
            dialog.Filter = "cookies文件|cookies.txt";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                file = dialog.FileName;
                Cookies.Text = file;
            }
            if (Cookies.Text == "请选择Cookies文件（用于下载大会员视频）")
            {
                Cookies.Text = "";
            }
        }

        private void Cookies_LostFocus(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Cookies.Text))
            {
                Cookies.Text = "请选择Cookies文件（用于下载大会员视频）";
            }
        }

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            YoutubeDL ytdl = new YoutubeDL();
            string ytdlp = fileDirectory + "\\yt-dlp.exe";
            string ffmpeg = fileDirectory + "\\ffmpeg.exe";
            ytdl.YoutubeDLPath = ytdlp;
            ytdl.FFmpegPath = ffmpeg;
            ytdl.OutputFolder = Path.Text;
            //OptionSet options = new OptionSet()
            //{
            //    Cookies = Cookies.Text,
            //};
            OptionSet options = new OptionSet();
            options.Cookies = Cookies.Text;

            //ProgressBar
            var progress = new Progress<DownloadProgress>(p => ProgressBarQAQ.Value = Math.Round(p.Progress * 100, 1));
            var cts = new CancellationTokenSource();

            //var res = await ytdl.RunWithOptions(new string[] { URL.Text }, options, CancellationToken.None);
            var res2 = await ytdl.RunVideoDownload(URL.Text, "bestvideo+bestaudio/best", mergeFormat: DownloadMergeFormat.Mp4, overrideOptions: options, ct: CancellationToken.None, progress: progress);

            string path = res2.Data;
            await Console.Out.WriteLineAsync(path);
        }

        private void DefaultChooser_Checked(object sender, RoutedEventArgs e)
        {
            Path.IsEnabled = false;
            Cookies.IsEnabled = false;
            AddUpdateAppSettings("path", Path.Text);
            AddUpdateAppSettings("cookies", Cookies.Text);
            AddUpdateAppSettings("check", "1");
        }

        private void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private void DefaultChooser_Unchecked(object sender, RoutedEventArgs e)
        {
            Path.IsEnabled = true; 
            Cookies.IsEnabled = true;
            AddUpdateAppSettings("path", "请在此选择保存路径");
            AddUpdateAppSettings("cookies", "请选择Cookies文件（用于下载大会员视频）");
            AddUpdateAppSettings("check", "0");
        }

        private void Copyleft_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void DownloadDanmaku_Click(object sender, RoutedEventArgs e)
        {
            var p = new ProcessAsync("BBDown", $"\"{URL.Text}\" --cookie \"{Cookies.Text}\" --danmaku-only");
        }

        private void HighLevelSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }
    }
}
