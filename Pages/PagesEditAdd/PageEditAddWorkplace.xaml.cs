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
    /// Логика взаимодействия для PageEditAddWorkplace.xaml
    /// </summary>
    public partial class PageEditAddWorkplace : Page
    {
        public Workplace currentWorkplace = new Workplace();

        public PageEditAddWorkplace(Workplace _selectedWorkplace)
        {
            InitializeComponent();

            if (_selectedWorkplace != null)
                currentWorkplace = _selectedWorkplace;
            DataContext = currentWorkplace;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrEmpty(TxbWorkplaceName.Text))
            {
                errors.AppendLine("Укажите наименование рабочего места!");
            }


            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (currentWorkplace.WorkplaceID == 0)
            {
                try
                {
                    DatabaseEntities.GetContext().Workplace.Add(currentWorkplace);
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

                Manager.SecondFrame.Navigate(new PageWorkplace());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageTypeDevices());
        }
    }
}
