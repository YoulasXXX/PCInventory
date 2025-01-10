using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Win32;
using PCInventory.Database;
using PCInventory.Utility;

namespace PCInventory.Pages.PagesEditAdd
{
    /// <summary>
    /// Логика взаимодействия для PageEditAddDevDroping.xaml
    /// </summary>
    public partial class PageEditAddDevDroping : Page
    {
        public DeviceDroping currentDeviceDroping = new DeviceDroping();

        public PageEditAddDevDroping(DeviceDroping _selectedDeviceDroping)
        {
            InitializeComponent();
            CmbDeviceName.ItemsSource = DatabaseEntities.GetContext().Device.ToList();
            CmbUserSurname.ItemsSource = DatabaseEntities.GetContext().User.ToList();
            CmbWorkplace.ItemsSource = DatabaseEntities.GetContext().Workplace.ToList();

            if (_selectedDeviceDroping != null)
                currentDeviceDroping = _selectedDeviceDroping;
            DataContext = currentDeviceDroping;
        }

        private void DtpDevDropingDate_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;

            if (datePicker != null && (datePicker.SelectedDate == null || datePicker.SelectedDate == DateTime.MinValue))
            {
                datePicker.SelectedDate = DateTime.Today;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (CmbUserSurname.SelectedItem == null)
                errors.AppendLine("Укажите исполнителя списания!");
            if (CmbWorkplace.SelectedItem == null)
                errors.AppendLine("Выберите рабочее место!");
            if (DtpDevDropingDate.SelectedDate == null)
                errors.AppendLine("Укажите дату списания!");
            if (CmbDeviceName.SelectedItem == null)
                errors.AppendLine("Выберите оборудование!");
            if (string.IsNullOrEmpty(TxbDeviceDropingDefect.Text))
                errors.AppendLine("Опишите причину списания!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (currentDeviceDroping.DeviceDropingID == 0)
            {
                try
                {
                    DatabaseEntities.GetContext().DeviceDroping.Add(currentDeviceDroping);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            try
            {
                DatabaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!", "Информация",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.SecondFrame.Navigate(new PageDevDroping());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageDevDroping());
        }

        private void BtnCreateAct_Click(object sender, RoutedEventArgs e)
        {
            FillWordTemplate();
        }

        private void FillWordTemplate()
        {
            // Имя ресурса вашего файла шаблона
            string templateResourceName = "PCInventory.Resources.ActDeviceDroping.docx";

            // Путь к выходному файлу
            string outputPath = @"C:\Users\Pasha\OneDrive\Рабочий стол\Акт списания оборудования.docx";

            // Получение потока ресурса из сборки
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream templateStream = assembly.GetManifestResourceStream(templateResourceName))
            {
                if (templateStream == null)
                {
                    MessageBox.Show("Ошибка: Не удалось найти ресурс шаблона.");
                    return;
                }

                // Создание временного файла для работы с документом
                string tempFilePath = Path.GetTempFileName();
                File.WriteAllBytes(tempFilePath, ReadFully(templateStream));

                // Открытие документа Word
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(tempFilePath, true))
                {
                    // Переменные для замены
                    var variables = new Dictionary<string, string>
                    {
                        
                        { "WorkplaceName", currentDeviceDroping.Workplace.WorkplaceName},
                        { "DeviceDropingDate", string.Format("{0:yyyy-MM-dd}", DateTime.Now)},
                        { "DeviceName", currentDeviceDroping.Device.DeviceName},
                        { "DeviceModel", currentDeviceDroping.Device.DeviceModel},
                        { "DeviceDatePurchase", string.Format("{0:yyyy-MM-dd}", currentDeviceDroping.Device.DeviceDatePurchase)},
                        { "DeviceInventNum", currentDeviceDroping.Device.DeviceInventNum},
                        { "DeviceDropingDefect", currentDeviceDroping.DeviceDropingDefect},
                        { "UserName", $"{currentDeviceDroping.User.UserSurname} {currentDeviceDroping.User.UserName} {currentDeviceDroping.User.UserPatronymic}"},
                        { "RoleName", currentDeviceDroping.User.Role.RoleName},

                    };

                    // Получение основного документа
                    var doc = wordDoc.MainDocumentPart.Document;

                    // Замена переменных в тексте
                    foreach (var text in doc.Descendants<Text>())
                    {
                        foreach (var variable in variables)
                        {
                            if (text.Text.Contains(variable.Key))
                            {
                                text.Text = text.Text.Replace(variable.Key, variable.Value);
                            }
                        }
                    }

                    // Сохранение изменений в документе
                    wordDoc.Save();
                }

                // Открытие диалогового окна сохранения файла
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Word Document (*.docx)|*.docx",
                    DefaultExt = "docx",
                    Title = "Укажите путь для сохранения файла...",
                    FileName = "Акт списания оборудования.docx",
                    AddExtension = true
                };


                if (saveFileDialog.ShowDialog() == true)
                {
                    // Копирование временного файла в выбранный пользователем файл
                    File.Copy(tempFilePath, saveFileDialog.FileName, true);
                    MessageBox.Show("Измененный документ успешно сохранен по пути: " + saveFileDialog.FileName, 
                        "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // Удаление временного файла
                File.Delete(tempFilePath);
            }
        }

        // Вспомогательный метод для чтения потока в массив байтов
        private byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
