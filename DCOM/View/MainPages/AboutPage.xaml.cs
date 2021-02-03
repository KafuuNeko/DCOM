using DCOM.View.UControl;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace DCOM.View.MainPages
{
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void UrlButton_Click(object sender, RoutedEventArgs e)
        {
            var link = sender as UrlButton;
            Process.Start(new ProcessStartInfo(link.Url));
        }
    }
}
