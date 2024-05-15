using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Warehouse.View.AddPage
{

    public partial class CarAdd : Window
    {
        DataGrid data;
        Database database = new Database();

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

            database.CreateCar(number, mark, scrutiny);
            database.ReadCar(data);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
