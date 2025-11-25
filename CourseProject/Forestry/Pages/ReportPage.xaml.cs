using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
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
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using ServiceLayer.DTO_s;

namespace Forestry.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private List<ForestEventReportDto> _eventList = new();
        private int? selectedPlotId;
        private readonly ApiService _apiServic = new();
        public ReportPage()
        {
            InitializeComponent();
        }
        private async void ReportAllForestryEvents_Checked(object sender, RoutedEventArgs e)
        {
            _eventList = await _apiServic.GetAllSilvicultureEventAsync();
        }
        private async void ReportForestPlotEvents_Checked(object sender, RoutedEventArgs e)
        {
            PlotIdStackPanel.Visibility = Visibility.Visible;
            ForestPlotIdComboBox.ItemsSource = await _apiServic.GetForestPlotsAsync();
        }
        private async void ForestPlotIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPlotId = (ForestPlotIdComboBox.SelectedItem as ForestPlotDto).PlotId;
            _eventList = await _apiServic.GetPlotSilvicultureEventAsync((int)selectedPlotId);
        }
        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (_eventList == null || _eventList.Count == 0)
            {
                string msg = selectedPlotId.HasValue
                   ? "Нет мероприятий для выбранного участка."
                   : "Нет данных для экспорта.";
                MessageBox.Show(msg, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Мероприятия");

            // Заголовки
            ws.Cell(1, 1).Value = "Номер мероприятия";
            ws.Cell(1, 2).Value = "Тип мероприятия";
            ws.Cell(1, 3).Value = "Участок проведения";
            ws.Cell(1, 4).Value = "Описание";
            ws.Cell(1, 5).Value = "Дата";
            ws.Cell(1, 6).Value = "Тип древесины";
            ws.Cell(1, 7).Value = "Количество древесины";

            var headerRange = ws.Range(1, 1, 1, 7);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Данные
            for (int i = 0; i < _eventList.Count; i++)
            {
                var row = i + 2;
                var ev = _eventList[i];

                ws.Cell(row, 1).Value = ev.EventId;
                ws.Cell(row, 2).Value = ev.EventType;
                ws.Cell(row, 3).Value = $"Лесной участок №{ev.PlotId}";
                ws.Cell(row, 4).Value = ev.Description;
                ws.Cell(row, 5).Value = ev.Date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
                ws.Cell(row, 6).Value = ev.TreeType ?? "";
                ws.Cell(row, 7).Value = ev.TreesNumber?.ToString() ?? "";
            }
            int lastRow = _eventList.Count + 1; // +1 потому что первая строка — заголовок
            int lastColumn = 7; // количество столбцов

            var dataRange = ws.Range(1, 1, lastRow, lastColumn);

            // Центрируем текст по горизонтали и вертикали
            dataRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            // (опционально) добавить границы для наглядности
            dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            ws.Columns().AdjustToContents();

            // --- Сохранение ---
            var dialog = new SaveFileDialog
            {
                FileName = selectedPlotId.HasValue
                    ? $"Отчёт_участок_{selectedPlotId.Value}.xlsx"
                    : "Отчёт_все_мероприятия.xlsx",
                DefaultExt = ".xlsx",
                Filter = "Файлы Excel (*.xlsx)|*.xlsx"
            };

            if (dialog.ShowDialog() != true) return;

            using var fs = new FileStream(dialog.FileName, FileMode.Create);
            workbook.SaveAs(fs);

            MessageBox.Show($"Отчёт сохранён:\n{dialog.FileName}", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentFrame.GoBack();
        }
    }
}
