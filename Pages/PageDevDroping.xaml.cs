using PCInventory.Database;
using PCInventory.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PCInventory.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageDevDroping.xaml
    /// </summary>
    public partial class PageDevDroping : Page
    {
        public PageDevDroping()
        {
            InitializeComponent();
            DataGridDevDroping.ItemsSource = DatabaseEntities.GetContext().DeviceDroping.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.SecondFrame.Navigate(new Pages.PagesEditAdd.PageEditAddDevDroping(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var ObjectForEdit = DataGridDevDroping.SelectedItem;
            Manager.SecondFrame.Navigate(new Pages.PagesEditAdd.PageEditAddDevDroping((DeviceDroping)ObjectForEdit));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var ObjectForDelete = DataGridDevDroping.SelectedItems.Cast<DeviceDroping>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить {ObjectForDelete.Count()} элемент", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DatabaseEntities.GetContext().DeviceDroping.RemoveRange(ObjectForDelete);
                    DatabaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    DataGridDevDroping.ItemsSource = DatabaseEntities.GetContext().DeviceDroping.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = TxbSearch.Text.ToLower();
            ICollectionView cv = CollectionViewSource.GetDefaultView(DataGridDevDroping.ItemsSource);

            if (string.IsNullOrEmpty(searchText))
            {
                cv.Filter = null;
            }
            else
            {
                cv.Filter = o =>
                {
                    DeviceDroping item = o as DeviceDroping;
                    return item.Device.DeviceName.ToLower().Contains(searchText);
                };
            }
        }
    }
}
