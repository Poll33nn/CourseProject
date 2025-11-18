using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для CreateEventPage.xaml
    /// </summary>
    public partial class CreateEventPage : Page
    {
        public CreateEventPage(object label)
        {
            InitializeComponent();
            //контроллер получения списка ответственных
            //контроллер добавления мероприятия
            PlotIdLabel.Content = label;
            EventTypeComboBox.Items.Add("1");
            EventTypeComboBox.Items.Add("2");

        }

        private void EventTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventTypeComboBox.SelectedValue == "1")
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.GoBack(); 
        }
    }
}
