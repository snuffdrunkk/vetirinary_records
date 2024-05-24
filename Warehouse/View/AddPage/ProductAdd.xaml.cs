using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class ProductAdd : Window
    {
        DataGrid grid;
        ProductStorage productStorage = new ProductStorage();
        ProductTypeStorage productTypeStorage = new ProductTypeStorage();

        public ProductAdd(DataGrid grid)
        {
            InitializeComponent();
            this.grid = grid;
            productTypeStorage.ReadProductTypeToComboBox(ProductTypeComboBox);

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
            string title = ProductTitleBox.Text;
            ComboBoxDTO dto = (ComboBoxDTO) ProductTypeComboBox.SelectedItem;
            string cost = ProductCost.Text;
            string description = ProductDescriptionBox.Text;
            string suitability = OrderSuitabilityComboBox.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationProductAdd(title, cost, description, dto))
            {
                productStorage.CreateProduct(title, validation.CastCostToDouble(cost), description, suitability, dto);
                productStorage.ReadProduct(grid);
            }
        }
    }
}
