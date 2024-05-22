using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class StorekeeperAdd : Window
    {
        DataGrid grid;
        Database database = new Database();
        StorekeeperStorage storekeeperStorage = new StorekeeperStorage();
        public StorekeeperAdd(DataGrid grid)
        {
            InitializeComponent();

            this.grid = grid;

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            StorekeeperAdressBox.Visibility = Visibility.Collapsed;
            StorekeeperPhoneBox.Visibility = Visibility.Collapsed;
            MedicalCertificateComboBox.Visibility = Visibility.Collapsed;
            Next.Visibility = Visibility.Collapsed;
            Return.Visibility = Visibility.Collapsed;

            SurnameBox.Visibility = Visibility.Visible;
            FirstNameBox.Visibility = Visibility.Visible;
            MiddleNameBox.Visibility = Visibility.Visible;
            Preview.Visibility = Visibility.Visible;
            Confirm.Visibility = Visibility.Visible;
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            SurnameBox.Visibility = Visibility.Collapsed;
            FirstNameBox.Visibility = Visibility.Collapsed;
            MiddleNameBox.Visibility = Visibility.Collapsed;
            Preview.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;

            StorekeeperAdressBox.Visibility = Visibility.Visible;
            StorekeeperPhoneBox.Visibility = Visibility.Visible;
            MedicalCertificateComboBox.Visibility = Visibility.Visible;
            Next.Visibility = Visibility.Visible;
            Return.Visibility = Visibility.Visible;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string address = StorekeeperAdressBox.Text;
            string phone = StorekeeperPhoneBox.Text;
            string surname = SurnameBox.Text;
            string firstName = FirstNameBox.Text;
            string middleName = MiddleNameBox.Text;
            string medCertificate = MedicalCertificateComboBox.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationStorekeeper(surname, firstName, middleName, phone, address, medCertificate))
            {
                storekeeperStorage.CreateStorekeeper(surname, firstName, middleName, phone, address, medCertificate);
                storekeeperStorage.ReadStorekeeper(grid);
                this.Close();
            }
        }
    }
}
