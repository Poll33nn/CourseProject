using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для PlotChangePage.xaml
    /// </summary>
    public partial class PlotChangePage : Page
    {
        private readonly ApiService _apiService = new();
        public PlotChangePage(int PlotId, int UserId, int Compartment, int Subcompartment)
        {
            InitializeComponent();

            PlotNumberTextBox.Text = PlotId.ToString();
            ResonsibleComboBox.SelectedIndex = UserId;
            CompartmentTextBox.Text = Compartment.ToString();
            SubcompartmentTextBox.Text = Subcompartment.ToString();

            LoadResponible();
        }
        public async Task LoadResponible()
        {
            ResonsibleComboBox.ItemsSource = await _apiService.GetAllResponsibleAsync();
        }

        private async void ChangePlotButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await _apiService.UpdateForestPlot(new UpdateForestPlotDto
            {
                PlotId = Convert.ToInt32(PlotNumberTextBox.Text),
                UserId = ResonsibleComboBox.SelectedIndex + 1,
                Compartment = Convert.ToByte(CompartmentTextBox.Text),
                Subcompartment = Convert.ToByte(SubcompartmentTextBox.Text),
            });

            if (result == HttpStatusCode.OK)
            {
                MessageBox.Show("Успешно изменено!"
                    , "Успех"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Information);

                App.CurrentFrame.Navigate(new MainPage());
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.GoBack();
        }
    }
}
