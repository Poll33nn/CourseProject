using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using ServiceLayer.Models;

namespace Forestry.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly ApiService _apiService = new();
        private CollectionViewSource _forestCollection = new();
        public MainPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadForestPlots();

            switch (App.UserRole)
            {
                case "Администратор":
                    ReportButton.Visibility = Visibility.Visible;
                    UserButton.Visibility = Visibility.Visible;
                    PopUpSeparator.Visibility = Visibility.Visible;
                    break;

                case "Инженер 2 категории":
                    ReportButton.Visibility = Visibility.Visible;
                    PopUpSeparator.Visibility = Visibility.Visible;
                    break;

                case "Участковый лесничий":
                    CreatePlotButton.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private async Task LoadForestPlots()
        {
            var forestPlots = new ObservableCollection<ForestPlotDto>(await _apiService.GetForestPlotsAsync());

            _forestCollection.Source = forestPlots;
            _forestCollection.Filter += OnFilter;
            ForestPlotsList.ItemsSource = _forestCollection.View;
        }
        private void OnFilter(object sender, FilterEventArgs e)
        {
            string searchText = SearchTextBox.Text?.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                e.Accepted = true;
                return;
            }

            var plot = e.Item as ForestPlotDto;

            bool isMatch = plot.PlotId.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                           plot.Responsible.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase);

            e.Accepted = isMatch;
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _forestCollection.View.Refresh();
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
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
        private async void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var plot = button?.Tag as ForestPlotDto;
            if (plot == null) return;
            {
                var result = await _apiService.DeleteForestPlotAsync(plot.PlotId);
                if (result == System.Net.HttpStatusCode.OK)
                    MessageBox.Show("Участок удален!"
                        , "Успех"
                        , MessageBoxButton.OK
                        , MessageBoxImage.Information);
                LoadForestPlots();
            }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

            App.UserFullName = null;
            App.UserRole = null;
            App.HttpClient.DefaultRequestHeaders.Authorization = null;
            App.CurrentFrame.Navigate(new AuthPage());
        }
        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new UserPage());
        }
        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new ReportPage());
        }
        private void CreatePlotButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new CreatePlotPage());
        }
    }
}
