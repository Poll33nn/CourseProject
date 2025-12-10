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
            ResonsibleComboBox.SelectedIndex = UserId - 2;
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
            if (string.IsNullOrWhiteSpace(PlotNumberTextBox.Text))
            {
                MessageBox.Show("Укажите номер участка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (ResonsibleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите ответственного сотрудника.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(CompartmentTextBox.Text, out int compartment) || compartment <= 0 || compartment >= 256)
            {
                MessageBox.Show("Квартал должен быть целым числом от 1 до 255.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(SubcompartmentTextBox.Text, out int subcompartment) || subcompartment <= 0 || subcompartment >= 256)
            {
                MessageBox.Show("Выдел должен быть целым числом от 1 до 255.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = await _apiService.UpdateForestPlot(new UpdateForestPlotDto
            {
                PlotId = Convert.ToInt32(PlotNumberTextBox.Text),
                UserId = ResonsibleComboBox.SelectedIndex + 2,
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
