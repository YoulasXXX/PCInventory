using PCInventory.Database;
using PCInventory.Utility;
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

namespace PCInventory.Pages.PagesEditAdd
{
    /// <summary>
    /// Логика взаимодействия для PageEditAddDevice.xaml
    /// </summary>
    public partial class PageEditAddDevice : Page
    {
        public Device currentDevice = new Device();

        public PageEditAddDevice(Device _selectedDevice)
        {
            InitializeComponent();
            CmbDeviceStatus.ItemsSource = DatabaseEntities.GetContext().DeviceStatus.ToList();
            CmbDeviceType.ItemsSource = DatabaseEntities.GetContext().DeviceType.ToList();

            if (_selectedDevice != null)
                currentDevice = _selectedDevice;
            DataContext = currentDevice;
        }

        private void DtpDeviceDatePurchase_Loaded(object sender, RoutedEventArgs e)
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

            if (string.IsNullOrEmpty(TxbDeviceName.Text))
                errors.AppendLine("Укажите наименование оборудования!");
            if (CmbDeviceType.SelectedItem == null)
                errors.AppendLine("Выберите тип оборудования!");
            if (string.IsNullOrEmpty(TxbDeviceModel.Text))
                errors.AppendLine("Укажите модель оборудования!");
            if (string.IsNullOrEmpty(TxbDeviceInventNum.Text))
                errors.AppendLine("Укажите инвентарный номер оборудования!");
            if (string.IsNullOrEmpty(TxbDeviceSerialNum.Text))
                errors.AppendLine("Укажите серийный номер оборудования!");
            if (CmbDeviceStatus.SelectedItem == null)
                errors.AppendLine("Укажите статус оборудования!");
            if (DtpDeviceDatePurchase.SelectedDate == null)
                errors.AppendLine("Укажите дату приобретения оборудования!");
            if (string.IsNullOrEmpty(TxbDevicePrice.Text))
            {
                errors.AppendLine("Укажите цену приобретения!");
            }
            else
            {
                // Пытаемся преобразовать введенный текст в число
                if (!decimal.TryParse(TxbDevicePrice.Text, out decimal totalPrice))
                {
                    errors.AppendLine("Укажите корректную цену!");
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (currentDevice.DeviceID == 0)
            {
                try
                {
                    DatabaseEntities.GetContext().Device.Add(currentDevice);
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
                Manager.SecondFrame.Navigate(new PageDevice());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageDevice());
            
        }

    }
}
