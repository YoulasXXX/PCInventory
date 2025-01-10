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

namespace PCInventory.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAuth.xaml
    /// </summary>
    public partial class PageAuth : Page
    {
        public PageAuth()
        {
            InitializeComponent();
        }

        private void PsBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PsBoxPassword.Password.Length == 0)
            {
                TxBlPassword.Visibility = Visibility.Visible;
            }
            else
            {
                TxBlPassword.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnSingIn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(TxBoxLogin.Text))
                errors.AppendLine("Поле \"Логин\" должно быть заполнено!");
            if (string.IsNullOrEmpty(PsBoxPassword.Password))
                errors.AppendLine("Поле \"Пароль\" должно быть заполнено!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                PsBoxPassword.Password = string.Empty;
            }
            else
            {
                User _currentUser = DatabaseEntities.GetContext().User.FirstOrDefault(x => x.UserLogin == TxBoxLogin.Text || x.UserPassword == PsBoxPassword.Password);

                if (_currentUser != null)
                {
                    Manager.AuthUser = _currentUser;
                    MessageBox.Show($"Здраствуйте, {Manager.AuthUser.FullName}!", "Информация", 
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    Manager.FrameMain.Navigate(new MainPage());
                }
                else
                {
                    MessageBox.Show("Пользователь с такими данными не найден!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
