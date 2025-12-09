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
            TreeTypeComboBox.ItemsSource = await _apiService.GetTreeTypeNameAsync();
        }
        private void AddTreeButton_Click(object sender, RoutedEventArgs e)
        {
            TreesNumberListBox.Items.Add($"{TreeTypeComboBox.SelectedValue} - {TreesNumberTextBox.Text}");
            _treesComposition.Add(new CreateTreesNumberDto { TreeTypeId = TreeTypeComboBox.SelectedIndex + 1, Amount = Convert.ToInt32(TreesNumberTextBox.Text) });
        }
        private async void CreatePlotButton_Click(object sender, RoutedEventArgs e)
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
            if (!int.TryParse(SubcompartmentTextBox.Text, out int subcompartment) || subcompartment <= 0 || compartment >= 256)
            {
                MessageBox.Show("Выдел должен быть целым числом от 1 до 255.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_treesComposition.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одну породу дерева.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = await _apiService.CreateForestPlotAsync(new CreateForestPlotDto
            {
                PlotId = Convert.ToInt32(PlotNumberTextBox.Text),
                UserId = ResonsibleComboBox.SelectedIndex + 2,
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

                App.CurrentFrame.Navigate(new MainPage());
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.GoBack();
        }
    }
}
