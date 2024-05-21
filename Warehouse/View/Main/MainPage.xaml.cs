using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using Warehouse.Profile;
using Warehouse.Service;
using Warehouse.Storage;
using Warehouse.View.AddPage;
using Warehouse.View.EditPage;
using Warehouse.View.FIltrationAndSearch;
using Warehouse.View.OutputDocuments;

namespace Warehouse.View.Main
{
    public partial class MainPage : Window
    {
        private Database database = new Database();
        ProductTypeStorage productTypeStore = new ProductTypeStorage();
        CarStorage carStorage = new CarStorage();
        ProductStorage productStorage = new ProductStorage();
        DriverStorage driverStorage = new DriverStorage();
        StorekeeperStorage storekeeperStorage = new StorekeeperStorage();
        FreezerStorage freezerStorage = new FreezerStorage();
        FreezerCheckStorage freezerCheckStorage = new FreezerCheckStorage();
        DriverCheckStorage carCheckStorage = new DriverCheckStorage();
        CarCheckStorage driverCheckStorage = new CarCheckStorage();

        private MainLogic mainLogic;

        public MainPage()
        {
            InitializeComponent();
            if (AuthManager.CurrentUsername != null)
            {
                bool isAdmin = database.CheckAdmin(AuthManager.CurrentUsername);
                if (!isAdmin)
                {
                    AdminRegistration.Visibility = Visibility.Collapsed;
                } else
                {
                    AdminRegistration.Visibility = Visibility.Visible;
                }
            }

            mainLogic = new MainLogic();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)//Закрытие окна
        {
            Application.Current.Shutdown();
        }
        
        private void ProductType_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//вывод типа продукта
        {
            SupplierGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            OrderGrid.Visibility = Visibility.Collapsed;
            CarGrid.Visibility = Visibility.Collapsed;
            StorekeeperGrid.Visibility = Visibility.Collapsed;
            FreezerGrid.Visibility = Visibility.Collapsed;
            FreezerCheckGrid.Visibility = Visibility.Collapsed;
            CarCheckGrid.Visibility = Visibility.Collapsed;
            DriverCheckGrid.Visibility = Visibility.Collapsed;

            ProductTypeGrid.Visibility = Visibility.Visible;

            productTypeStore.ReadProductType(ProductTypeGrid);
        }

        private void AddProductType_Click(object sender, RoutedEventArgs e)//добавление типа продукта
        {
            ProductType product = new ProductType(ProductTypeGrid);
            product.ShowDialog();
        }

        private void EditProductType_Click(object sender, RoutedEventArgs e)//редакт типа продукта
        {
            var selectedRow = ProductTypeGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                ProductTypeEdit productTypeEdit = new ProductTypeEdit(Convert.ToInt32(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), ProductTypeGrid);
                productTypeEdit.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteProductType_Click(object sender, RoutedEventArgs e)//удаление типа продукта
        {
            DataRowView selectedRow = ProductTypeGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    productTypeStore.DeleteProductType(selectedRow);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этим типом продукта!");
                    return;
                }

                productTypeStore.ReadProductType(ProductTypeGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void FiltrationProductType_Click(object sender, RoutedEventArgs e)//фильтр типа продукта
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, ProductTypeGrid);
        }

        private void SearchProductType_Click(object sender, RoutedEventArgs e)//поиск типа продукта
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, ProductTypeGrid);
        }

        private void ProductTypeCancel_Click(object sender, RoutedEventArgs e)//Отмена типа продукта
        {
            productTypeStore.ReadProductType(ProductTypeGrid);
        }

        private void Product_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//вывод продукта
        {
            ProductTypeGrid.Visibility = Visibility.Collapsed;
            SupplierGrid.Visibility = Visibility.Collapsed;
            OrderGrid.Visibility = Visibility.Collapsed;
            CarGrid.Visibility = Visibility.Collapsed;
            StorekeeperGrid.Visibility = Visibility.Collapsed;
            FreezerGrid.Visibility = Visibility.Collapsed;
            FreezerCheckGrid.Visibility = Visibility.Collapsed;
            CarCheckGrid.Visibility = Visibility.Collapsed;
            DriverCheckGrid.Visibility = Visibility.Collapsed;

            ProductGrid.Visibility = Visibility.Visible;

            productStorage.ReadProduct(ProductGrid);
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)//Добавление продукта
        {
            ProductAdd product = new ProductAdd(ProductGrid);
            product.ShowDialog();
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)//редакт продукта
        {
            var selectedRow = ProductGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                ProductEdit productEdit = new ProductEdit(Convert.ToInt32(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[5]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[6]), ProductGrid);
                productEdit.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)//удаление продукта
        {
            DataRowView selectedRow = ProductGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    productStorage.DeleteProduct(selectedRow);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этим продуктом!");
                    return;
                }

                productStorage.ReadProduct(ProductGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void FiltrationProduct_Click(object sender, RoutedEventArgs e)//фильтр продукта
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, ProductGrid);
        }

        private void SearchProduct_Click(object sender, RoutedEventArgs e)//поиск продукта
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, ProductGrid);
        }

        private void ProductCancel_Click(object sender, RoutedEventArgs e)//отмена продукта
        {
            productStorage.ReadProduct(ProductGrid);
        }

        private void Supplier_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//вывод водителя
        {
            ProductGrid.Visibility = Visibility.Collapsed;
            ProductTypeGrid.Visibility = Visibility.Collapsed;
            OrderGrid.Visibility = Visibility.Collapsed;
            CarGrid.Visibility = Visibility.Collapsed;
            StorekeeperGrid.Visibility = Visibility.Collapsed;
            FreezerGrid.Visibility = Visibility.Collapsed;
            FreezerCheckGrid.Visibility = Visibility.Collapsed;
            CarCheckGrid.Visibility = Visibility.Collapsed;
            DriverCheckGrid.Visibility = Visibility.Collapsed;

            SupplierGrid.Visibility = Visibility.Visible;

            driverStorage.ReadDriver(SupplierGrid);
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)//Добавление водителя
        {
            SupplierAdd supplierAdd = new SupplierAdd(SupplierGrid);
            supplierAdd.ShowDialog();
        }

        private void EditSupplier_Click(object sender, RoutedEventArgs e)//редакт водителя
        {
            var selectedRow = SupplierGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                SupplierEdit supplierEdit = new SupplierEdit(Convert.ToInt32(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[5]), Convert.ToString(selectedRow.Row.ItemArray[6]), Convert.ToString(selectedRow.Row.ItemArray[7]), SupplierGrid);
                supplierEdit.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)//удалене водителя
        {
            DataRowView selectedRow = SupplierGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    driverStorage.DeleteDriver(selectedRow);
            }
                catch (SqlException)
                {
                MessageBox.Show("Удаление невозможно. Удалите связанные данные с этим водителем!");
                return;
            }

            driverStorage.ReadDriver(SupplierGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void FiltrationSupplier_Click(object sender, RoutedEventArgs e)//фильтр водителя
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, SupplierGrid);
        }

        private void SearchSupplier_Click(object sender, RoutedEventArgs e)//поиск водителя
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, SupplierGrid);
        }

        private void SupplierCancel_Click(object sender, RoutedEventArgs e)//отмена водителя
        {
            driverStorage.ReadDriver(SupplierGrid);
        }

        private void Storekeeper_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//Вывод кладовщика
        {
            ProductGrid.Visibility = Visibility.Collapsed;
            ProductTypeGrid.Visibility = Visibility.Collapsed;
            OrderGrid.Visibility = Visibility.Collapsed;
            CarGrid.Visibility = Visibility.Collapsed;
            SupplierGrid.Visibility = Visibility.Collapsed;
            FreezerGrid.Visibility = Visibility.Collapsed;
            FreezerCheckGrid.Visibility = Visibility.Collapsed;
            CarCheckGrid.Visibility = Visibility.Collapsed;
            DriverCheckGrid.Visibility = Visibility.Collapsed;

            StorekeeperGrid.Visibility = Visibility.Visible;


            storekeeperStorage.ReadStorekeeper(StorekeeperGrid);
        }

        private void AddStorekeeper_Click(object sender, RoutedEventArgs e)//добавление кладовщика
        {
            StorekeeperAdd storekeeperAdd = new StorekeeperAdd(StorekeeperGrid);
            storekeeperAdd.ShowDialog();
        }

        private void EditStorekeeper_Click(object sender, RoutedEventArgs e)//редактирование кладовщика
        {
            var selectedRow = StorekeeperGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                StorekeeperEdit storekeeperEdit = new StorekeeperEdit(Convert.ToInt32(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[5]), Convert.ToString(selectedRow.Row.ItemArray[4]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), Convert.ToString(selectedRow.Row.ItemArray[4]), StorekeeperGrid);
                storekeeperEdit.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteStorekeeper_Click(object sender, RoutedEventArgs e)//удаление кладовщика
        {
            DataRowView selectedRow = StorekeeperGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    storekeeperStorage.DeleteStorekeeper(selectedRow);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этим кладовщиком!");
                    return;
                }

                storekeeperStorage.ReadStorekeeper(StorekeeperGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void FiltrationStorekeeper_Click(object sender, RoutedEventArgs e)//фильтрация кладовщика
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, StorekeeperGrid);
        }

        private void SearchStorekeeper_Click(object sender, RoutedEventArgs e)// поиск кладовщика
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, StorekeeperGrid);
        }

        private void StorekeeperCancel_Click(object sender, RoutedEventArgs e)//отмена кладовщика
        {
            storekeeperStorage.ReadStorekeeper(StorekeeperGrid);
        }

        private void Car_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//Вывод машины
        {
            SupplierGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            OrderGrid.Visibility = Visibility.Collapsed;
            ProductTypeGrid.Visibility = Visibility.Collapsed;
            StorekeeperGrid.Visibility = Visibility.Collapsed;
            FreezerGrid.Visibility = Visibility.Collapsed;
            FreezerCheckGrid.Visibility = Visibility.Collapsed;
            CarCheckGrid.Visibility = Visibility.Collapsed;
            DriverCheckGrid.Visibility = Visibility.Collapsed;

            CarGrid.Visibility = Visibility.Visible;

            carStorage.ReadCar(CarGrid);
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)//добавление машины
        {
            CarAdd carAdd = new CarAdd(CarGrid);
            carAdd.ShowDialog();
        }

        private void EditCar_Click(object sender, RoutedEventArgs e)//редактирование машины
        {
            var selectedRow = CarGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                EditCar editCar = new EditCar(Convert.ToInt32(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), CarGrid);
                editCar.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)//Удаление машины
        {
            DataRowView selectedRow = CarGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    carStorage.DeleteCar(selectedRow);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этой машиной!");
                    return;
                }

                carStorage.ReadCar(CarGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void FiltrationCar_Click(object sender, RoutedEventArgs e)//Фильтрация машины
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, CarGrid);
        }

        private void SearchCar_Click(object sender, RoutedEventArgs e)//Поиск машины
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, CarGrid);
        }

        private void CarCancel_Click(object sender, RoutedEventArgs e)//Отмена машины
        {
            carStorage.ReadCar(CarGrid);
        }

        private void Order_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//вывод накладной
        {
            ProductTypeGrid.Visibility = Visibility.Collapsed;
            SupplierGrid.Visibility = Visibility.Collapsed;
            ProductGrid.Visibility = Visibility.Collapsed;
            CarGrid.Visibility = Visibility.Collapsed;
            StorekeeperGrid.Visibility = Visibility.Collapsed;
            FreezerGrid.Visibility = Visibility.Collapsed;
            FreezerCheckGrid.Visibility = Visibility.Collapsed;
            CarCheckGrid.Visibility = Visibility.Collapsed;
            DriverCheckGrid.Visibility= Visibility.Collapsed;

            OrderGrid.Visibility = Visibility.Visible;

            DataTable orderTable = database.GetOrdersWithProducts();
            OrderGrid.ItemsSource = orderTable.DefaultView;
        }

        private void OrderButtontetet_Click(object sender, RoutedEventArgs e)//Добавление ттн
        {
            OrderAdd order = new OrderAdd(OrderGrid);
            order.Show();
            ComboBoxOrder.dicrtionaryWithId1.Clear();
            ComboBoxOrder.dicrtionaryWithName.Clear();
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)//удаление накладкой
        {
            DataRowView selectedRow = OrderGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    database.DeleteOrder(selectedRow);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные!");
                    return;
                }

                DataTable orderTable = database.GetOrdersWithProducts();
                OrderGrid.ItemsSource = orderTable.DefaultView;
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void FiltrationOrder_Click(object sender, RoutedEventArgs e)//фильтр ттн
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, OrderGrid);
        }

        private void SearchOrder_Click(object sender, RoutedEventArgs e)//поиск ттн
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, OrderGrid);
        }

        private void OrderCancel_Click(object sender, RoutedEventArgs e)//отмена ттн
        {
            DataTable orderTable = database.GetOrdersWithProducts();
            OrderGrid.ItemsSource = orderTable.DefaultView;
        }

        private void VetDataGrid_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//вывод вет услуг
        {
            VetService vet = new VetService(FreezerGrid, FreezerCheckGrid, CarCheckGrid, DriverCheckGrid);
            vet.ShowDialog();
        }

        private void AddFreezer_Click(object sender, RoutedEventArgs e)//Добавление морозилки
        {
            FreezerAdd freezerAdd = new FreezerAdd(FreezerGrid);
            freezerAdd.ShowDialog();
        }

        private void EditFreezer_Click(object sender, RoutedEventArgs e)//Редактирование морозилки
        {
            var selectedRow = FreezerGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                FreezerEdit freezerEdit = new FreezerEdit(Convert.ToInt32(selectedRow.Row.ItemArray[0]), Convert.ToString(selectedRow.Row.ItemArray[1]), Convert.ToString(selectedRow.Row.ItemArray[2]), Convert.ToString(selectedRow.Row.ItemArray[3]), FreezerGrid);
                freezerEdit.ShowDialog();

            }
            else
            {
                MessageBox.Show("Не выбрана строка для редактирования", "Ошибка", MessageBoxButton.OK);
            }
        }

        private void DeleteFreezer_Click(object sender, RoutedEventArgs e)//Удаление морозилки
        {
            DataRowView selectedRow = FreezerGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                try
                {
                    freezerStorage.DeleteFreezer(selectedRow);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Удаление невозможно. Удалите связанные данные с этой камерой!");
                    return;
                }

                freezerStorage.ReadFreezer(FreezerGrid);
            }
            else
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private void FiltrationFreezer_Click(object sender, RoutedEventArgs e)//Фильтрация морозилки
        {
            Filtration filtration = new Filtration();
            filtration.ShowDialog();

            string field = filtration.Field;

            if (field == null)
                return;

            mainLogic.ApplyFilter(field, FreezerGrid);
        }

        private void SearchFreezer_Click(object sender, RoutedEventArgs e)//Поиск морозилки
        {
            Search search = new Search();
            search.ShowDialog();

            string field = search.Field;

            if (field == null)
                return;

            mainLogic.SearchAndSort(field, FreezerGrid);
        }

        private void FreezerCancel_Click(object sender, RoutedEventArgs e)//Отмена морозилки
        {
            freezerStorage.ReadFreezer(FreezerGrid);
        }

        private void AdminRegistration_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//регистрация
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }

        private void Settings_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//Настройки
        {
            PrfSettings settings = new PrfSettings();
            settings.ShowDialog();
        }

        private void Output_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)//вывод печати
        {
            MainOutput main = new MainOutput(OrderGrid);
            main.ShowDialog();
        }

        private void AddFreezerCheck_Click(object sender, RoutedEventArgs e)//Добавление мороз камеры
        {
            
        }

        private void EditFreezerCheck_Click(object sender, RoutedEventArgs e)//Редакт мороз камеры
        {

        }

        private void DeleteFreezerCheck_Click(object sender, RoutedEventArgs e)//Удаление мороз камеры
        {

        }

        private void FiltrationFreezerCheck_Click(object sender, RoutedEventArgs e)//Фильтр мороз камеры
        {

        }

        private void SearchFreezerCheck_Click(object sender, RoutedEventArgs e)//Поиск мороз камеры
        {

        }

        private void FreezerCheckCancel_Click(object sender, RoutedEventArgs e)//Отмена мороз камеры
        {

        }
    }
}