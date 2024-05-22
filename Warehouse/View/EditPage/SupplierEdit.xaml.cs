using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Service;
using Warehouse.Storage;
using Warehouse.View.AddPage;

namespace Warehouse.View.EditPage
{
    public partial class SupplierEdit : Window
    {
        long id;
        DataGrid grid;
        DriverStorage driverStorage = new DriverStorage();

        public SupplierEdit(long id, string carNumber, string address, string phoneNumber, string surname, string firstName, string middleName, string medicalCertificate, DataGrid grid)
        {
            InitializeComponent();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;

            this.id = id;
            this.grid = grid;
            CarNumberComboBox.Items.Add(carNumber);
            CarNumberComboBox.SelectedIndex = 0;
            SupplierAdressBox.Text = address;
            SupplierPhoneBox.Text = phoneNumber;
            SurnameBox.Text = surname;
            FirstNameBox.Text = firstName;
            MiddleNameBox.Text = middleName;
            MedicalCertificateComboBox.Text = medicalCertificate;
            MedicalCertificateComboBox.SelectedIndex = 0;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO)CarNumberComboBox.SelectedItem;
            string address = SupplierAdressBox.Text;
            string phone = SupplierPhoneBox.Text;
            string surname = SurnameBox.Text;
            string firstName = FirstNameBox.Text;
            string middleName = MiddleNameBox.Text;
            string medicalCertificate = MedicalCertificateComboBox.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationSupplierEdit(address, phone, surname, firstName, middleName, medicalCertificate))
            {
                driverStorage.UpdateDriver(id, address, phone, surname, firstName, middleName, medicalCertificate);
                driverStorage.ReadDriver(grid);

                this.Close();
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            CarNumberComboBox.Visibility = Visibility.Collapsed;
            SupplierAdressBox.Visibility = Visibility.Collapsed;
            SupplierPhoneBox.Visibility = Visibility.Collapsed;
            MedicalCertificateComboBox.Visibility = Visibility.Collapsed;
            Next.Visibility = Visibility.Collapsed;
            Return.Visibility = Visibility.Collapsed;

            SurnameBox.Visibility = Visibility.Visible;
            FirstNameBox.Visibility = Visibility.Visible;
            MiddleNameBox.Visibility = Visibility.Visible;
            Preview.Visibility = Visibility.Visible;
            Confirm.Visibility = Visibility.Visible;
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {
            SurnameBox.Visibility = Visibility.Collapsed;
            FirstNameBox.Visibility = Visibility.Collapsed;
            MiddleNameBox.Visibility = Visibility.Collapsed;
            Preview.Visibility = Visibility.Collapsed;
            Confirm.Visibility = Visibility.Collapsed;

            CarNumberComboBox.Visibility = Visibility.Visible;
            SupplierAdressBox.Visibility = Visibility.Visible;
            SupplierPhoneBox.Visibility = Visibility.Visible;
            MedicalCertificateComboBox.Visibility = Visibility.Visible;
            Next.Visibility = Visibility.Visible;
            Return.Visibility = Visibility.Visible;
        }
    }
}
