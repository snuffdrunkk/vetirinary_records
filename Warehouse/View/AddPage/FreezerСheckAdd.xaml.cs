using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class FreezerСheckAdd : Window
    {
        DataGrid data;
        Database database = new Database();
        FreezerCheckStorage freezerCheckStorage = new FreezerCheckStorage();
        public FreezerСheckAdd(DataGrid data)
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

        private void Return_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackToSecondPage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
