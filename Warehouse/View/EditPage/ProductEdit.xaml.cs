using System;
using System.Text.RegularExpressions;
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
            ProductDescriptionBox.Text = description;
            OrderSuitabilityComboBox.Text = suitability;
            ProductTypeComboBox.SelectedIndex = 0;

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
            string description = ProductDescriptionBox.Text;
            string suitability = OrderSuitabilityComboBox.Text;

            ValidationFileds validation = new ValidationFileds();

            if (ValidationProductEdit(title, cost, description, suitability))
            {
                productStorage.UpdateProduct(id, title, validation.CastCostToDouble(cost), description, suitability);
                productStorage.ReadProduct(grid);
                this.Close();
            }
        }

        public bool ValidationProductEdit(string title, string cost, string description, string suitability)//Редакт продукта
        {
            if (!ValidationProductTypeTitle(title))
                return false;

            if (!ValidationCost(cost))
                return false;

            if (!ValidationProductDescription(description))
                return false;

            if (!ValidationProductSuitability(suitability))
                return false;

            return true;
        }

        public bool ValidationProductTypeTitle(string title)//Проверка типа продукта
        {
            string productTypePattern = @"^(?![- ])(?!.*[- ]{2})[a-zA-Zа-яА-Я -]{3,30}(?<![- ])$";

            if (!Regex.IsMatch(title, productTypePattern))
            {
                MessageBox.Show("Размер наименования от 3 до 30 символов, без цифр и знаков!");
                return false;
            }

            return true;
        }

        public bool ValidationCost(string cost)
        {
            if (int.TryParse(cost, out int costDouble))
            {
                if (costDouble <= 0 || costDouble > 10000)
                {
                    MessageBox.Show("Число должно быть больше 0 и не больше 10000");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Введите число!");
                return false;
            }

            return true;
        }

        public bool ValidationProductDescription(string description)
        {

            if (description.Length < 3 || description.Length > 60)
            {
                MessageBox.Show("Размер описания от 3 до 60 символов!");
                return false;
            }

            return true;
        }

        public bool ValidationProductSuitability(string suitability)
        {
            if (OrderSuitabilityComboBox.IsEnabled)
            {
                if (string.IsNullOrEmpty(suitability))
                {
                    MessageBox.Show("Выберите пригодность продукта!");
                    return false;
                }

                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
