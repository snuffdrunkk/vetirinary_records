using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class FreezerAdd : Window
    {
        DataGrid data;
        Database database = new Database();
        FreezerStorage freezerStorage = new FreezerStorage();
        public FreezerAdd(DataGrid data)
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
            string name = FreezerNameBox.Text;
            string description = FreezerDescriptionBox.Text;
            string volume = FreezerVolumeBox.Text;

            freezerStorage.CreateFreezer(name, description, volume);
            freezerStorage.ReadFreezer(data);
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
