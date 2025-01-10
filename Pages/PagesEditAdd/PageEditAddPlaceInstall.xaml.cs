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
    /// Логика взаимодействия для PageEditAddPlaceInstall.xaml
    /// </summary>
    public partial class PageEditAddPlaceInstall : Page
    {
        public PlaceInstall currentPlaceInstall = new PlaceInstall();

        public PageEditAddPlaceInstall(PlaceInstall _selectedPlaceInstall)
        {
            InitializeComponent();
            CmbWorkplaceName.ItemsSource = DatabaseEntities.GetContext().Workplace.ToList();

            if (_selectedPlaceInstall != null)
                currentPlaceInstall = _selectedPlaceInstall;
            DataContext = currentPlaceInstall;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (CmbWorkplaceName.SelectedItem == null)
                errors.AppendLine("Укажите рабочее место!");
            if (TxbPlaceInstallCabinet.Text == null)
                errors.AppendLine("Укажите кабинет!");
            if (TxbPlaceInstallDescription.Text == null)
                errors.AppendLine("Опишите место установки!");
           

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (currentPlaceInstall.PlaceInstallID == 0)
            {
                try
                {
                    DatabaseEntities.GetContext().PlaceInstall.Add(currentPlaceInstall);
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
                Manager.SecondFrame.Navigate(new PagePlaceInstall());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PagePlaceInstall());
        }

    }
}
