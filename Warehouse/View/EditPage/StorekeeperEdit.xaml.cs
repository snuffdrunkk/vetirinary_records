using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
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
using System.Windows.Shapes;
using Warehouse.DTO;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.EditPage
{
    public partial class StorekeeperEdit : Window
    {
        long id;
        DataGrid grid;
        StorekeeperStorage storekeeperStorage = new StorekeeperStorage();
        public StorekeeperEdit(long id, string address, string phoneNumber, string surname, string firstName, string middleName, string medicalCertificate, DataGrid grid)
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
            StorekeeperAdressBox.Text = address;
            StorekeeperPhoneBox.Text = phoneNumber;
            SurnameBox.Text = surname;
            FirstNameBox.Text = firstName;
            MiddleNameBox.Text = middleName;
            MedicalCertificateComboBox.Text = medicalCertificate;
            MedicalCertificateComboBox.SelectedIndex = 0;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string address = StorekeeperAdressBox.Text;
            string phone = StorekeeperPhoneBox.Text;
            string surname = SurnameBox.Text;
            string firstName = FirstNameBox.Text;
            string middleName = MiddleNameBox.Text;
            string medicalCertificate = MedicalCertificateComboBox.Text;

            ValidationFileds validation = new ValidationFileds();

            /*            if (validation.ValidationSupplierAdd(title, address, phone, surname, firstName, middleName))
                        {*/
            storekeeperStorage.UpdateStorekeeper(id, surname, firstName, middleName, phone , address,  medicalCertificate);
            storekeeperStorage.ReadStorekeeper(grid);

            this.Close();
            /*            }*/
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

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
