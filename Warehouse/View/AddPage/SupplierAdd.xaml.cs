using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Service;

namespace Warehouse.View.AddPage
{
    public partial class SupplierAdd : Window
    {
        DataGrid grid;
        Database database = new Database();

        public SupplierAdd(DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;
            database.ReadCarNumberToComboBox(CarNumberComboBox);

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO dto = (ComboBoxDTO)CarNumberComboBox.SelectedItem;
            string address = SupplierAdressBox.Text;
            string phone = SupplierPhoneBox.Text;
            string surname = SurnameBox.Text;
            string firstName = FirstNameBox.Text;
            string middleName = MiddleNameBox.Text;
            string medCertificate = MedicalCertificateComboBox.Text;

            ValidationFileds validation = new ValidationFileds();

/*            if (validation.ValidationSupplierAdd(){*/
                Database database = new Database();
                database.CreateSupplier(address, phone, surname, firstName, middleName, medCertificate, dto);
                database.ReadSupplier(grid);
/*            }*/

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
