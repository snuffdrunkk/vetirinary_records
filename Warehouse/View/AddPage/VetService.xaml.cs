using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Warehouse.Storage;

namespace Warehouse.View.AddPage
{
    public partial class VetService : Window
    {
        private DataGrid dataGridFrz;
        private DataGrid dataGridFrzCheck;
        private DataGrid dataGridCarCheck;
        private DataGrid dataGridDriverCheck;
        private DataGrid dataGridStorekeeperCheck;
        private FreezerStorage freezerStorage = new FreezerStorage();
        private FreezerCheckStorage freezerCheckStorage = new FreezerCheckStorage();
        private CarCheckStorage carCheckStorage = new CarCheckStorage();
        private DriverCheckStorage driverCheckStorage = new DriverCheckStorage();
        private StorekeeperCheckStorage storekeeperCheckStorage = new StorekeeperCheckStorage();

        public VetService(DataGrid dataGridFrz, DataGrid dataGridFrzCheck, DataGrid dataGridCarCheck, DataGrid dataGridDriverCheck, DataGrid dataGridStorekeeperCheck)
        {
            InitializeComponent();

            string imagePath = "D:\\ДИПЛОМ\\warehouse-main\\Warehouse\\Resources\\logo.jpg";

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            imageControl.Source = bitmap;

            this.dataGridFrz = dataGridFrz;
            this.dataGridFrzCheck = dataGridFrzCheck;
            this.dataGridCarCheck = dataGridCarCheck;
            this.dataGridDriverCheck = dataGridDriverCheck;
            this.dataGridStorekeeperCheck = dataGridStorekeeperCheck;
        }

        private void FreezerGrid_Click(object sender, RoutedEventArgs e)//Вывод морозильной камеры
        {
            freezerStorage.ReadFreezer(dataGridFrz);

            dataGridStorekeeperCheck.Visibility = Visibility.Collapsed;
            dataGridDriverCheck.Visibility = Visibility.Collapsed;
            dataGridCarCheck.Visibility = Visibility.Collapsed;
            dataGridFrzCheck.Visibility = Visibility.Collapsed;

            dataGridFrz.Visibility = Visibility.Visible;
            this.Close();
        }

        private void FreezerReportGrid_Click(object sender, RoutedEventArgs e)//Вывод отчета морозилки
        {
            freezerCheckStorage.ReadFreezerCheck(dataGridFrzCheck);

            dataGridStorekeeperCheck.Visibility = Visibility.Collapsed;
            dataGridDriverCheck.Visibility = Visibility.Collapsed;
            dataGridCarCheck.Visibility = Visibility.Collapsed;
            dataGridFrz.Visibility = Visibility.Collapsed;

            dataGridFrzCheck.Visibility = Visibility.Visible;

            this.Close();
        }

        private void DriverReportGrid_Click(object sender, RoutedEventArgs e)//Вывод очтета водителя
        {
            driverCheckStorage.ReadDriverCheck(dataGridDriverCheck);

            dataGridStorekeeperCheck.Visibility = Visibility.Collapsed;
            dataGridFrz.Visibility = Visibility.Collapsed;
            dataGridCarCheck.Visibility = Visibility.Collapsed;
            dataGridFrzCheck.Visibility = Visibility.Collapsed;

            dataGridDriverCheck.Visibility = Visibility.Visible;

            this.Close();
        }

        private void CarRecord_Click(object sender, RoutedEventArgs e)//Вывод отчета машины
        {
            carCheckStorage.ReadCarCheck(dataGridCarCheck);

            dataGridStorekeeperCheck.Visibility = Visibility.Collapsed;
            dataGridDriverCheck.Visibility = Visibility.Collapsed;
            dataGridFrzCheck.Visibility = Visibility.Collapsed;
            dataGridFrz.Visibility = Visibility.Collapsed;

            dataGridCarCheck.Visibility = Visibility.Visible;

            this.Close();
        }

        private void StorekeeperReportGrid_Click(object sender, RoutedEventArgs e)//Вывод отчета кладовщика
        {
            storekeeperCheckStorage.ReadStorekeeperCheck(dataGridStorekeeperCheck);

            dataGridCarCheck.Visibility = Visibility.Collapsed;
            dataGridDriverCheck.Visibility = Visibility.Collapsed;
            dataGridFrzCheck.Visibility = Visibility.Collapsed;
            dataGridFrz.Visibility = Visibility.Collapsed;

            dataGridStorekeeperCheck.Visibility = Visibility.Visible;

            this.Close();
        }
    }
}
