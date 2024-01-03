using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BDownloaderRe
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : Window
    {
        BDownloadReAssembly assembly = new BDownloadReAssembly();
        public string enableCookieSetting { get; set; }
        public string? enablePathDefaulter { get; set; }

        private string? enableCookieValue;
        private string? pathDefaulter;
        public int i = 0;
        public Settings()
        {
            pathDefaulter = ConfigurationManager.AppSettings["defaultPath"];
            enablePathDefaulter = pathDefaulter;
            enableCookieValue = ConfigurationManager.AppSettings["enableCookie"];
            InitializeComponent();
            InitializeForSettings();
        }

        private void InitializeForSettings()
        {
            if (enableCookieValue == "0")
            {
                EnableCookie.IsChecked = false;
                CookieDefault.IsEnabled = false;
            }
            else
            {
                EnableCookie.IsChecked = true;
                CookieDefault.IsEnabled = true;
            }
            if(pathDefaulter == "0")
            {
                PathDefault.IsChecked = false;
            }
            else
            {
                PathDefault.IsChecked = true;
            }
            i++;
        }

        private void ProxyEnabledCheck_Checked(object sender, RoutedEventArgs e)
        {
            LocalIPProxy.IsEnabled = true;
            LocalProxy.IsEnabled = true;
        }

        private void EnableCookie_Checked(object sender, RoutedEventArgs e)
        {
            assembly.AddUpdateAppSettings("enableCookie", "1");
            enableCookieSetting = "1";
            if (i == 1)
            {
                CookieDefault.IsEnabled = true;
            }
        }

        private void EnableCookie_Unchecked(object sender, RoutedEventArgs e)
        {
            assembly.AddUpdateAppSettings("enableCookie", "0");
            enableCookieSetting = "0";
            if (i == 1)
            {
                CookieDefault.IsEnabled = false;
            }
        }

        private void PathDefault_Checked(object sender, RoutedEventArgs e)
        {
            assembly.AddUpdateAppSettings("defaultPath", "1");
        }

        private void PathDefault_Unchecked(object sender, RoutedEventArgs e)
        {
            assembly.AddUpdateAppSettings("defaultPath", "0");
        }
    }
}
