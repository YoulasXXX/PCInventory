using PCInventory.Database;
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
    /// Логика взаимодействия для PagePlaceInstall.xaml
    /// </summary>
    public partial class PagePlaceInstall : Page
    {
        public PagePlaceInstall()
        {
            InitializeComponent();
            DataGridPlaceInstall.ItemsSource = DatabaseEntities.GetContext().PlaceInstall.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.SecondFrame.Navigate(new PageEditAddPlaceInstall(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var ObjectForEdit = DataGridPlaceInstall.SelectedItem;
            Manager.SecondFrame.Navigate(new PageEditAddPlaceInstall((PlaceInstall)ObjectForEdit));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var ObjectForDelete = DataGridPlaceInstall.SelectedItems.Cast<PlaceInstall>().ToList();

            if (MessageBox.Show($"Вы действительно хотите удалить {ObjectForDelete.Count()} элемент", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DatabaseEntities.GetContext().PlaceInstall.RemoveRange(ObjectForDelete);
                    DatabaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    DataGridPlaceInstall.ItemsSource = DatabaseEntities.GetContext().PlaceInstall.ToList();
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
            ICollectionView cv = CollectionViewSource.GetDefaultView(DataGridPlaceInstall.ItemsSource);

            if (string.IsNullOrEmpty(searchText))
            {
                cv.Filter = null;
            }
            else
            {
                cv.Filter = o =>
                {
                    PlaceInstall item = o as PlaceInstall;
                    return item.Workplace.WorkplaceName.ToLower().Contains(searchText);
                };
            }
        }
    }
}
