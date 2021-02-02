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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DCOM.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowModel();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch { }
        }

        private DoubleAnimation animationRadiusXProperty = new DoubleAnimation()
        {
            From = 0,
            To = 150,
            Duration = new Duration(TimeSpan.FromSeconds(1))
        };

        private DoubleAnimation targetOpacityProperty = new DoubleAnimation()
        {
            From = 0.3,
            To = 0,
            Duration = new Duration(TimeSpan.FromSeconds(1))
        };

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;

            //半径扩散动画
            var target = grid.FindName("ellipse") as EllipseGeometry;
            //target.Center = Mouse.GetPosition(this);

            //透明度动画
            var target2 = grid.FindName("path") as Path;

            target.BeginAnimation(EllipseGeometry.RadiusXProperty, animationRadiusXProperty);
            target2.BeginAnimation(Path.OpacityProperty, targetOpacityProperty);

        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Frame_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            UpdateFrameContext();
        }

        private void Frame_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateFrameContext();
        }

        private void UpdateFrameContext()
        {
            var content = this.pageFrame.Content as FrameworkElement;
            if (content == null)
            {
                return;
            }
            content.DataContext = this.pageFrame.DataContext;
        }

        private void Nav_Selected_Home(object sender, RoutedEventArgs e)
        {
            if (pageFrame == null) return;
            this.pageFrame.Navigate(new Uri("DCOM;component/view/MainPages/MainPage.xaml", UriKind.Relative));
        }

        private void Nav_Selected_Setting(object sender, RoutedEventArgs e)
        {
            if (pageFrame == null) return;
            this.pageFrame.Navigate(new Uri("DCOM;component/view/MainPages/SettingPage.xaml", UriKind.Relative));

        }

        private void Nav_Selected_About(object sender, RoutedEventArgs e)
        {
            if (pageFrame == null) return;
            this.pageFrame.Navigate(new Uri("DCOM;component/view/MainPages/AboutPage.xaml", UriKind.Relative));
        }
    }
}
