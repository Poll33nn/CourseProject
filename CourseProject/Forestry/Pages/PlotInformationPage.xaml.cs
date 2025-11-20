using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ServiceLayer.DTO_s;

namespace Forestry.Pages
{
    /// <summary>
    /// Логика взаимодействия для PlotInformationPage.xaml
    /// </summary>
    public partial class PlotInformationPage : Page
    {
        public int currentPlotId;
        public PlotInformationPage(int PlotId, string Responsible, int Compartment, int Subcompartment, List<TreesNumberDto> TreesComposition)
        {
            InitializeComponent();
            currentPlotId = PlotId;
            PlotIdLabel.Content = "Лесной участок № " + PlotId;
            ResponsibleLabel.Content = "Ответственный: " + Responsible;
            CompartmentLabel.Content = "Квартал: " + Compartment;
            SubcompartmentLabel.Content = "Выдел: " + Subcompartment;
            TreesDataGrid.ItemsSource = TreesComposition;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.GoBack();
        }
        private void CreateEventButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new CreateEventPage(currentPlotId));
        }
    }
}
