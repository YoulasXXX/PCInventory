﻿using PCInventory.Database;
using PCInventory.Pages.PagesEditAdd;
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
    /// Логика взаимодействия для PageTypeDevices.xaml
    /// </summary>
    public partial class PageTypeDevices : Page
    {
        public PageTypeDevices()
        {
            InitializeComponent();
            DataGridDeviceType.ItemsSource = DatabaseEntities.GetContext().DeviceType.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.SecondFrame.Navigate(new Pages.PagesEditAdd.PageEditAddDeviceType(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var ObjectForEdit = DataGridDeviceType.SelectedItem;
            Manager.SecondFrame.Navigate(new Pages.PagesEditAdd.PageEditAddDeviceType((DeviceType)ObjectForEdit));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var ObjectForDelete = DataGridDeviceType.SelectedItems.Cast<DeviceType>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить {ObjectForDelete.Count()} элемент", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DatabaseEntities.GetContext().DeviceType.RemoveRange(ObjectForDelete);
                    DatabaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены", "Информация", 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    DataGridDeviceType.ItemsSource = DatabaseEntities.GetContext().DeviceType.ToList();
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
            ICollectionView cv = CollectionViewSource.GetDefaultView(DataGridDeviceType.ItemsSource);

            if (string.IsNullOrEmpty(searchText))
            {
                cv.Filter = null;
            }
            else
            {
                cv.Filter = o =>
                {
                    DeviceType item = o as DeviceType;
                    return item.DeviceTypeName.ToLower().Contains(searchText);
                };
            }
        }
    }
}