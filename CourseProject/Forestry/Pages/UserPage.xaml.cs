using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private readonly ApiService _apiService = new();
        public UserPage()
        {
            InitializeComponent();
        }
        private async void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            var fioParts = FullNameTextBox.Text.Split(' ');
            var result = await _apiService.CreateUserAsync(new CreateUserDto
            {
                RoleId = RoleComboBox.SelectedIndex + 1,
                Name = fioParts[1],
                LastName = fioParts[0],
                Patronymic = fioParts[2],
                Login = LoginTextBox.Text.Trim(),
                PasswordHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(PasswordTextBox.Text.Trim()))),
            });
            if (result == HttpStatusCode.BadRequest)
            {
                MessageBox.Show("Ошибка при создании пользователя!"
                    , "Ошибка"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Warning);
            }

            if (result == HttpStatusCode.Created)
            {
                MessageBox.Show("Пользователь успешно создан!"
                    , "Успех"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Information);

                App.CurrentFrame.Navigate(new MainPage());
            }
        }
        private async void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await _apiService.DeleteUserAsync(Convert.ToInt32(UserIdTextBox.Text.Trim()));
            if (result == HttpStatusCode.OK)
                MessageBox.Show("Пользователь удален!"
                    , "Успех"
                    , MessageBoxButton.OK
                    , MessageBoxImage.Information);

            int inputText = Convert.ToInt32(UserIdTextBox.Text.Trim());
            Debug.WriteLine($"[Client] Input text: '{inputText}'");

            //App.CurrentFrame.Navigate(new MainPage());
        }
        private async void CreateStackPanelButton_Click(object sender, RoutedEventArgs e)
        {
            CreateUserStackPanel.Visibility = Visibility.Visible;
            CreateStackPanelButton.Visibility = Visibility.Collapsed;
            DeleteStackPanelButton.Visibility = Visibility.Collapsed;
            RoleComboBox.ItemsSource = await _apiService.GetRoleNameAsync();
        }
        private void DeleteStackPanelButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteUserStackPanel.Visibility = Visibility.Visible;
            DeleteStackPanelButton.Visibility = Visibility.Collapsed;
            CreateStackPanelButton.Visibility = Visibility.Collapsed;
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.GoBack();
        }
    }
}
