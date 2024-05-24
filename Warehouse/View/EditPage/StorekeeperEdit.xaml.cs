using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.EditPage
{
    public partial class StorekeeperEdit : Window
    {
        long id;
        DataGrid grid;
        StorekeeperStorage storekeeperStorage = new StorekeeperStorage();
        public StorekeeperEdit(long id, string surname, string firstName, string middleName, string phoneNumber, string address,  DataGrid grid)
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
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string address = StorekeeperAdressBox.Text;
            string phone = StorekeeperPhoneBox.Text;
            string surname = SurnameBox.Text;
            string firstName = FirstNameBox.Text;
            string middleName = MiddleNameBox.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationStorekeeper(surname, firstName, middleName, address, phone))
            {
                storekeeperStorage.UpdateStorekeeper(id, surname, firstName, middleName, phone , address);
                storekeeperStorage.ReadStorekeeper(grid);

                this.Close();
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
