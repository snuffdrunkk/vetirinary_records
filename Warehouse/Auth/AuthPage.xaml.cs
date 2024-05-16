using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Warehouse.Service;
using Warehouse.Storage;
using Warehouse.View.Main;

namespace Warehouse.Auth
{
    public partial class AuthPage : Window
    {
        ValidationFileds validationFileds = new ValidationFileds();
        Authentif auth = new Authentif();

        public AuthPage()
        {
            InitializeComponent();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;
        }

        private void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = Password.Password;

            Database database = new Database();
            bool isAuth = auth.Check(username, password);

            if (isAuth)
            {
                AuthManager.CurrentUsername = username;
                MainPage mainPage = new MainPage();
                this.Hide();
                mainPage.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void Registr_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow main = new MainWindow();
            main.Show();
        }
    }
}
