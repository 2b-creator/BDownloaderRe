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
        private string enableCookieValue;
        public Settings()
        {
            enableCookieValue = ConfigurationManager.AppSettings["enableCookie"];
            InitializeComponent();
            InitializeForSettings();
        }

        private void InitializeForSettings()
        {
            if (enableCookieValue == "0")
            {
                EnableCookie.IsChecked = false;
            }
            else
            {
                EnableCookie.IsChecked = true;
            }
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
        }

        private void EnableCookie_Unchecked(object sender, RoutedEventArgs e)
        {
            assembly.AddUpdateAppSettings("enableCookie", "0");
            enableCookieSetting = "0";
        }
    }
}
