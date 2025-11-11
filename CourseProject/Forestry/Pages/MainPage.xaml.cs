using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Forestry.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public class ForestPlot
        {
            public int Id { get; set; }
            public string Number { get; set; }
            public string Responsible { get; set; }
            public string Address { get; set; }

            public ForestPlot(int id, string number, string responsible, string address)
            {
                Id = id;
                Number = number;
                Responsible = responsible;
                Address = address;
            }
        }

        private List<ForestPlot> _forestPlots;

        public MainPage()
        {
            InitializeComponent();
            LoadForestPlots();
        }

        private void LoadForestPlots()
        {
            // Здесь — загрузка из БД
            _forestPlots = new List<ForestPlot>
            {
                new ForestPlot(1, "Лесной участок №1", "Иванов И.И.", "ул. Лесная, д. 5"),
                new ForestPlot(2, "Лесной участок №2", "Петров П.П.", "ул. Сосновая, д. 12"),
                new ForestPlot(3, "Лесной участок №3", "Сидоров С.С.", "пер. Берёзовый, д. 8"),
                new ForestPlot(3, "Лесной участок №3", "Сидоров С.С.", "пер. Берёзовый, д. 8"),
                new ForestPlot(3, "Лесной участок №3", "Сидоров С.С.", "пер. Берёзовый, д. 8"),
                new ForestPlot(3, "Лесной участок №3", "Сидоров С.С.", "пер. Берёзовый, д. 8"),
                new ForestPlot(3, "Лесной участок №3", "Сидоров С.С.", "пер. Берёзовый, д. 8"),
                new ForestPlot(3, "Лесной участок №3", "Сидоров С.С.", "пер. Берёзовый, д. 8")
            };

            // Привязка к ItemsControl
            ForestPlotsList.ItemsSource = _forestPlots;
        }

        // Обработчики кнопок
        private void OnInfoClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var plot = button?.Tag as ForestPlot;
            if (plot != null)
                App.CurrentFrame.Navigate(new PlotInformationPage(plot.Number, plot.Responsible, plot.Address));
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var plot = button?.Tag as ForestPlot;
            if (plot != null)
                App.CurrentFrame.Navigate(new PlotChangePage());
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var plot = button?.Tag as ForestPlot;
            if (plot == null) return;

            if (MessageBox.Show($"Удалить {plot.Number}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _forestPlots.Remove(plot);
                // Обновляем отображение
                ForestPlotsList.ItemsSource = null;
                ForestPlotsList.ItemsSource = _forestPlots;
            }
        }   
        private void CreatePlotButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new CreatePlotPage());
        }
    }
}
