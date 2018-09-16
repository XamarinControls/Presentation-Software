using Adamson_Graduation_Software.ViewModels;
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

namespace Adamson_Graduation_Software
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm = new MainViewModel();
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            var key = e.Key;
            switch (key)
            {
                case Key.Left:
                    _vm.NavigateKey(-1);
                    break;
                case Key.Right:
                    _vm.NavigateKey(1);
                    break;
                case Key.F:
                    navigation.Visibility = navigation.IsVisible ? Visibility.Collapsed : Visibility.Visible;
                    break;
            };
        }
    }
}
