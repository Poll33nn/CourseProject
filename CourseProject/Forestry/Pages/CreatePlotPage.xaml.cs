using ServiceLayer.DTO_s;
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

namespace Forestry.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreatePlotPage.xaml
    /// </summary>
    public partial class CreatePlotPage : Page
    {
        private readonly ApiService _apiService = new();
        private List<CreateTreesNumberDto> _treesComposition = new();
        public CreatePlotPage()
        {
            InitializeComponent();
            LoadResponible();
            LoadTreeType();
        }

        public async Task LoadResponible()
        { 
            ResonsibleComboBox.ItemsSource = await _apiService.GetAllResponsibleAsync();
        }
        public async Task LoadTreeType()
        {
            TreeTypeComboBox.ItemsSource = await _apiService.GetAllResponsibleAsync();
        }
        private void AddTreeButton_Click(object sender, RoutedEventArgs e)
        {
            TreesNumberListBox.Items.Add($"{ResonsibleComboBox.SelectedValue} - {TreesNumberTextBox.Text}");
            _treesComposition.Add(new CreateTreesNumberDto { TreeTypeId = ResonsibleComboBox.SelectedIndex + 1, Amount = Convert.ToInt32(TreesNumberTextBox.Text) });
        }
        private async void CreatePlotButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await _apiService.CreateForestPlotAsync(new CreateForestPlotDto
            {
                PlotId = Convert.ToInt32(PlotNumberTextBox.Text),
                UserId = ResonsibleComboBox.SelectedIndex + 1,
                Compartment = Convert.ToInt32(CompartmentTextBox.Text),
                Subcompartment = Convert.ToInt32(SubcompartmentTextBox.Text),
                TreeComposition = _treesComposition,
            });

            if (result == HttpStatusCode.BadRequest)
            {
                MessageBox.Show("Ошибка при создании участка!"
                    ,"Ошибка"
                    ,MessageBoxButton.OK
                    ,MessageBoxImage.Warning);
            }

            if (result == HttpStatusCode.Created)
            {
                MessageBox.Show("Участок успешно создан!"
                    , "Успех"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Information);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.GoBack();
        }
    }
}
