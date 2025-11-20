using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly ApiService _apiService = new();
        public MainPage()
        {
            InitializeComponent();
            LoadForestPlots();
        }

        private async Task LoadForestPlots()
        {
            ForestPlotsList.ItemsSource = await _apiService.GetForestPlotsAsync();
        }

        private void OnInfoClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var plot = button?.Tag as ForestPlotDto;
            if (plot != null)
                App.CurrentFrame.Navigate(new PlotInformationPage(plot.PlotId, plot.Responsible, plot.Compartment, plot.Subcompartment, plot.TreesComposition));
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var plot = button?.Tag as ForestPlotDto;
            if (plot != null)
                App.CurrentFrame.Navigate(new PlotChangePage(plot.PlotId, plot.UserId, plot.Compartment, plot.Subcompartment));
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var plot = button?.Tag as ForestPlotDto;
            if (plot == null) return;
            //контроллер удаления
           
        }   
        private void CreatePlotButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new CreatePlotPage());
        }
    }
}
