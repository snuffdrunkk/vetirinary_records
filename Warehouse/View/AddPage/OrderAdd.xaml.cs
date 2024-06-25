using System;
using System.CodeDom;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.DTO;
using Warehouse.Service;

namespace Warehouse.View.AddPage
{
    public partial class OrderAdd : Window
    {
        Database database = new Database();
        ValidationFileds validation = new ValidationFileds();
        DataGrid grid = new DataGrid();
        private bool typeComboToProduct = false;

        public OrderAdd(DataGrid grid)
        {
            InitializeComponent();

            OrderTypeComboBox.SelectionChanged += OrderTypeComboBox_SelectionChanged;

            database.ReadSupplierToComboBox(SupplierComboBox);
            database.ReadCarToComboBox(CarComboBox);
            database.ReadStorekeeperToComboBox(StorekeeperComboBox);

            this.grid = grid;
            Date.SelectedDate = DateTime.Today;

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();

            imageControl.Source = bitmap;

            OrderTypeComboBoxProduct();
        }

        private void OrderTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content.ToString() == "Поступление")
                {
                    ArrivalPointBox.Text = "Гомель, аг. Коммунар, ул. Приозерная, 1";
                    ArrivalPointBox.IsEnabled = false;
                    DeparturePointBox.Text = string.Empty;
                    DeparturePointBox.IsEnabled = true;
                }
                else if (selectedItem.Content.ToString() == "Выбытие")
                {
                    ArrivalPointBox.Text = string.Empty;
                    ArrivalPointBox.IsEnabled = true;
                    DeparturePointBox.Text = "Гомель, аг. Коммунар, ул. Приозерная, 1";
                    DeparturePointBox.IsEnabled = false;
                }
            }
        }

        private void OrderTypeComboBoxProduct()
        {
            if (OrderTypeComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content.ToString() == "Поступление")
                {
                    typeComboToProduct = false;
                }
                else if (selectedItem.Content.ToString() == "Выбытие")
                {
                    typeComboToProduct = true;
                }
            }
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            AddFromComboBoxOrder add = new AddFromComboBoxOrder(SupplierGrid, ProductCost, typeComboToProduct);
            add.ShowDialog();
        }

        private void ReturnToMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxDTO supplier = (ComboBoxDTO)SupplierComboBox.SelectedItem;
            ComboBoxDTO car = (ComboBoxDTO)CarComboBox.SelectedItem;
            ComboBoxDTO keeper = (ComboBoxDTO)StorekeeperComboBox.SelectedItem;
            string departurePointBox = DeparturePointBox.Text;
            string arrivalPointBox = ArrivalPointBox.Text;

            try
            {
                string orderDate = Date.SelectedDate.Value.ToString("yyyy-MM-dd");

                ValidationFileds validation = new ValidationFileds();

                if (validation.ValidationComboBoxProduct(supplier, "водителя") && validation.ValidationComboBoxProduct(car, "машину") && validation.ValidationComboBoxProduct(keeper, "кладовщика") && validation.ValidationComboBox(((ComboBoxItem)OrderTypeComboBox.SelectedItem).Content.ToString(), "Тип заказа") && validation.ValidateAmount(ProductCost.Text) && validation.ValidateAdressOrd(DeparturePointBox.Text) && validation.ValidateAdressOrd(ArrivalPointBox.Text))
                {
                    database.CreateOrder(supplier, validation.CastCostToDouble(ProductCost.Text), ((ComboBoxItem)OrderTypeComboBox.SelectedItem).Content.ToString(), orderDate, car.id, keeper.id, departurePointBox, arrivalPointBox);
                    DataTable orderTable = database.GetOrdersWithProducts();
                    grid.ItemsSource = orderTable.DefaultView;
                    this.Close();
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Выберите дату!");
            }
        }

        private void OrderTypeComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            OrderTypeComboBoxProduct();
        }
    }
}
