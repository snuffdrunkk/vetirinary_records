using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{

    public partial class ProductType : Window
    {
        DataGrid data;
        ProductTypeStorage productTypeStore = new ProductTypeStorage();

        public ProductType(DataGrid data)
        {
            this.data = data;
            InitializeComponent();

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
            string title = ProductTypeBox.Text;

            ValidationFileds validation = new ValidationFileds();
            if (validation.ValidationProductTypeTitle(title))
            {
                productTypeStore.CreateProductType(title);
                productTypeStore.ReadProductType(data);
            }
        }
    }
}
