using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.EditPage
{
    public partial class ProductTypeEdit : Window
    {
        long id;

        DataGrid grid;
        ProductTypeStorage productTypeStore = new ProductTypeStorage();

        public ProductTypeEdit(long id, string title, DataGrid grid)
        {
            InitializeComponent();
            this.id = id;
            this.grid = grid;
            ProductTypeBox.Text = title;

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
                Database database = new Database();

                productTypeStore.UpdateProductType(id, title);
                productTypeStore.ReadProductType(grid);

                this.Close();
            }
        }
    }
}
