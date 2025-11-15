using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace Forestry.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private HttpClient _httpClient = new();
        private readonly ApiService _apiService;
        public AuthPage()
        {
            InitializeComponent();
            _apiService = new(_httpClient);
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LoginTextBox.Text))
                    MessageBox.Show("Логин не заполнен!");
                if (string.IsNullOrEmpty(PasswordBox.Password))
                    MessageBox.Show("Пароль не заполнен!");

                var result = await _apiService.Login(LoginTextBox.Text.Trim(), PasswordBox.Password.Trim());
                
                if (result == HttpStatusCode.NotFound)
                    MessageBox.Show("Пользователь не найден");
                if (result == HttpStatusCode.OK)
                {
                    MessageBox.Show("Авторизация прошла успешно!"
                        , "Успех"
                        , MessageBoxButton.OK
                        , MessageBoxImage.Information);

                    App.CurrentFrame.Navigate(new MainPage());
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
