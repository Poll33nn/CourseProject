using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace Forestry
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string UserFullName {  get;  set; }
        public static string UserRole {  get;  set; }
        public static Frame CurrentFrame { get; set; }
        public static HttpClient HttpClient = new()
        {
            BaseAddress = new Uri("http://localhost:5163/api/")
        };
    }

}
