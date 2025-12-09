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
    /// Логика взаимодействия для CreateEventPage.xaml
    /// </summary>
    public partial class CreateEventPage : Page
    {
        private readonly ApiService _apiService = new();
        public int currentPlotId;
        public CreateEventPage(int PlotId)
        {
            InitializeComponent();
            LoadEventType();
            LoadTreeType();
            currentPlotId = PlotId;
            PlotIdLabel.Content = "Лесной участок № " + PlotId;

            if (App.UserRole == "Мастер участка")
            {
                EventTypeComboBox.SelectedIndex = 0;
                EventTypeComboBox.IsEnabled = false;
            }
        }
        public async Task LoadEventType()
        {
            EventTypeComboBox.ItemsSource = await _apiService.GetEventTypeNameAsync();
        }
        public async Task LoadTreeType()
        {
            TreeTypeComboBox.ItemsSource = await _apiService.GetTreeTypeNameAsync();
        }
        private void EventTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventTypeComboBox.SelectedIndex != 0)
            {
                PanelLable.Content = "Высажено:";
                PanelLable.Margin = new Thickness(15, 0, 7, 5);
                PlantingPanel.Visibility = Visibility.Visible;
            }
            else
            {
                PanelLable.Content = "Вырублена:";
                PanelLable.Margin = new Thickness(15, 0, 1, 5);
                PlantingPanel.Visibility = Visibility.Visible;
            }
        }
        private async void CreateEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (EventTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите тип мероприятия."
                    , "Ошибка ввода"
                        , MessageBoxButton.OK
                        , MessageBoxImage.Warning);
                return;
            }

            bool? plantingOrFelling = null;
            var selectedEvent = EventTypeComboBox.SelectedItem as EventTypeDto;
            if (selectedEvent != null)
            {
                plantingOrFelling = selectedEvent.Name == "Вырубка леса" || selectedEvent.Name == "Лесовосстановление" || selectedEvent.Name == "Воспроизводство лесов";
            }

            if (PlantingPanel.Visibility == Visibility.Visible)
            {
                if (TreeTypeComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите породу дерева."
                        , "Ошибка ввода"
                        , MessageBoxButton.OK
                        , MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(TreesNumberTextBox.Text))
                {
                    MessageBox.Show("Поле 'Количество' не может быть пустым."
                        , "Ошибка ввода"
                        , MessageBoxButton.OK
                        , MessageBoxImage.Warning);
                    return;
                }
                if (!int.TryParse(TreesNumberTextBox.Text, out int count) || count <= 0)
                {
                    MessageBox.Show("Количество должно быть целым положительным числом."
                        , "Ошибка ввода"
                        , MessageBoxButton.OK
                        , MessageBoxImage.Warning);
                    return;
                }
            }

            var result = await _apiService.CreateSilvicultureEventAsync(new CreateSilvicultureEventDto
            {
                PlotId = currentPlotId,
                EventTypeId = EventTypeComboBox.SelectedIndex + 1,
                TreeTypeId = TreeTypeComboBox.SelectedIndex + 1,
                Description = DescriptionTextBox.Text,
                Date = DateTime.Now.Date,
                TreesNumber = Convert.ToInt32(TreesNumberTextBox.Text),
            });

            if (result == HttpStatusCode.BadRequest)
            {
                MessageBox.Show("Ошибка при создании мероприятия!"
                    , "Ошибка"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Warning);
            }

            if (result == HttpStatusCode.Created)
            {
                MessageBox.Show("Мероприятие успешно создано!"
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
