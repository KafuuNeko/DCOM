using DCOM.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DCOM.View.MainPages
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            this.DataContext = new MainWindowModel();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var obj = sender as TextBox;
            if(obj != null)
            {
                obj.ScrollToEnd();
            }
        }
    }
}
