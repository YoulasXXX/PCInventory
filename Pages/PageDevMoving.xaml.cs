using DocumentFormat.OpenXml.Packaging;
using PCInventory.Database;
using PCInventory.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace PCInventory.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageDevMoving.xaml
    /// </summary>
    public partial class PageDevMoving : Page
    {
        public PageDevMoving()
        {
            InitializeComponent();
            DataGridDevMoving.ItemsSource = DatabaseEntities.GetContext().DeviceMoving.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.SecondFrame.Navigate(new Pages.PagesEditAdd.PageEditAddDevMoving(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var ObjectForEdit = DataGridDevMoving.SelectedItem;
            Manager.SecondFrame.Navigate(new Pages.PagesEditAdd.PageEditAddDevMoving((DeviceMoving)ObjectForEdit));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var ObjectForDelete = DataGridDevMoving.SelectedItems.Cast<DeviceMoving>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить {ObjectForDelete.Count()} элемент", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DatabaseEntities.GetContext().DeviceMoving.RemoveRange(ObjectForDelete);
                    DatabaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    DataGridDevMoving.ItemsSource = DatabaseEntities.GetContext().DeviceMoving.ToList();
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
            ICollectionView cv = CollectionViewSource.GetDefaultView(DataGridDevMoving.ItemsSource);

            if (string.IsNullOrEmpty(searchText))
            {
                cv.Filter = null;
            }
            else
            {
                cv.Filter = o =>
                {
                    DeviceMoving item = o as DeviceMoving;
                    return item.Device.DeviceName.ToLower().Contains(searchText);
                };
            }
        }

        
    }
}
