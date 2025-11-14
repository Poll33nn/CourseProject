using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для PlotInformationPage.xaml
    /// </summary>
    public partial class PlotInformationPage : Page
    {
        public class Tree
        {
            public string Name { get; set; }
            public int Amount { get; set; }
        }
        public PlotInformationPage(string Name, string Otv, string Address)
        {
            InitializeComponent();
            NumberLabel.Content = "Лесной участок № " + Name;
            OtvLabel.Content = "Ответственный: " + Otv;
            AddressLabel.Content = "Квартал: " + Address;

            var trees = new ObservableCollection<Tree>
            {
                new Tree { Name = "Ольха", Amount = 150 },
                new Tree { Name = "Ольха", Amount = 150 }
            };
            TreesDataGrid.ItemsSource = trees;
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.GoBack();
        }

        private void CreateEventButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.Navigate(new CreateEventPage(NumberLabel.Content));
        }
    }
}
