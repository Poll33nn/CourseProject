using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Forestry.Pages;

namespace Forestry
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DefaultWindow : Window
    {
        public DefaultWindow()
        {
            InitializeComponent();
            App.CurrentFrame = DefaultFrame;
            DefaultFrame.Navigate(new AuthPage());
        }
    }
}