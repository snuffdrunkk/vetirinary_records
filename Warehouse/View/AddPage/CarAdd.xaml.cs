using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{

    public partial class CarAdd : Window
    {
        DataGrid data;
        Database database = new Database();
        CarStorage carStorage = new CarStorage();

        public CarAdd(DataGrid data)
        {
            InitializeComponent();
            this.data = data;

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string number = CarNumberBox.Text;
            string mark = CarMarkBox.Text;
            string scrutiny = CarScrutinyComboBox.Text;

            carStorage.CreateCar(number, mark, scrutiny);
            carStorage.ReadCar(data);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
