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
    /// Логика взаимодействия для PageEditAddUser.xaml
    /// </summary>
    public partial class PageEditAddUser : Page
    {
        public User currentUser = new User();

        public PageEditAddUser(User _selectedUser)
        {
            InitializeComponent();
            CmbRole.ItemsSource = DatabaseEntities.GetContext().Role.ToList();
            CmbWorkplace.ItemsSource = DatabaseEntities.GetContext().Workplace.ToList();

            if (_selectedUser != null)
                currentUser = _selectedUser;
            DataContext = currentUser;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (TxbUserName.Text == null)
                errors.AppendLine("Укажите имя!");
            if (TxbUserSurname.Text == null)
                errors.AppendLine("Укажите фамилию!");
            if (TxbUserPatronymic.Text == null)
                errors.AppendLine("Укажите отчество!");
            if (CmbRole.Text == null)
                errors.AppendLine("Укажите роль!");
            if (TxbUserPhone.Text == null)
                errors.AppendLine("Укажите номер телефона!");
            if (TxbUserLogin.Text == null)
                errors.AppendLine("Укажите логин!");
            if (TxbUserPassword.Text == null)
                errors.AppendLine("Укажите пароль!");
            if (CmbWorkplace.SelectedItem == null)
                errors.AppendLine("Укажите рабочее место!");
            

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (currentUser.UserID == 0)
            {
                try
                {
                    DatabaseEntities.GetContext().User.Add(currentUser);
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
                Manager.SecondFrame.Navigate(new PageUser());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageUser());
        }
    }
}
