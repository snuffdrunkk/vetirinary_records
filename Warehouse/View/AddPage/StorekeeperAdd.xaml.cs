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
                storekeeperStorage.CreateStorekeeper(surname, firstName, middleName, phone, address);
                storekeeperStorage.ReadStorekeeper(grid);
                this.Close();
            }
        }
    }
}
