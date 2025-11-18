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

namespace Forestry.Pages
{
    /// <summary>
    /// Логика взаимодействия для PlotChangePage.xaml
    /// </summary>
    public partial class PlotChangePage : Page
    {
        public PlotChangePage(int PlotId, string Responsible, int Compartment, int Subcompartment)
        {
            InitializeComponent();
            PlotNumberTextBox.Text = PlotId.ToString();
            UserComboBox.SelectedItem = Responsible;
            CompartmentTextBox.Text = Compartment.ToString();
            SubcompartmentTextBox.Text = Subcompartment.ToString();
        }

        private void ChangePlotButton_Click(object sender, RoutedEventArgs e)
        {
            //контроллер изменения
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.GoBack();
        }
    }
}
