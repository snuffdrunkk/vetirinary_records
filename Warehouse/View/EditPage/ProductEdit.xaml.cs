using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Service;
using Warehouse.Storage;

namespace Warehouse.View.EditPage
{
    public partial class ProductEdit : Window
    {
        DataGrid grid;
        ProductStorage productStorage = new ProductStorage();

        int id;

        public ProductEdit(int id, string title, string productType, string presence, string cost, string description, string suitability, DataGrid grid)
        {
            InitializeComponent();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;

            this.grid = grid;
            this.id = id;
            ProductTitleBox.Text = title;
            ProductTypeComboBox.Items.Add(productType);
            ProductTypeComboBox.SelectedIndex = 0;
            ProductCost.Text = cost;
            ProductDescription.Text = description;
            OrderSuitabilityComboBox.Text = suitability;

            if (!presence.Trim().Equals("0"))
            {
                OrderSuitabilityComboBox.IsEnabled = true;
            }
            else
            {
                OrderSuitabilityComboBox.IsEnabled = false;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string title = ProductTitleBox.Text;
            string cost = ProductCost.Text;
            string description = ProductDescription.Text;
            string suitability = OrderSuitabilityComboBox.Text;

            ValidationFileds validation = new ValidationFileds();

            if (validation.ValidationProductEdit(title, cost, description))
            {
                productStorage.UpdateProduct(id, title, validation.CastCostToDouble(cost), description, suitability);
                productStorage.ReadProduct(grid);

                this.Close();
            }
        }
    }
}
